import { useEffect, useMemo, useRef, useState } from 'react'
import { Code2, RotateCcw } from 'lucide-react'

import type {
  ChangePreviewView,
  SemanticBindingView,
  SemanticOperationView,
  SqlActionRequest,
  SqlModelView,
  SymbolView,
} from './contracts.generated'
import { SchemaValueField, type FieldDraftProps } from './ParameterField'
import { FileListSelector, FileSelector, OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { StructuredSqlEditor } from './StructuredSqlEditor'
import { RESET_VALUE, effectiveBindingValue, type FieldPath } from './workspaceState'

interface Props extends FieldDraftProps {
  tabId: string
  operation: SemanticOperationView
  values: Record<string, unknown>
  saving: boolean
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

export function SemanticOperationEditor({
  tabId,
  operation,
  values,
  saving,
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
  const normal = operation.bindings.filter((binding) => binding.visibility === 'normal')
  const advanced = operation.bindings.filter((binding) => binding.visibility === 'advanced')
  const readOnly = operation.validation_state === 'unsupported'

  return <section className="operation-editor semantic-operation-editor" aria-label={`Configure ${operation.display_name}`}>
    <header className="operation-editor__header">
      <div>
        <span className="eyebrow">Configuration</span>
        <h3>{operation.display_name}</h3>
        {operation.description && <p className="operation-description">{operation.description}</p>}
      </div>
      <span className={`state-pill${readOnly ? ' state-pill--readonly' : ''}`}>
        {readOnly ? 'Read only' : operation.validation_state === 'unresolved' ? 'Needs attention' : 'Editable'}
      </span>
    </header>

    {operation.validation_state === 'unresolved' && <ValidationMessage message="One or more values reference an unresolved symbol." />}

    <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
      {operation.capabilities.includes('condition-editor')
        ? <ConditionEditor operation={operation} values={values} symbols={symbols} onEdit={onEdit} />
        : normal.map((binding) => <BindingField
            key={binding.id}
            tabId={tabId}
            binding={binding}
            values={values}
            readOnly={readOnly}
            knownFiles={knownFiles}
            symbols={symbols}
            onUploadFile={onUploadFile}
            drafts={drafts}
            onDraft={onDraft}
            onEdit={onEdit}
            validateBinding={validateBinding}
            inspectSql={inspectSql}
            runSqlAction={runSqlAction}
          />)}
      {!operation.capabilities.includes('condition-editor') && normal.length === 0 && <p className="empty-copy">No configurable values.</p>}
    </fieldset>

    {advanced.length > 0 && <details className="advanced-settings">
      <summary>Advanced</summary>
      <div className="parameter-grid">{advanced.map((binding) => <BindingField
        key={binding.id}
        tabId={tabId}
        binding={binding}
        values={values}
        readOnly={readOnly}
        knownFiles={knownFiles}
        symbols={symbols}
        onUploadFile={onUploadFile}
        drafts={drafts}
        onDraft={onDraft}
        onEdit={onEdit}
        validateBinding={validateBinding}
        inspectSql={inspectSql}
        runSqlAction={runSqlAction}
      />)}</div>
    </details>}

    {operation.comments.length > 0 && <details className="operation-comments"><summary>Comments</summary>{operation.comments.map((comment, index) => <p key={index}>{comment}</p>)}</details>}
  </section>
}

function BindingField({
  tabId,
  binding,
  values,
  readOnly,
  knownFiles,
  symbols,
  onUploadFile,
  drafts,
  onDraft,
  onEdit,
  validateBinding,
  inspectSql,
  runSqlAction,
}: Omit<Props, 'operation' | 'saving'> & { binding: SemanticBindingView; readOnly: boolean }) {
  const value = effectiveBindingValue(values, binding)
  const disabled = readOnly || !binding.editable
  const reset = binding.resettable && binding.id in values
    ? <button type="button" className="icon-button" aria-label={`Reset ${binding.display_label}`} title="Reset value" onClick={() => onEdit(binding, RESET_VALUE)}><RotateCcw size={14} aria-hidden="true" /></button>
    : null

  if (binding.capabilities.includes('embedded-python')) {
    return <div className="parameter semantic-binding"><div className="parameter-field__meta"><strong>{binding.display_label}</strong>{reset}</div><EmbeddedPythonEditor tabId={tabId} binding={binding} value={String(value ?? '')} disabled={disabled} validateBinding={validateBinding} onCommit={(next) => onEdit(binding, next)} /></div>
  }

  if (binding.capabilities.includes('structured-sql')) {
    return <div className="parameter semantic-binding"><StructuredSqlEditor tabId={tabId} binding={binding} values={values} readOnly={disabled} inspect={inspectSql} runAction={runSqlAction} onReset={() => onEdit(binding, RESET_VALUE)} /></div>
  }

  const isFileBinding = binding.capabilities.includes('file-input') || binding.capabilities.includes('file-output')
  const body = isFileBinding && binding.value_schema?.kind === 'list'
    ? <FileListSelector label={binding.display_label} value={value} files={knownFiles} disabled={disabled} onChange={(next) => onEdit(binding, next)} onUpload={onUploadFile} />
    : isFileBinding
      ? <FileSelector label={binding.display_label} value={value} files={knownFiles} disabled={disabled} onChange={(next) => onEdit(binding, next)} />
    : binding.capabilities.includes('symbol-or-literal')
      ? <SymbolSelector label={binding.display_label} value={value} symbols={symbols} disabled={disabled} onChange={(next) => onEdit(binding, next)} />
      : binding.value_schema
        ? <SchemaValueField
            schema={binding.value_schema}
            value={value}
            onChange={(next, cleared) => onEdit(binding, next, cleared)}
            label={binding.display_label}
            parameterId={binding.id}
            path={[]}
            multiline={false}
            drafts={drafts}
            onDraft={onDraft}
          />
        : <input aria-label={binding.display_label} disabled={disabled} value={String(value ?? '')} onChange={(event) => onEdit(binding, event.target.value)} />

  return <div className={`parameter semantic-binding${disabled ? ' parameter--readonly' : ''}`}>
    <div className="parameter-field__meta"><strong>{binding.display_label}{binding.required ? ' *' : ''}</strong>{reset}</div>
    {binding.required ? body : <OptionalValue
      label={binding.display_label}
      enabled={value !== null && value !== undefined}
      disabled={disabled}
      onEnabledChange={(enabled) => onEdit(binding, enabled ? binding.value ?? binding.default ?? '' : null)}
    >{body}</OptionalValue>}
    {disabled && binding.read_only_reason && <small>{binding.read_only_reason}</small>}
    {binding.validation_state === 'unresolved' && <ValidationMessage message="This value does not resolve to a known symbol." />}
  </div>
}

function ConditionEditor({
  operation,
  values,
  symbols,
  onEdit,
}: {
  operation: SemanticOperationView
  values: Record<string, unknown>
  symbols: SymbolView[]
  onEdit: Props['onEdit']
}) {
  const bindings = useMemo(() => new Map(operation.bindings.map((binding) => [binding.name, binding])), [operation.bindings])
  const get = (name: string) => bindings.get(name)
  const value = (name: string) => {
    const binding = get(name)
    return binding ? effectiveBindingValue(values, binding) : null
  }
  const secondEnabled = ['conj', 'lhs2', 'op2', 'rhs2'].some((name) => value(name) !== null && value(name) !== undefined && value(name) !== '')

  const symbolField = (name: string) => {
    const binding = get(name)
    if (!binding) return null
    return <div className="parameter semantic-binding" key={binding.id}>
      <SymbolSelector label={binding.display_label} value={value(name)} symbols={symbols} disabled={!binding.editable} onChange={(next) => onEdit(binding, next)} />
      {binding.validation_state === 'unresolved' && <ValidationMessage message="Unknown symbol. Choose a known symbol or enter a literal value." />}
    </div>
  }

  const choiceField = (name: string) => {
    const binding = get(name)
    if (!binding) return null
    const choices = binding.value_schema?.choices ?? []
    return <label className="parameter semantic-binding" key={binding.id}>{binding.display_label}
      <select value={String(value(name) ?? '')} disabled={!binding.editable} onChange={(event) => onEdit(binding, event.target.value || null)}>
        {!binding.required && <option value="">Not set</option>}
        {choices.map((choice) => <option key={String(choice)} value={String(choice)}>{String(choice)}</option>)}
      </select>
    </label>
  }

  function setSecond(enabled: boolean) {
    for (const name of ['conj', 'lhs2', 'op2', 'rhs2']) {
      const binding = get(name)
      if (!binding) continue
      if (!enabled) onEdit(binding, null)
      else if (name === 'conj') onEdit(binding, 'AND')
      else if (name === 'op2') onEdit(binding, binding.value_schema?.choices[0] ?? null)
      else onEdit(binding, '')
    }
  }

  return <div className="condition-editor">
    <div className="condition-clause">{symbolField('lhs')}{choiceField('op')}{symbolField('rhs')}</div>
    <OptionalValue label="Add second condition" enabled={secondEnabled} onEnabledChange={setSecond}>
      <div className="condition-clause condition-clause--secondary">{choiceField('conj')}{symbolField('lhs2')}{choiceField('op2')}{symbolField('rhs2')}</div>
    </OptionalValue>
  </div>
}

function EmbeddedPythonEditor({
  tabId,
  binding,
  value,
  disabled,
  validateBinding,
  onCommit,
}: {
  tabId: string
  binding: SemanticBindingView
  value: string
  disabled: boolean
  validateBinding: Props['validateBinding']
  onCommit: (value: string) => void
}) {
  const dialogRef = useRef<HTMLDialogElement>(null)
  const [draft, setDraft] = useState(value)
  const [issues, setIssues] = useState<string[]>([])
  const [validating, setValidating] = useState(false)
  useEffect(() => setDraft(value), [value])

  async function save() {
    setValidating(true)
    try {
      const preview = await validateBinding(tabId, binding.id, draft)
      if (!preview.valid) {
        setIssues(preview.issues.map((issue) => issue.message))
        return
      }
      setIssues([])
      onCommit(draft)
      dialogRef.current?.close()
    } catch (error) {
      setIssues([error instanceof Error ? error.message : 'Could not validate embedded Python.'])
    } finally {
      setValidating(false)
    }
  }

  return <>
    <button type="button" className="field-command" disabled={disabled} onClick={() => { setIssues([]); setDraft(value); dialogRef.current?.showModal() }}><Code2 size={15} aria-hidden="true" />Edit Python</button>
    <dialog ref={dialogRef} className="embedded-python-dialog" aria-labelledby={`${binding.id}-title`}>
      <form method="dialog" className="dialog-card" onSubmit={(event) => event.preventDefault()}>
        <header><h3 id={`${binding.id}-title`}>Embedded Python</h3><p>Edit only the Python embedded in this source block. Generated application Python remains hidden.</p></header>
        <textarea aria-label="Embedded Python source" value={draft} onChange={(event) => setDraft(event.target.value)} rows={18} spellCheck={false} />
        {issues.map((issue, index) => <ValidationMessage key={index} message={issue} />)}
        <footer className="dialog-actions">
          <button type="button" onClick={() => dialogRef.current?.close()}>Cancel</button>
          <button type="button" className="primary-button" disabled={validating} onClick={() => void save()}>{validating ? 'Validating…' : 'Validate & Save'}</button>
        </footer>
      </form>
    </dialog>
  </>
}
