import { RotateCcw } from 'lucide-react'

import type {
  ChangePreviewView,
  SemanticBindingView,
  SqlActionRequest,
  SqlModelView,
  SymbolView,
  ValueSchemaView,
} from './contracts.generated'
import { EmbeddedPythonEditor } from './EmbeddedPythonEditor'
import { SchemaValueField, type FieldDraftProps } from './SchemaValueField'
import { FileListSelector, FileSelector, OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { StructuredSqlEditor } from './StructuredSqlEditor'
import { effectiveBindingValue, RESET_VALUE, type FieldPath } from './workspaceState'

interface Props extends FieldDraftProps {
  tabId: string
  binding: SemanticBindingView
  values: Record<string, unknown>
  readOnly: boolean
  knownFiles: string[]
  symbols: SymbolView[]
  onUploadFile: (file: File) => Promise<string>
  onEdit: (binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => void
  validateBinding: (tabId: string, bindingId: string, value: unknown) => Promise<ChangePreviewView>
  inspectSql: (tabId: string, bindingId: string) => Promise<SqlModelView>
  runSqlAction: (
    tabId: string,
    bindingId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

export function SemanticBindingField({
  tabId,
  binding,
  values,
  readOnly,
  knownFiles,
  symbols,
  onUploadFile,
  onEdit,
  validateBinding,
  inspectSql,
  runSqlAction,
  drafts,
  onDraft,
}: Props) {
  const value = effectiveBindingValue(values, binding)
  const disabled = readOnly || !binding.editable
  const reset = binding.resettable && Object.hasOwn(values, binding.id)
    ? <button type="button" className="icon-button" aria-label={`Reset ${binding.display_label}`} title="Reset value" onClick={() => onEdit(binding, RESET_VALUE)}><RotateCcw size={14} aria-hidden="true" /></button>
    : null

  if (binding.capabilities.includes('embedded-python')) {
    return <div className="parameter semantic-binding">
      <div className="parameter-field__meta"><strong>{binding.display_label}</strong>{reset}</div>
      <EmbeddedPythonEditor
        tabId={tabId}
        binding={binding}
        value={String(value ?? '')}
        disabled={disabled}
        validateBinding={validateBinding}
        onCommit={(next) => onEdit(binding, next)}
      />
    </div>
  }

  if (binding.capabilities.includes('structured-sql')) {
    return <div className="parameter semantic-binding">
      <StructuredSqlEditor
        tabId={tabId}
        binding={binding}
        values={values}
        readOnly={disabled}
        inspect={inspectSql}
        runAction={runSqlAction}
        onReset={() => onEdit(binding, RESET_VALUE)}
      />
    </div>
  }

  const schema = binding.value_schema
  const controlSchema = !binding.required && schema?.nullable ? { ...schema, nullable: false } : schema
  const body = renderBindingControl({
    binding,
    value,
    disabled,
    schema: controlSchema,
    knownFiles,
    symbols,
    onUploadFile,
    onEdit,
    drafts,
    onDraft,
  })

  return <fieldset className={`parameter semantic-binding${disabled ? ' parameter--readonly' : ''}`} disabled={disabled}>
    {binding.required
      ? <>
          <div className="parameter-field__meta"><strong>{binding.display_label} *</strong>{reset}</div>
          {body}
        </>
      : <OptionalValue
          label={binding.display_label}
          enabled={value !== null && value !== undefined}
          disabled={disabled}
          action={reset}
          onEnabledChange={(enabled) => onEdit(binding, enabled ? enabledValue(binding, schema) : null)}
        >{body}</OptionalValue>}
    {disabled && binding.read_only_reason && <small>{binding.read_only_reason}</small>}
    {binding.validation_state === 'unresolved' && <ValidationMessage message="This value does not resolve to a known symbol." />}
  </fieldset>
}

function renderBindingControl({
  binding,
  value,
  disabled,
  schema,
  knownFiles,
  symbols,
  onUploadFile,
  onEdit,
  drafts,
  onDraft,
}: {
  binding: SemanticBindingView
  value: unknown
  disabled: boolean
  schema: ValueSchemaView | null
  knownFiles: string[]
  symbols: SymbolView[]
  onUploadFile: (file: File) => Promise<string>
  onEdit: Props['onEdit']
  drafts: Props['drafts']
  onDraft: Props['onDraft']
}) {
  const isFileBinding = binding.capabilities.includes('file-input') || binding.capabilities.includes('file-output')
  if (isFileBinding && schema?.kind === 'list') {
    return <FileListSelector label={binding.display_label} value={value} files={knownFiles} disabled={disabled} onChange={(next) => onEdit(binding, next)} onUpload={onUploadFile} />
  }
  if (isFileBinding) {
    return <FileSelector label={binding.display_label} value={value} files={knownFiles} disabled={disabled} onChange={(next) => onEdit(binding, next)} />
  }
  if (binding.capabilities.includes('symbol-or-literal')) {
    return <SymbolSelector label={binding.display_label} value={value} symbols={symbols} disabled={disabled} onChange={(next) => onEdit(binding, next)} />
  }
  if (schema) {
    return <SchemaValueField
      schema={schema}
      value={value}
      onChange={(next, cleared) => onEdit(binding, next, cleared)}
      label={binding.display_label}
      bindingId={binding.id}
      path={[]}
      multiline={false}
      drafts={drafts}
      onDraft={onDraft}
    />
  }
  return <input aria-label={binding.display_label} disabled={disabled} value={String(value ?? '')} onChange={(event) => onEdit(binding, event.target.value)} />
}

function enabledValue(binding: SemanticBindingView, schema: ValueSchemaView | null): unknown {
  if (binding.value !== null && binding.value !== undefined) return binding.value
  if (binding.default !== null && binding.default !== undefined) return binding.default
  if (!schema) return ''
  return defaultValue({ ...schema, nullable: false })
}

function defaultValue(schema: ValueSchemaView): unknown {
  if (schema.choices.length) return schema.choices[0]
  if (schema.kind === 'boolean') return false
  if (schema.kind === 'integer' || schema.kind === 'number') return 0
  if (schema.kind === 'list') return schema.tuple_value ? schema.prefix_items.map(defaultValue) : []
  if (schema.kind === 'object') return Object.fromEntries(schema.required_keys.map((key) => [key, defaultValue(schema.properties[key])]))
  if (schema.kind === 'union') return schema.variants[0] ? defaultValue(schema.variants[0]) : ''
  return ''
}
