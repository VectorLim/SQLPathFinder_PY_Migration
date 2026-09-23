import { RotateCcw } from 'lucide-react'

import type { SemanticBindingView, ValueSchemaView } from './api/contracts.generated'
import { EmbeddedPythonEditor } from './EmbeddedPythonEditor'
import { defaultSchemaValue, SchemaValueField } from './SchemaValueField'
import type { SemanticEditorSession } from './semanticEditorSession'
import { FileListSelector, FileSelector, OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { StructuredSqlEditor } from './StructuredSqlEditor'
import { effectiveBindingValue, RESET_VALUE } from './workspace/state'

interface Props {
  binding: SemanticBindingView
  readOnly: boolean
  session: SemanticEditorSession
}

export function SemanticBindingField({ binding, readOnly, session }: Props) {
  const { values, resources, drafts, actions } = session
  const value = effectiveBindingValue(values, binding)
  const disabled = readOnly || !binding.editable
  const reset = binding.resettable && Object.hasOwn(values, binding.id)
    ? <button type="button" className="icon-button" aria-label={`Reset ${binding.display_label}`} title="Reset value" onClick={() => actions.edit({ binding, value: RESET_VALUE })}><RotateCcw size={14} aria-hidden="true" /></button>
    : null

  if (binding.capabilities.includes('embedded-python')) {
    return <div className="parameter semantic-binding">
      <div className="parameter-field__meta"><strong>{binding.display_label}</strong>{reset}</div>
      <EmbeddedPythonEditor
        binding={binding}
        value={String(value ?? '')}
        disabled={disabled}
        validateBinding={actions.validateBinding}
        onCommit={(next) => actions.edit({ binding, value: next })}
      />
    </div>
  }

  if (binding.capabilities.includes('structured-sql')) {
    return <div className="parameter semantic-binding">
      <StructuredSqlEditor
        binding={binding}
        values={values}
        readOnly={disabled}
        fileListReadOnly={readOnly || session.readOnly}
        inspect={actions.inspectSql}
        runCommand={actions.runSqlCommand}
        onReset={() => actions.edit({ binding, value: RESET_VALUE })}
      />
    </div>
  }

  const schema = binding.value_schema
  const nullable = Boolean(schema?.nullable)
  const controlSchema = nullable && schema ? { ...schema, nullable: false } : schema
  const body = renderBindingControl({
    binding,
    value,
    disabled,
    schema: controlSchema,
    symbols: resources.symbols,
    onUploadFile: actions.uploadFile,
    onEdit: actions.edit,
    drafts: drafts.values,
    onDraft: drafts.update,
  })

  return <fieldset className={`parameter semantic-binding${disabled ? ' parameter--readonly' : ''}`} disabled={disabled}>
    {nullable
      ? <OptionalValue
          label={`${binding.display_label}${binding.required ? ' *' : ''}`}
          enabled={value !== null && value !== undefined}
          disabled={disabled}
          action={reset}
          onEnabledChange={(enabled) => actions.edit({ binding, value: enabled ? enabledValue(binding, schema) : null })}
        >{body}</OptionalValue>
      : <>
          <div className="parameter-field__meta"><strong>{binding.display_label}{binding.required ? ' *' : ''}</strong>{reset}</div>
          {body}
        </>}
    {disabled && binding.read_only_reason && <small>{binding.read_only_reason}</small>}
    {binding.validation_state === 'unresolved' && <ValidationMessage message="This value does not resolve to a known symbol." />}
  </fieldset>
}

function renderBindingControl({
  binding,
  value,
  disabled,
  schema,
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
  symbols: SemanticEditorSession['resources']['symbols']
  onUploadFile: SemanticEditorSession['actions']['uploadFile']
  onEdit: SemanticEditorSession['actions']['edit']
  drafts: SemanticEditorSession['drafts']['values']
  onDraft: SemanticEditorSession['drafts']['update']
}) {
  const isFileBinding = binding.capabilities.includes('file-input') || binding.capabilities.includes('file-output')
  if (isFileBinding && schema?.kind === 'list') {
    return <FileListSelector label={binding.display_label} showLabel={false} value={value} files={binding.file_choices} disabled={disabled} onChange={(next) => onEdit({ binding, value: next })} onUpload={onUploadFile} />
  }
  if (isFileBinding) {
    return <FileSelector label={binding.display_label} showLabel={false} value={value} files={binding.file_choices} disabled={disabled} onChange={(next) => onEdit({ binding, value: next })} />
  }
  if (binding.capabilities.includes('symbol-or-literal')) {
    return <SymbolSelector label={binding.display_label} showLabel={false} value={value} symbolId={binding.symbol_id} symbols={symbols} disabled={disabled} onChange={(next) => onEdit({ binding, value: next })} />
  }
  if (schema) {
    return <SchemaValueField
      schema={schema}
      value={value}
      onChange={(next, clearDraftPaths) => onEdit({ binding, value: next, clearDraftPaths })}
      label={binding.display_label}
      bindingId={binding.id}
      path={[]}
      multiline={binding.capabilities.includes('multiline')}
      drafts={drafts}
      onDraft={onDraft}
    />
  }
  return <input aria-label={binding.display_label} disabled={disabled} value={String(value ?? '')} onChange={(event) => onEdit({ binding, value: event.target.value })} />
}

function enabledValue(binding: SemanticBindingView, schema: ValueSchemaView | null): unknown {
  if (binding.value !== null && binding.value !== undefined) return binding.value
  if (binding.default !== null && binding.default !== undefined) return binding.default
  if (!schema) return ''
  return defaultSchemaValue({ ...schema, nullable: false })
}
