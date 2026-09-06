import type { ReactElement } from 'react'
import type {
  DependencyIssueView,
  ParameterView,
  SqlActionRequest,
  SqlModelView,
  StepView,
} from './contracts.generated'
import { formatOperationLabel } from './operationLabels'
import { StructuredSqlEditor } from './StructuredSqlEditor'

interface Props {
  tabId: string
  step: StepView
  values: Record<string, unknown>
  files: { inputs: string[]; outputs: string[] }
  diagnostics?: DependencyIssueView[]
  onEdit: (parameter: ParameterView, value: unknown) => void
  inspectSql: (tabId: string, parameterId: string) => Promise<SqlModelView>
  runSqlAction: (
    tabId: string,
    parameterId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

type CustomEditor = (props: Props) => ReactElement
const CAPABILITY_EDITORS: Record<string, CustomEditor> = {
  'structured-sql': (props) => <StructuredSqlEditor tabId={props.tabId} step={props.step} values={props.values} files={props.files} diagnostics={props.diagnostics ?? []} inspect={props.inspectSql} runAction={props.runSqlAction} />,
}

export function OperationEditor(props: Props) {
  const capability = props.step.capabilities.find((item) => CAPABILITY_EDITORS[item])
  if (capability) {
    const Editor = CAPABILITY_EDITORS[capability]
    return <Editor {...props} />
  }
  return <GenericOperationEditor {...props} />
}

function GenericOperationEditor({ step, values, files, diagnostics = [], onEdit }: Props) {
  const label = formatOperationLabel(step)
  const editableCount = step.parameters.filter((parameter) => parameter.editable && !step.read_only).length
  return (
    <section className="operation-editor" aria-label={`Edit ${label.primary}`}>
      <header className="operation-editor__header">
        <div><span className="eyebrow">Operation {step.block_index + 1}</span><h3>{label.primary}</h3>{label.secondary && <p className="operation-secondary">{label.secondary}</p>}</div>
        <span className={`state-pill${step.read_only ? ' state-pill--readonly' : ''}`}>{step.read_only ? 'Read only' : `${editableCount} editable`}</span>
      </header>
      {step.description && <p className="operation-description">{step.description}</p>}
      {diagnostics.length > 0 && <div className="operation-diagnostics" role="alert">{diagnostics.map((item) => <p key={`${item.code}-${item.artifact}`}><strong>{item.code.replaceAll('_', ' ')}</strong><span>{item.message}</span></p>)}</div>}
      <div className="parameter-grid">
        {step.parameters.length ? step.parameters.map((parameter) => <ParameterEditor
          key={parameter.id}
          parameter={parameter}
          value={Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter.value}
          disabled={step.read_only || !parameter.editable}
          onChange={(value) => onEdit(parameter, value)}
        />) : <p className="empty-copy">No configurable values are exposed by this utility.</p>}
      </div>
      {(files.inputs.length > 0 || files.outputs.length > 0) && <div className="operation-io">{files.inputs.length > 0 && <FileChips label="Reads" paths={files.inputs} />}{files.outputs.length > 0 && <FileChips label="Produces" paths={files.outputs} />}</div>}
      {step.validation_state === 'unsupported' && <p className="read-only-note">This operation is not safely editable.</p>}
      <details className="generated-details"><summary>Generated information</summary><dl>
        <div><dt>Function</dt><dd><code>{step.function_name}</code></dd></div>
        <div><dt>Utility</dt><dd>{step.utility.module}.{step.utility.class_name}</dd></div>
        {step.utility.method && <div><dt>Method</dt><dd><code>{step.utility.method}</code></dd></div>}
        <div><dt>Source</dt><dd>lines {step.source_span.start_line}–{step.source_span.end_line}</dd></div>
      </dl>{step.raw_code && <pre className="raw-code">{step.raw_code}</pre>}</details>
    </section>
  )
}

function ParameterEditor({ parameter, value, disabled, onChange }: { parameter: ParameterView; value: unknown; disabled: boolean; onChange: (value: unknown) => void }) {
  const choices = Array.isArray(parameter.constraints.choices) ? parameter.constraints.choices as unknown[] : null
  const id = `parameter-${parameter.id.replace(/[^A-Za-z0-9_-]/g, '-')}`
  return <label className={`parameter${disabled ? ' parameter--readonly' : ''}`} htmlFor={id}>
    <span className="parameter__label">{humanize(parameter.name)}{parameter.required && <span aria-label="required"> *</span>}</span>
    {choices ? <select id={id} disabled={disabled} value={String(value ?? '')} onChange={(event) => onChange(choices.find((choice) => String(choice) === event.target.value))}>{choices.map((choice) => <option key={String(choice)} value={String(choice)}>{String(choice)}</option>)}</select>
      : parameter.editor_type === 'boolean' ? <span className="checkbox-field"><input id={id} type="checkbox" disabled={disabled} checked={Boolean(value)} onChange={(event) => onChange(event.target.checked)} /><span>{Boolean(value) ? 'Enabled' : 'Disabled'}</span></span>
      : parameter.editor_type === 'multiline' || parameter.editor_type === 'list' ? <textarea id={id} disabled={disabled} rows={parameter.editor_type === 'multiline' ? 6 : 4} value={formatValue(value, parameter.source)} onChange={(event) => {
        if (parameter.editor_type !== 'list') return onChange(event.target.value)
        try { onChange(JSON.parse(event.target.value)) } catch { onChange(event.target.value) }
      }} />
      : <input id={id} disabled={disabled} type={parameter.editor_type === 'integer' ? 'number' : 'text'} value={formatValue(value, parameter.source)} onChange={(event) => onChange(parameter.editor_type === 'integer' ? Number(event.target.value) : event.target.value)} />}
    {(parameter.read_only_reason || parameter.annotation) && <small>{parameter.read_only_reason || parameter.annotation}</small>}
  </label>
}

function FileChips({ label, paths }: { label: string; paths: string[] }) { return <div className="file-chip-row"><strong>{label}</strong><div>{paths.map((path) => <code key={path} title={path}>{path}</code>)}</div></div> }
function formatValue(value: unknown, source: string): string { if (value === null || value === undefined) return source; return typeof value === 'string' ? value : JSON.stringify(value, null, 2) }
function humanize(value: string): string { return value.replaceAll('_', ' ').replace(/(^|\s)\S/g, (match) => match.toUpperCase()) }
