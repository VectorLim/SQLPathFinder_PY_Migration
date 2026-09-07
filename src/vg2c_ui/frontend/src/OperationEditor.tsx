import type { DependencyDiagnostic } from './dependencyValidation'
import { formatOperationLabel } from './operationLabels'
import { isSqlOperation } from './sql/operation'
import type { SqlMetadataProvider } from './sql/metadata'
import { SqlOperationEditor } from './sql/components/SqlOperationEditor'
import type { ParameterDescriptor, StepNode } from './types'

interface OperationEditorProps {
  step: StepNode
  values: Record<string, unknown>
  diagnostics?: DependencyDiagnostic[]
  onEdit: (parameter: ParameterDescriptor, value: unknown) => void
  sqlMetadataProvider?: SqlMetadataProvider
}

export function OperationEditor({
  step,
  values,
  diagnostics = [],
  onEdit,
  sqlMetadataProvider,
}: OperationEditorProps) {
  if (isSqlOperation(step)) {
    return (
      <SqlOperationEditor
        step={step}
        values={values}
        diagnostics={diagnostics}
        onEdit={onEdit}
        metadataProvider={sqlMetadataProvider}
      />
    )
  }

  const label = formatOperationLabel(step, values)
  const editableCount = step.parameters.filter((parameter) => parameter.editable && !step.read_only).length

  return (
    <section className="operation-editor" aria-label={`Edit ${label.primary}`}>
      <header className="operation-editor__header">
        <div>
          <span className="eyebrow">Operation {step.block_index + 1}</span>
          <h3>{label.primary}</h3>
          {label.secondary && <p className="operation-secondary">{label.secondary}</p>}
        </div>
        <span className={`state-pill${step.read_only ? ' state-pill--readonly' : ''}`}>
          {step.read_only ? 'Read only' : `${editableCount} editable`}
        </span>
      </header>

      {step.description && <p className="operation-description">{step.description}</p>}

      {diagnostics.length > 0 && (
        <div className="operation-diagnostics" role="alert">
          {diagnostics.map((diagnostic) => (
            <p key={`${diagnostic.code}-${diagnostic.artifact}`}><strong>{diagnostic.code.replaceAll('_', ' ')}</strong><span>{diagnostic.message}</span></p>
          ))}
        </div>
      )}

      <div className="parameter-grid">
        {step.parameters.length ? step.parameters.map((parameter) => (
          <ParameterEditor
            key={parameter.id}
            parameter={parameter}
            value={Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter.value}
            disabled={step.read_only || !parameter.editable}
            onChange={(value) => onEdit(parameter, value)}
          />
        )) : <p className="empty-copy">No configurable values were detected for this operation.</p>}
      </div>

      {(step.csv_inputs.length > 0 || step.csv_outputs.length > 0) && (
        <div className="operation-io" aria-label="Operation data files">
          {step.csv_inputs.length > 0 && <FileChips label="Reads" paths={step.csv_inputs} />}
          {step.csv_outputs.length > 0 && <FileChips label="Produces" paths={step.csv_outputs} />}
        </div>
      )}

      {step.validation_state === 'unsupported' && (
        <p className="read-only-note">This generated operation cannot be edited safely and is shown read-only.</p>
      )}

      <details className="generated-details">
        <summary>Generated information</summary>
        <dl>
          <div><dt>Function</dt><dd><code>{step.function_name}</code></dd></div>
          <div><dt>Utility</dt><dd>{step.utility.module}.{step.utility.class_name}</dd></div>
          {step.utility.method && <div><dt>Method</dt><dd><code>{step.utility.method}</code></dd></div>}
          <div>
            <dt>Source</dt>
            <dd>lines {step.source_span.start_line}–{step.source_span.end_line}</dd>
          </div>
          {step.utility.return_type && <div><dt>Returns</dt><dd>{step.utility.return_type}</dd></div>}
        </dl>
        {step.raw_code && <pre className="raw-code">{step.raw_code}</pre>}
      </details>
    </section>
  )
}

function ParameterEditor({ parameter, value, disabled, onChange }: {
  parameter: ParameterDescriptor
  value: unknown
  disabled: boolean
  onChange: (value: unknown) => void
}) {
  const choices = Array.isArray(parameter.constraints.choices)
    ? parameter.constraints.choices as unknown[]
    : null
  const help = parameter.read_only_reason || parameter.annotation
  const id = `parameter-${parameter.id}`

  return (
    <label className={`parameter${disabled ? ' parameter--readonly' : ''}`} htmlFor={id}>
      <span className="parameter__label">
        {humanizeParameter(parameter.name)}
        {parameter.required && <span aria-label="required"> *</span>}
      </span>
      {choices ? (
        <select
          id={id}
          disabled={disabled}
          value={String(value ?? '')}
          onChange={(event) => {
            const selected = choices.find((choice) => String(choice) === event.target.value)
            onChange(selected)
          }}
        >
          {choices.map((choice) => <option key={String(choice)} value={String(choice)}>{String(choice)}</option>)}
        </select>
      ) : parameter.editor_type === 'boolean' ? (
        <span className="checkbox-field">
          <input
            id={id}
            type="checkbox"
            disabled={disabled}
            checked={Boolean(value)}
            onChange={(event) => onChange(event.target.checked)}
          />
          <span>{Boolean(value) ? 'Enabled' : 'Disabled'}</span>
        </span>
      ) : parameter.editor_type === 'multiline' || parameter.editor_type === 'list' ? (
        <textarea
          id={id}
          disabled={disabled}
          rows={parameter.editor_type === 'multiline' ? 6 : 4}
          value={formatValue(value, parameter.source)}
          onChange={(event) => {
            if (parameter.editor_type !== 'list') {
              onChange(event.target.value)
              return
            }
            try {
              onChange(JSON.parse(event.target.value))
            } catch {
              onChange(event.target.value)
            }
          }}
        />
      ) : (
        <input
          id={id}
          disabled={disabled}
          type={parameter.editor_type === 'integer' ? 'number' : 'text'}
          value={formatValue(value, parameter.source)}
          onChange={(event) => onChange(
            parameter.editor_type === 'integer'
              ? Number(event.target.value)
              : event.target.value,
          )}
        />
      )}
      {help && <small>{help}</small>}
    </label>
  )
}

function FileChips({ label, paths }: { label: string; paths: string[] }) {
  return (
    <div className="file-chip-row">
      <strong>{label}</strong>
      <div>{paths.map((path) => <code key={path} title={path}>{path}</code>)}</div>
    </div>
  )
}

function formatValue(value: unknown, source: string): string {
  if (value === null || value === undefined) return source
  return typeof value === 'string' ? value : JSON.stringify(value, null, 2)
}

function humanizeParameter(value: string): string {
  return value.replaceAll('_', ' ').replace(/(^|\s)\S/g, (match) => match.toLocaleUpperCase())
}
