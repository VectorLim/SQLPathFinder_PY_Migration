import type { ScopeSummary } from './graph'
import type { CsvPreview, ParameterDescriptor, WorkflowNode } from './types'

interface InspectorProps {
  item: WorkflowNode | null
  summary?: ScopeSummary
  values: Record<string, unknown>
  preview: { valid: boolean; diff: string; issues: Array<{ code: string; message: string }> } | null
  csv: CsvPreview | null
  onEdit: (parameter: ParameterDescriptor, value: unknown) => void
  onPreviewCsv: (path: string) => void
}

export function Inspector({ item, summary, values, preview, csv, onEdit, onPreviewCsv }: InspectorProps) {
  return (
    <aside className="inspector" aria-label="Selection inspector">
      <h2>Inspector</h2>
      {!item && <p className="empty-copy">Select a block to inspect its details.</p>}
      {item?.node_kind === 'step' && (
        <>
          <span className="eyebrow">Step {item.block_index + 1}</span>
          <h3>{item.display_label}</h3>
          <p>{item.description}</p>
          <section className="utility-card">
            <strong>{item.utility.title}</strong>
            <small>{item.utility.module}.{item.utility.class_name}</small>
            <p>{item.utility.method_description || item.utility.description}</p>
            {item.utility.return_type && <small>Returns {item.utility.return_type}</small>}
            {item.utility.fallback && <small className="warning-text">Generic fallback metadata</small>}
          </section>
          <h4>Parameters</h4>
          {item.parameters.length ? item.parameters.map((parameter) => (
            <ParameterEditor
              key={parameter.id}
              parameter={parameter}
              value={Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter.value}
              disabled={item.read_only || !parameter.editable}
              onChange={(value) => onEdit(parameter, value)}
            />
          )) : <p className="empty-copy">No literal parameters detected.</p>}
          {(item.csv_inputs.length > 0 || item.csv_outputs.length > 0) && (
            <section><h4>Data</h4><p>{[...item.csv_inputs, ...item.csv_outputs].join(', ')}</p></section>
          )}
          {item.validation_state === 'unsupported' && (
            <p className="read-only-note">Unsupported code is shown read-only.</p>
          )}
        </>
      )}
      {item?.node_kind === 'csv-artifact' && (
        <>
          <h3>{item.label}</h3>
          <p>{item.path}</p>
          {(item.conditional || item.in_loop) && (
            <p className="read-only-note">
              This output is {item.conditional ? 'conditional' : 'produced in a loop'}.
            </p>
          )}
          <button className="secondary-button" type="button" onClick={() => onPreviewCsv(item.path)}>
            Preview CSV
          </button>
          {csv && <CsvTable preview={csv} />}
        </>
      )}
      {item && item.node_kind !== 'step' && item.node_kind !== 'csv-artifact' && (
        <>
          <span className="eyebrow">{item.node_kind}</span>
          <h3>{item.label}</h3>
          <p>Blocks {item.start_index + 1}–{item.end_index + 1}</p>
          {summary && (
            <section>
              <p>{summary.stepCount} nested step{summary.stepCount === 1 ? '' : 's'}. Click the group on the canvas to {summary.expanded ? 'collapse' : 'expand'} it.</p>
              {summary.csvInputs.length > 0 && <><h4>Inputs</h4><p>{summary.csvInputs.join(', ')}</p></>}
              {summary.csvOutputs.length > 0 && <><h4>Outputs</h4><p>{summary.csvOutputs.join(', ')}</p></>}
            </section>
          )}
        </>
      )}
      {preview && (
        <section className="diff-preview" aria-label="Python diff preview">
          <h4>Proposed Python diff</h4>
          {preview.issues.map((issue) => <p className="validation-error" key={`${issue.code}-${issue.message}`}>{issue.message}</p>)}
          <pre>{preview.diff || 'No textual change.'}</pre>
        </section>
      )}
    </aside>
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
  const help = parameter.annotation
    ? `${parameter.annotation}${parameter.required ? ', required' : ''}`
    : parameter.read_only_reason
  return (
    <label className="parameter">
      {parameter.name}
      {choices ? (
        <select disabled={disabled} value={String(value ?? '')} onChange={(event) => {
          const selected = choices.find((choice) => String(choice) === event.target.value)
          onChange(selected)
        }}>
          {choices.map((choice) => <option key={String(choice)}>{String(choice)}</option>)}
        </select>
      ) : parameter.editor_type === 'boolean' ? (
        <input type="checkbox" disabled={disabled} checked={Boolean(value)} onChange={(event) => onChange(event.target.checked)} />
      ) : parameter.editor_type === 'multiline' || parameter.editor_type === 'list' ? (
        <textarea disabled={disabled} rows={4} value={formatValue(value, parameter.source)} onChange={(event) => {
          if (parameter.editor_type !== 'list') onChange(event.target.value)
          else {
            try { onChange(JSON.parse(event.target.value)) } catch { onChange(event.target.value) }
          }
        }} />
      ) : (
        <input
          disabled={disabled}
          type={parameter.editor_type === 'integer' ? 'number' : 'text'}
          value={formatValue(value, parameter.source)}
          onChange={(event) => onChange(parameter.editor_type === 'integer' ? Number(event.target.value) : event.target.value)}
        />
      )}
      {help && <small>{help}</small>}
    </label>
  )
}

function CsvTable({ preview }: { preview: CsvPreview }) {
  return (
    <div className="csv-preview">
      <small>{preview.size_bytes.toLocaleString()} bytes{preview.truncated ? ' · truncated' : ''}</small>
      <div className="table-scroll"><table>
        <thead><tr>{preview.columns.map((column, index) => <th key={`${column}-${index}`}>{column}</th>)}</tr></thead>
        <tbody>{preview.rows.map((row, rowIndex) => (
          <tr key={rowIndex}>{row.map((cell, index) => <td key={index}>{cell}</td>)}</tr>
        ))}</tbody>
      </table></div>
    </div>
  )
}

function formatValue(value: unknown, source: string): string {
  if (value === null || value === undefined) return source
  return typeof value === 'string' ? value : JSON.stringify(value, null, 2)
}
