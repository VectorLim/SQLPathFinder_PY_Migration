import { useState } from 'react'

import type { SqlMetadataSnapshot } from '../useSqlMetadata'
import type { SqlEditableModel, SqlLogicalConnector } from '../model'
import { expressionSourceLabels, presentMetadataExpression } from '../presentation'
import { addFilter, FILTER_OPERATORS, removeFilter, updateFilter } from '../transform'
import { SqlExpressionField } from './SqlExpressionField'

interface FiltersTabProps {
  model: SqlEditableModel
  metadata: SqlMetadataSnapshot
  onSqlChange: (sql: string) => void
  onError: (message: string) => void
}

const OPERATOR_LABELS: Record<string, string> = {
  '=': 'equals',
  '!=': 'does not equal',
  '<>': 'does not equal',
  '<': 'is less than',
  '<=': 'is at most',
  '>': 'is greater than',
  '>=': 'is at least',
  LIKE: 'matches pattern',
  'NOT LIKE': 'does not match',
  ILIKE: 'matches (case-insensitive)',
  IN: 'is one of',
  'NOT IN': 'is not one of',
  IS: 'is',
  'IS NOT': 'is not',
}

export function FiltersTab({ model, metadata, onSqlChange, onError }: FiltersTabProps) {
  const [adding, setAdding] = useState(false)
  const [pendingLeft, setPendingLeft] = useState('')
  const [pendingOperator, setPendingOperator] = useState('=')
  const [pendingRight, setPendingRight] = useState('')

  function apply(action: () => { sql: string }): boolean {
    try {
      onSqlChange(action().sql)
      onError('')
      return true
    } catch (error) {
      onError(error instanceof Error ? error.message : 'Could not update filter.')
      return false
    }
  }

  function resetPending() {
    setAdding(false)
    setPendingLeft('')
    setPendingRight('')
    setPendingOperator('=')
  }

  return (
    <div className="sql-section sql-filter-section" role="tabpanel" id="sql-panel-filters" aria-labelledby="sql-tab-filters">
      {!model.filters.length && !adding && (
        <div className="sql-filter-empty">
          <strong>No filters yet</strong>
          <span>Add a rule to limit which rows this query returns.</span>
        </div>
      )}

      <div className="sql-filter-stack">
        {model.filters.map((filter, index) => {
          const valueListId = metadata.filterValues[filter.id]?.length ? `sql-filter-values-${index}` : undefined
          return (
            <div className="sql-filter-item" key={filter.id}>
              {index > 0 && filter.connector && (
                <div className="sql-filter-logic" aria-label={`How filter ${index + 1} combines with the previous filter`}>
                  <span className="sql-filter-logic__line" aria-hidden="true" />
                  <div className="sql-filter-logic__toggle" role="group" aria-label={`Logical connector for filter ${index + 1}`}>
                    {(['AND', 'OR'] as const).map((connector) => (
                      <button
                        key={connector}
                        type="button"
                        className={filter.connector === connector ? 'is-active' : ''}
                        disabled={!filter.editable}
                        aria-pressed={filter.connector === connector}
                        onClick={() => apply(() => updateFilter(model.source, filter.id, { connector: connector as SqlLogicalConnector }))}
                      >
                        {connector}
                      </button>
                    ))}
                  </div>
                  <span className="sql-filter-logic__line" aria-hidden="true" />
                </div>
              )}

              <article className={`sql-filter-card${filter.editable ? '' : ' is-readonly'}`}>
                <header className="sql-filter-card__header">
                  <div>
                    <span className="sql-filter-card__eyebrow">Filter {index + 1}</span>
                    <strong>{filter.editable ? 'Keep rows where…' : 'Complex filter'}</strong>
                  </div>
                  {filter.editable && (
                    <button
                      type="button"
                      className="icon-button icon-button--danger sql-filter-card__remove"
                      aria-label={`Remove filter ${index + 1}`}
                      onClick={() => apply(() => removeFilter(model.source, filter.id))}
                    >×</button>
                  )}
                </header>

                {filter.editable ? (
                  <div className="sql-filter-rule">
                    <label className="sql-filter-control sql-filter-control--field">
                      <span className="sql-filter-control__label">Field</span>
                      <SqlExpressionField
                        model={model}
                        className="sql-field sql-field--predicate"
                        value={filter.left}
                        ariaLabel={`Field for filter ${index + 1}`}
                        list={metadata.attributes.length ? 'sql-filter-attributes' : undefined}
                        sourceLabels={expressionSourceLabels(filter.left, model, metadata.attributes)}
                        onCommit={(value) => apply(() => updateFilter(model.source, filter.id, { left: value }))}
                      />
                    </label>

                    <label className="sql-filter-control sql-filter-control--operator">
                      <span className="sql-filter-control__label">Condition</span>
                      <select
                        className="sql-filter-operator-select"
                        aria-label={`Condition for filter ${index + 1}`}
                        value={filter.operator}
                        onChange={(event) => apply(() => updateFilter(model.source, filter.id, { operator: event.target.value }))}
                      >
                        {FILTER_OPERATORS.map((operator) => (
                          <option key={operator} value={operator}>{OPERATOR_LABELS[operator] ?? operator}</option>
                        ))}
                      </select>
                    </label>

                    <label className="sql-filter-control sql-filter-control--value">
                      <span className="sql-filter-control__label">Value</span>
                      <SqlExpressionField
                        model={model}
                        className="sql-field sql-field--predicate"
                        value={filter.right}
                        ariaLabel={`Value for filter ${index + 1}`}
                        list={valueListId}
                        placeholder="Enter a value"
                        onCommit={(value) => apply(() => updateFilter(model.source, filter.id, { right: value }))}
                      />
                    </label>
                  </div>
                ) : (
                  <div className="sql-filter-card__raw">
                    <code>{filter.raw}</code>
                    {filter.readOnlyReason && <small>{filter.readOnlyReason}</small>}
                  </div>
                )}

                {valueListId && (
                  <datalist id={valueListId}>
                    {metadata.filterValues[filter.id].map((option) => <option key={option.value} value={option.value}>{option.label}</option>)}
                  </datalist>
                )}
              </article>
            </div>
          )
        })}

        {adding && (
          <div className="sql-filter-item sql-filter-item--new">
            {model.filters.length > 0 && (
              <div className="sql-filter-logic sql-filter-logic--new" aria-hidden="true">
                <span className="sql-filter-logic__line" />
                <span className="sql-filter-logic__new">AND</span>
                <span className="sql-filter-logic__line" />
              </div>
            )}
            <article className="sql-filter-card sql-filter-card--new" aria-label="Add filter">
              <header className="sql-filter-card__header">
                <div>
                  <span className="sql-filter-card__eyebrow">New filter</span>
                  <strong>Keep rows where…</strong>
                </div>
              </header>
              <div className="sql-filter-rule">
                <label className="sql-filter-control sql-filter-control--field">
                  <span className="sql-filter-control__label">Field</span>
                  <select aria-label="New filter field" value={pendingLeft} onChange={(event) => setPendingLeft(event.target.value)}>
                    <option value="">Choose a field…</option>
                    {metadata.attributes.map((option) => (
                      <option key={option.expression} value={option.expression}>{presentMetadataExpression(option.expression, model)}</option>
                    ))}
                  </select>
                </label>
                <label className="sql-filter-control sql-filter-control--operator">
                  <span className="sql-filter-control__label">Condition</span>
                  <select aria-label="New filter condition" value={pendingOperator} onChange={(event) => setPendingOperator(event.target.value)}>
                    {FILTER_OPERATORS.map((operator) => (
                      <option key={operator} value={operator}>{OPERATOR_LABELS[operator] ?? operator}</option>
                    ))}
                  </select>
                </label>
                <label className="sql-filter-control sql-filter-control--value">
                  <span className="sql-filter-control__label">Value</span>
                  <input aria-label="New filter value" value={pendingRight} onChange={(event) => setPendingRight(event.target.value)} placeholder="Enter a value" />
                </label>
              </div>
              <div className="sql-filter-card__actions">
                <button type="button" className="secondary-button" onClick={resetPending}>Cancel</button>
                <button
                  type="button"
                  disabled={!pendingLeft || !pendingRight.trim()}
                  onClick={() => {
                    if (apply(() => addFilter(model.source, { left: pendingLeft, operator: pendingOperator, right: pendingRight }))) resetPending()
                  }}
                >Add filter</button>
              </div>
            </article>
          </div>
        )}
      </div>

      {metadata.attributes.length > 0 && !adding && (
        <div className="sql-centered-action sql-filter-add-action">
          <button type="button" className="sql-add-button" onClick={() => setAdding(true)}>+ Add filter</button>
        </div>
      )}

      {metadata.attributes.length > 0 && (
        <datalist id="sql-filter-attributes">
          {metadata.attributes.map((option) => (
            <option key={option.expression} value={presentMetadataExpression(option.expression, model)}>{presentMetadataExpression(option.expression, model)}</option>
          ))}
        </datalist>
      )}
    </div>
  )
}
