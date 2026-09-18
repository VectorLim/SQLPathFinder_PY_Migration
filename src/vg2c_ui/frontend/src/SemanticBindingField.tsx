import { RotateCcw } from 'lucide-react'

import type {
  FileResourceView,
  SemanticBindingView,
  SqlActionRequest,
  SqlModelView,
  SymbolView,
  ValueSchemaView,
} from './contracts.generated'
import { EmbeddedPythonEditor } from './EmbeddedPythonEditor'
import { SchemaValueField, type FieldDraftProps } from './ParameterField'
import { FileSelector, OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { StructuredSqlEditor } from './StructuredSqlEditor'
import { effectiveBindingValue, RESET_VALUE, type FieldPath } from './workspaceState'

interface Props extends FieldDraftProps {
  tabId: string
  binding: SemanticBindingView
  values: Record<string, unknown>
  saving: boolean
  files: FileResourceView[]
  symbols: SymbolView[]
  onEdit: (binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => void
  validateCandidate: (tabId: string, bindingId: string, value: unknown) => Promise<{
    valid: boolean
    diff: string
    issues: Array<{ code: string; message: string }>
  }>
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
  saving,
  files,
  symbols,
  onEdit,
  validateCandidate,
  inspectSql,
  runSqlAction,
  drafts,
  onDraft,
}: Props) {
  const value = effectiveBindingValue(values, binding)
  const disabled = saving || !binding.editable
  const schema = binding.value_schema ?? dynamicSchema
  const validationMessage = binding.read_only_reason
    ?? (binding.validation_state === 'unresolved' ? 'This value does not resolve to a known symbol.' : null)
    ?? (binding.validation_state === 'unsupported' ? 'This value cannot be edited safely.' : null)

  if (binding.capabilities.includes('structured-sql')) {
    return <fieldset className="parameter semantic-binding">
      <legend>{binding.display_label}</legend>
      <StructuredSqlEditor
        tabId={tabId}
        binding={binding}
        values={values}
        readOnly={disabled}
        inspect={inspectSql}
        runAction={runSqlAction}
        onReset={() => onEdit(binding, RESET_VALUE)}
      />
      <ValidationMessage message={validationMessage} />
    </fieldset>
  }

  if (binding.capabilities.includes('embedded-python')) {
    return <fieldset className="parameter semantic-binding">
      <legend>{binding.display_label}</legend>
      <EmbeddedPythonEditor
        tabId={tabId}
        binding={binding}
        value={value}
        disabled={disabled}
        validate={validateCandidate}
        onChange={(next) => onEdit(binding, next)}
      />
      <ValidationMessage message={validationMessage} />
    </fieldset>
  }

  const editor = binding.capabilities.some((capability) => capability === 'file-input' || capability === 'file-output')
    && schema.kind === 'string'
    ? <FileSelector
        label={binding.display_label}
        value={value}
        files={files.flatMap((file) => file.path ? [file.path] : [])}
        disabled={disabled}
        onChange={(next) => onEdit(binding, next)}
      />
    : binding.capabilities.includes('symbol-or-literal') && schema.kind === 'string'
      ? <SymbolSelector
          label={binding.display_label}
          value={value}
          symbols={symbols}
          disabled={disabled}
          onChange={(next) => onEdit(binding, next)}
        />
      : <SchemaValueField
          schema={schema}
          value={value}
          onChange={(next, cleared) => onEdit(binding, next, cleared)}
          label={binding.display_label}
          parameterId={binding.id}
          path={[]}
          drafts={drafts}
          onDraft={onDraft}
        />

  const body = schema.nullable
    ? <OptionalValue
        label={binding.display_label}
        enabled={value !== null}
        disabled={disabled}
        onEnabledChange={(enabled) => onEdit(binding, enabled ? defaultFor({ ...schema, nullable: false }) : null)}
      >
        {value !== null ? renderNonNullable(editor) : null}
      </OptionalValue>
    : editor

  return <fieldset className={`parameter semantic-binding${disabled ? ' parameter--readonly' : ''}`} disabled={saving}>
    <legend>{schema.nullable ? '' : binding.display_label}{binding.required ? ' *' : ''}</legend>
    <div className="parameter-field__meta">
      <small>{Object.hasOwn(values, binding.id) ? 'Override' : 'Current value'}</small>
      <button
        type="button"
        className="icon-button"
        disabled={disabled || !binding.resettable || !Object.hasOwn(values, binding.id)}
        aria-label={`Reset ${binding.display_label}`}
        title="Reset to generated/default value"
        onClick={() => onEdit(binding, RESET_VALUE)}
      >
        <RotateCcw size={14} aria-hidden="true" />
      </button>
    </div>
    {body}
    <ValidationMessage message={validationMessage} />
  </fieldset>
}

function renderNonNullable(editor: JSX.Element): JSX.Element {
  return editor
}

const dynamicSchema: ValueSchemaView = {
  kind: 'dynamic',
  nullable: false,
  choices: [],
  items: null,
  properties: {},
  required_keys: [],
  variants: [],
  path: false,
  prefix_items: [],
  tuple_value: false,
}

function defaultFor(schema: ValueSchemaView): unknown {
  if (schema.choices.length) return schema.choices[0]
  if (schema.kind === 'boolean') return false
  if (schema.kind === 'integer' || schema.kind === 'number') return 0
  if (schema.kind === 'list') return schema.tuple_value ? schema.prefix_items.map(defaultFor) : []
  if (schema.kind === 'object') return Object.fromEntries(schema.required_keys.map((key) => [key, defaultFor(schema.properties[key])]))
  if (schema.kind === 'union') return schema.variants[0] ? defaultFor(schema.variants[0]) : ''
  return ''
}
