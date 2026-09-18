import { useId, useState } from 'react'
import { Plus, X } from 'lucide-react'
import type { ValueSchemaView } from './contracts.generated'
import type { FieldPath, TabState } from './workspaceState'

export interface FieldDraftProps {
  drafts: TabState['fieldDrafts']
  onDraft: (key: string, draft: TabState['fieldDrafts'][string] | null) => void
}

export interface SchemaValueProps extends FieldDraftProps {
  schema: ValueSchemaView
  value: unknown
  onChange: (value: unknown, clearDraftPaths?: FieldPath[]) => void
  label: string
  bindingId: string
  path: Array<string | number>
  multiline?: boolean
}

export function SchemaValueField(props: SchemaValueProps) {
  const { schema, value, label, bindingId, path, drafts, onDraft } = props
  const onChange = (next: unknown, clearDraftPaths: FieldPath[] = [path]) => props.onChange(next, clearDraftPaths)
  const id = useId()
  const [newKey, setNewKey] = useState('')
  const key = JSON.stringify([bindingId, ...path])
  const nested = (child: ValueSchemaView, item: unknown, name: string | number, change: SchemaValueProps['onChange']) => <SchemaValueField {...props} schema={child} value={item} path={[...path, name]} label={`${label} ${name}`} onChange={change} multiline={false} />
  if (schema.nullable) return <div className="nullable-field"><label className="checkbox-field"><input type="checkbox" aria-label={`${label} enabled`} checked={value !== null} onChange={(event) => onChange(event.target.checked ? defaultSchemaValue({ ...schema, nullable: false }) : null)} /><span>{value === null ? 'Not set' : 'Set value'}</span></label>{value !== null && <SchemaValueField {...props} schema={{ ...schema, nullable: false }} />}</div>
  if (schema.choices.length) return <select id={id} aria-label={label} value={schema.choices.findIndex((choice) => Object.is(choice, value))} onChange={(event) => onChange(schema.choices[Number(event.target.value)])}>{schema.choices.map((choice, index) => <option key={index} value={index}>{String(choice)}</option>)}</select>
  if (schema.kind === 'union') {
    const selected = Math.max(0, schema.variants.findIndex((variant) => matches(variant, value)))
    return <div className="union-field"><select aria-label={`${label} type`} value={selected} onChange={(event) => onChange(defaultSchemaValue(schema.variants[Number(event.target.value)]))}>{schema.variants.map((variant, index) => <option key={index} value={index}>{variant.tuple_value ? 'Fixed sequence' : humanize(variant.kind)}</option>)}</select>{schema.variants[selected] && <SchemaValueField {...props} schema={schema.variants[selected]} />}</div>
  }
  if (schema.kind === 'boolean') return <label className="checkbox-field"><input id={id} aria-label={label} type="checkbox" checked={value === true} onChange={(event) => onChange(event.target.checked)} /><span>{value ? 'On' : 'Off'}</span></label>
  if (schema.kind === 'integer' || schema.kind === 'number') {
    const draft = drafts[key]
    return <div className="numeric-field"><input id={id} aria-label={label} type="text" inputMode={schema.kind === 'integer' ? 'numeric' : 'decimal'} value={draft?.text ?? String(value ?? '')} aria-invalid={Boolean(draft)} aria-describedby={draft ? `${id}-error` : undefined} onChange={(event) => {
      const text = event.target.value
      const number = Number(text)
      if (!text.trim() || !Number.isFinite(number) || text.endsWith('.') || schema.kind === 'integer' && !Number.isInteger(number)) {
        onDraft(key, { bindingId, text, error: schema.kind === 'integer' ? 'Enter a whole number.' : 'Enter a finite number.' })
      } else { onChange(number) }
    }} />{draft && <small id={`${id}-error`} className="validation-error" role="alert">{draft.error}</small>}</div>
  }
  if (schema.kind === 'list') {
    const items = Array.isArray(value) ? value : []
    return <div className="collection-field">{(schema.tuple_value ? schema.prefix_items : items.map(() => schema.items)).map((child, index) => child && <div className="collection-row" key={index}>{nested(child, items[index], index + 1, (next, cleared) => onChange(items.map((item, position) => position === index ? next : item), cleared))}{!schema.tuple_value && <button type="button" className="icon-button" aria-label={`Remove ${label} item ${index + 1}`} onClick={() => onChange(items.filter((_, position) => position !== index), items.slice(index).map((_, offset) => [...path, index + offset + 1]))}><X size={14} aria-hidden="true" /></button>}</div>)}{!schema.tuple_value && schema.items && <button className="field-command" type="button" onClick={() => onChange([...items, defaultSchemaValue(schema.items!)], [])}><Plus size={14} aria-hidden="true" />Add item</button>}</div>
  }
  if (schema.kind === 'object') {
    const object = value && typeof value === 'object' && !Array.isArray(value) ? value as Record<string, unknown> : {}
    const entries = Object.keys(schema.properties).length ? Object.entries(schema.properties) : Object.keys(object).map((name) => [name, schema.items] as const)
    return <div className="object-field">{entries.map(([name, child]) => child && <div key={name} className="object-row"><div className="object-key"><strong>{humanize(name)}</strong>{!schema.required_keys.includes(name) && <input type="checkbox" aria-label={`Include ${label} ${name}`} checked={Object.hasOwn(object, name)} onChange={(event) => { const next = { ...object }; if (event.target.checked) next[name] = defaultSchemaValue(child); else delete next[name]; onChange(next, [[...path, name]]) }} />}</div>{Object.hasOwn(object, name) && nested(child, object[name], name, (next, cleared) => onChange({ ...object, [name]: next }, cleared))}</div>)}{schema.items && <div className="map-add"><input aria-label={`${label} new key`} value={newKey} onChange={(event) => setNewKey(event.target.value)} /><button type="button" className="field-command" disabled={!newKey.trim() || Object.hasOwn(object, newKey.trim())} onClick={() => { onChange(Object.fromEntries([...Object.entries(object), [newKey.trim(), defaultSchemaValue(schema.items!)]]), []); setNewKey('') }}><Plus size={14} aria-hidden="true" />Add key</button></div>}</div>
  }
  if (schema.kind === 'string') return props.multiline ? <textarea id={id} aria-label={label} rows={5} value={String(value ?? '')} onChange={(event) => onChange(event.target.value)} /> : <input id={id} aria-label={label} type="text" value={String(value ?? '')} onChange={(event) => onChange(event.target.value)} />
  return <pre className="parameter-source">{JSON.stringify(value, null, 2)}</pre>
}

export function defaultSchemaValue(schema: ValueSchemaView): unknown {
  if (schema.nullable) return null
  if (schema.choices.length) return schema.choices[0]
  if (schema.kind === 'union') return schema.variants[0] ? defaultSchemaValue(schema.variants[0]) : null
  if (schema.kind === 'boolean') return false
  if (schema.kind === 'integer' || schema.kind === 'number') return 0
  if (schema.kind === 'list') return schema.tuple_value ? schema.prefix_items.map(defaultSchemaValue) : []
  if (schema.kind === 'object') return Object.fromEntries(schema.required_keys.map((key) => [key, defaultSchemaValue(schema.properties[key])]))
  return ''
}

function matches(schema: ValueSchemaView, value: unknown): boolean {
  if (value === null) return schema.nullable
  if (schema.kind === 'string') return typeof value === 'string'
  if (schema.kind === 'integer' || schema.kind === 'number') return typeof value === 'number'
  if (schema.kind === 'boolean') return typeof value === 'boolean'
  if (schema.kind === 'list') return Array.isArray(value)
  return schema.kind === 'object' && typeof value === 'object'
}

function humanize(value: string): string { return value.replaceAll('_', ' ').replace(/(^|\s)\S/g, (match) => match.toUpperCase()) }