import type {
  DependencyIssueView,
  ParameterView,
  OperationView,
  SqlActionRequest,
  SqlModelView,
  StepView,
} from './contracts.generated'
import { formatOperationLabel } from './operationLabels'
import { OperationDiagnostics } from './OperationPresentation'
import { ParameterField, type FieldDraftProps } from './ParameterField'
import { StructuredSqlEditor } from './StructuredSqlEditor'
import { RESET_VALUE, type FieldPath } from './workspaceState'

interface Props extends FieldDraftProps {
  tabId: string
  step: StepView
  operation: OperationView
  values: Record<string, unknown>
  saving: boolean
  diagnostics?: DependencyIssueView[]
  onEdit: (parameter: ParameterView, value: unknown, clearDraftPaths?: FieldPath[]) => void
  inspectSql: (tabId: string, parameterId: string) => Promise<SqlModelView>
  runSqlAction: (
    tabId: string,
    parameterId: string,
    action: SqlActionRequest['action'],
    args: Record<string, unknown>,
  ) => Promise<SqlModelView>
}

export function OperationEditor({ tabId, step, operation, values, saving, diagnostics = [], onEdit, inspectSql, runSqlAction, drafts, onDraft }: Props) {
  const label = formatOperationLabel(step, operation)
  const editableCount = operation.parameters.filter((parameter) => parameter.editable && !step.read_only).length
  return (
    <section className="operation-editor" aria-label={`Edit ${label.primary}`}>
      <header className="operation-editor__header">
        <div><span className="eyebrow">Operation {step.block_index + 1}</span><h3>{label.primary}</h3>{label.secondary && <p className="operation-secondary">{label.secondary}</p>}</div>
        <span className={`state-pill${step.read_only ? ' state-pill--readonly' : ''}`}>{step.read_only ? 'Read only' : `${editableCount} editable`}</span>
      </header>
      {step.description && <p className="operation-description">{step.description}</p>}
      <OperationDiagnostics diagnostics={diagnostics} />
      <fieldset className="parameter-grid" disabled={saving} aria-busy={saving}>
        {operation.parameters.filter((parameter) => !parameter.internal).map((parameter) => parameter.capabilities.includes('structured-sql') && parameter.editable && !step.read_only
          ? <StructuredSqlEditor key={parameter.id} tabId={tabId} parameter={parameter} values={values} readOnly={step.read_only} inspect={inspectSql} runAction={runSqlAction} onReset={() => onEdit(parameter, RESET_VALUE)} />
          : <ParameterField key={parameter.id} parameter={parameter} values={values} disabled={step.read_only || !parameter.editable} onChange={(value, cleared) => onEdit(parameter, value, cleared)} drafts={drafts} onDraft={onDraft} />)}
        {!operation.parameters.some((parameter) => !parameter.internal) && <p className="empty-copy">No configurable values.</p>}
      </fieldset>
      {step.validation_state === 'unsupported' && <p className="read-only-note">This operation is not safely editable.</p>}
      <details className="generated-details"><summary>Generated information</summary><dl>
        <div><dt>Function</dt><dd><code>{step.function_name}</code></dd></div>
        <div><dt>Utility</dt><dd>{operation.utility.module}.{operation.utility.class_name}</dd></div>
        {operation.utility.method && <div><dt>Method</dt><dd><code>{operation.utility.method}</code></dd></div>}
        <div><dt>Source</dt><dd>lines {step.source_span.start_line}–{step.source_span.end_line}</dd></div>
      </dl>{operation.parameters.filter((parameter) => parameter.internal).map((parameter) => <div key={parameter.id}><strong>{parameter.name}</strong><pre className="parameter-source">{parameter.source}</pre><small>{parameter.read_only_reason}</small></div>)}{step.raw_code && <pre className="raw-code">{step.raw_code}</pre>}</details>
    </section>
  )
}
