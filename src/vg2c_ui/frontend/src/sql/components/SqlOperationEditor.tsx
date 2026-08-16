import { useMemo, useState, type KeyboardEvent } from 'react'

import '../sqlEditor.css'

import type { DependencyDiagnostic } from '../../dependencyValidation'
import { formatOperationLabel } from '../../operationLabels'
import type { ParameterDescriptor, StepNode } from '../../types'
import {
  effectiveParameterValue,
  effectiveSql,
  effectiveStepFiles,
  sqlOperationParameters,
} from '../operation'
import { unavailableSqlMetadataProvider, type SqlMetadataProvider } from '../metadata'
import { parseSql } from '../parser'
import { displaySource, restoreSource } from '../presentation'
import { updateSource } from '../transform'
import { useSqlMetadata } from '../useSqlMetadata'
import { FiltersTab } from './FiltersTab'
import { JoinsTab } from './JoinsTab'
import { SelectedTab } from './SelectedTab'
import { SqlField } from './SqlField'

interface SqlOperationEditorProps {
  step: StepNode
  values: Record<string, unknown>
  diagnostics: DependencyDiagnostic[]
  onEdit: (parameter: ParameterDescriptor, value: unknown) => void
  metadataProvider?: SqlMetadataProvider
}

type SqlTab = 'selected' | 'filters' | 'joins'
const TABS: Array<{ id: SqlTab; label: string }> = [
  { id: 'selected', label: 'Selected' },
  { id: 'filters', label: 'Filters' },
  { id: 'joins', label: 'Joins' },
]

export function SqlOperationEditor({
  step,
  values,
  diagnostics,
  onEdit,
  metadataProvider = unavailableSqlMetadataProvider,
}: SqlOperationEditorProps) {
  const label = formatOperationLabel(step, values)
  const parameters = sqlOperationParameters(step)
  const sql = effectiveSql(step, values)
  const model = useMemo(() => parseSql(sql ?? ''), [sql])
  const metadata = useSqlMetadata(metadataProvider, model)
  const [tab, setTab] = useState<SqlTab>('selected')
  const [localError, setLocalError] = useState('')
  const files = effectiveStepFiles(step, values)
  const structurallyEditable = Boolean(
    sql
      && parameters.sql?.editable
      && !step.read_only
      && (model.capabilities.selected || model.capabilities.filters || model.capabilities.joins)
  )

  function updateSql(nextSql: string) {
    if (parameters.sql) onEdit(parameters.sql, nextSql)
  }

  return (
    <section className="operation-editor sql-operation-editor" aria-label={`Edit ${label.primary}`}>
      <header className="operation-editor__header">
        <div>
          <span className="eyebrow">Operation {step.block_index + 1}</span>
          <h3>{label.primary}</h3>
          {label.secondary && <p className="operation-secondary">{label.secondary}</p>}
        </div>
        <span className={`state-pill${structurallyEditable ? '' : ' state-pill--readonly'}`}>
          {structurallyEditable ? 'Structured SQL' : 'Read only'}
        </span>
      </header>

      {step.description && <p className="operation-description">{step.description}</p>}
      <SqlFileReferences
        step={step}
        values={values}
        files={files}
        model={model}
        metadata={metadata}
        onEdit={onEdit}
        onSqlChange={updateSql}
        onError={setLocalError}
      />

      {diagnostics.length > 0 && (
        <div className="operation-diagnostics" role="alert" aria-label="Dependency problems">
          {diagnostics.map((diagnostic) => (
            <p key={`${diagnostic.code}-${diagnostic.artifact}`}>
              <strong>{diagnostic.code.replaceAll('_', ' ')}</strong>
              <span>{diagnostic.message}</span>
            </p>
          ))}
        </div>
      )}

      {structurallyEditable ? (
        <>
          <div className="sql-tabs" role="tablist" aria-label="SQL modifiers" onKeyDown={(event) => handleTabKeys(event, tab, setTab)}>
            {TABS.map((item) => (
              <button
                id={`sql-tab-${item.id}`}
                key={item.id}
                type="button"
                role="tab"
                aria-selected={tab === item.id}
                aria-controls={`sql-panel-${item.id}`}
                tabIndex={tab === item.id ? 0 : -1}
                className={tab === item.id ? 'is-active' : ''}
                onClick={() => setTab(item.id)}
              >
                {item.label}
                <small>{item.id === 'selected' ? model.selections.length : item.id === 'filters' ? model.filters.length : model.joins.length}</small>
              </button>
            ))}
          </div>

          {localError && <p className="sql-edit-error" role="alert">{localError}</p>}
          {tab === 'selected' && <SelectedTab model={model} metadata={metadata} onSqlChange={updateSql} onError={setLocalError} />}
          {tab === 'filters' && <FiltersTab model={model} metadata={metadata} onSqlChange={updateSql} onError={setLocalError} />}
          {tab === 'joins' && <JoinsTab model={model} metadata={metadata} onSqlChange={updateSql} onError={setLocalError} />}
        </>
      ) : (
        <p className="read-only-note">
          {parameters.sql?.read_only_reason || model.readOnlyReason || 'This SQL is generated from a dynamic Python expression and cannot be modified structurally without changing its semantics.'}
        </p>
      )}

      <details className="raw-sql-details">
        <summary>Raw SQL</summary>
        {sql ? <pre className="raw-code raw-sql">{sql}</pre> : <pre className="raw-code raw-sql">{parameters.sql?.source ?? 'SQL source unavailable'}</pre>}
        <small>Raw SQL is read-only. Structured edits are reparsed before they enter the normal preview/apply workflow.</small>
      </details>

      <details className="generated-details">
        <summary>Generated information</summary>
        <dl>
          <div><dt>Function</dt><dd><code>{step.function_name}</code></dd></div>
          <div><dt>Source</dt><dd>lines {step.source_span.start_line}–{step.source_span.end_line}</dd></div>
        </dl>
      </details>
    </section>
  )
}

function SqlFileReferences({
  step,
  values,
  files,
  model,
  metadata,
  onEdit,
  onSqlChange,
  onError,
}: {
  step: StepNode
  values: Record<string, unknown>
  files: { inputs: string[]; outputs: string[] }
  model: ReturnType<typeof parseSql>
  metadata: ReturnType<typeof useSqlMetadata>
  onEdit: (parameter: ParameterDescriptor, value: unknown) => void
  onSqlChange: (sql: string) => void
  onError: (message: string) => void
}) {
  const parameters = sqlOperationParameters(step)
  const inputValue = effectiveParameterValue(parameters.inputs, values)
  const editableInputs = parameters.inputs?.editable && Array.isArray(inputValue)
    && inputValue.every((item) => typeof item === 'string') && !step.read_only
  const outputValue = effectiveParameterValue(parameters.output, values)
  const editableOutput = Boolean(parameters.output?.editable && typeof outputValue === 'string' && !step.read_only)

  return (
    <div className="sql-file-refs" aria-label="Query files">
      <div className="sql-file-ref sql-file-ref--output">
        <span>Output</span>
        {editableOutput && parameters.output ? (
          <SqlField
            className="sql-file-input"
            value={String(outputValue)}
            ariaLabel="Output filename"
            onCommit={(value) => {
              const trimmed = value.trim()
              if (!trimmed || /[\r\n]/.test(trimmed)) return false
              onEdit(parameters.output!, trimmed)
              return true
            }}
          />
        ) : (
          <code title={files.outputs.join(', ')}>{files.outputs.join(', ') || '—'}</code>
        )}
      </div>

      {model.sources.some((source) => source.kind === 'from') && (
        <div className="sql-file-ref sql-file-ref--sources">
          <span>From</span>
          <div className="sql-file-inputs">
            {model.sources.filter((source) => source.kind === 'from').map((source, index) => source.editable ? (
              <SqlField
                key={source.id}
                className="sql-file-input"
                value={displaySource(source.expression)}
                ariaLabel={`SQL source ${index + 1}`}
                list={metadata.sources.length ? 'sql-source-options' : undefined}
                onCommit={(value) => {
                  try {
                    onSqlChange(updateSource(model.source, source.id, restoreSource(value, source.expression)).sql)
                    onError('')
                    return true
                  } catch (error) {
                    onError(error instanceof Error ? error.message : 'Could not update SQL source.')
                    return false
                  }
                }}
              />
            ) : (
              <code key={source.id} title={source.readOnlyReason ?? source.expression}>{source.expression}</code>
            ))}
          </div>
          {metadata.sources.length > 0 && (
            <datalist id="sql-source-options">
              {metadata.sources.map((option) => <option key={option.source} value={displaySource(option.source)}>{displaySource(option.source)}</option>)}
            </datalist>
          )}
        </div>
      )}

      <div className="sql-file-ref sql-file-ref--inputs">
        <span>Inputs</span>
        <div className="sql-file-inputs">
          {files.inputs.length ? files.inputs.map((path, index) => (
            editableInputs && parameters.inputs ? (
              <SqlField
                key={`${index}-${path}`}
                className="sql-file-input"
                value={path}
                ariaLabel={`Input filename ${index + 1}`}
                onCommit={(value) => {
                  const trimmed = value.trim()
                  if (!trimmed || /[\r\n]/.test(trimmed)) return false
                  const next = [...(inputValue as string[])]
                  next[index] = trimmed
                  onEdit(parameters.inputs!, next)
                  return true
                }}
              />
            ) : <code key={`${index}-${path}`} title={path}>{path}</code>
          )) : <span className="sql-file-empty">No local file inputs</span>}
        </div>
      </div>
    </div>
  )
}

function handleTabKeys(
  event: KeyboardEvent<HTMLDivElement>,
  active: SqlTab,
  setActive: (tab: SqlTab) => void,
) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  const index = TABS.findIndex((item) => item.id === active)
  const nextIndex = event.key === 'Home'
    ? 0
    : event.key === 'End'
      ? TABS.length - 1
      : event.key === 'ArrowLeft'
        ? (index - 1 + TABS.length) % TABS.length
        : (index + 1) % TABS.length
  setActive(TABS[nextIndex].id)
  requestAnimationFrame(() => document.getElementById(`sql-tab-${TABS[nextIndex].id}`)?.focus())
}
