import { useState } from 'react'

import type { SqlMetadataSnapshot } from '../useSqlMetadata'
import type { SqlEditableModel, SqlSource } from '../model'
import { displaySource, expressionSourceLabels, presentMetadataExpression, restoreSource } from '../presentation'
import {
  addJoin,
  FILTER_OPERATORS,
  JOIN_TYPES,
  removeJoin,
  removeJoinPredicate,
  updateJoinPredicate,
  updateJoinSource,
  updateJoinType,
} from '../transform'
import { SqlExpressionField } from './SqlExpressionField'
import { SqlField } from './SqlField'
import { SqlOperatorControl } from './SqlOperatorControl'

interface JoinsTabProps {
  model: SqlEditableModel
  metadata: SqlMetadataSnapshot
  onSqlChange: (sql: string) => void
  onError: (message: string) => void
}

export function JoinsTab({ model, metadata, onSqlChange, onError }: JoinsTabProps) {
  const [adding, setAdding] = useState(false)
  const [pendingSource, setPendingSource] = useState('')
  const [pendingJoinType, setPendingJoinType] = useState('INNER')
  const [pendingLeft, setPendingLeft] = useState('')
  const [pendingRight, setPendingRight] = useState('')

  function apply(action: () => { sql: string }): boolean {
    try {
      onSqlChange(action().sql)
      onError('')
      return true
    } catch (error) {
      onError(error instanceof Error ? error.message : 'Could not update join.')
      return false
    }
  }

  const pendingModel = pendingSource ? withPendingSource(model, pendingSource) : model

  return (
    <div className="sql-section" role="tabpanel" id="sql-panel-joins" aria-labelledby="sql-tab-joins">
      {!model.joins.length && <p className="empty-copy">This query has no explicit JOIN clauses.</p>}
      {model.joins.map((join, index) => {
        const keyOptions = metadata.joinKeys[join.id] ?? []
        const keyListId = keyOptions.length ? `sql-join-keys-${index}` : undefined
        const joinTypes = join.predicates.length || join.readOnlyReason?.includes('USING')
          ? JOIN_TYPES.filter((joinType) => joinType !== 'CROSS')
          : JOIN_TYPES
        return (
          <article className={`sql-join-card${join.readOnlyReason?.includes('NATURAL') ? ' is-readonly' : ''}`} key={join.id}>
            <div className="sql-row sql-join-heading">
              <span className="sql-row__index" aria-hidden="true">{index + 1}</span>
              <select
                className="sql-join-type"
                aria-label={`Join type ${index + 1}`}
                value={join.joinType}
                disabled={!join.editableType}
                onChange={(event) => apply(() => updateJoinType(model.source, join.id, event.target.value))}
              >
                {joinTypes.map((joinType) => <option key={joinType} value={joinType}>{joinType}</option>)}
              </select>
              <span className="sql-join-word" aria-hidden="true">JOIN</span>
              <SqlField
                className="sql-field sql-field--join-source"
                value={displaySource(join.source)}
                ariaLabel={`Join source ${index + 1}`}
                disabled={!join.editableSource}
                list={metadata.joinCandidates.length ? 'sql-join-candidates' : undefined}
                onCommit={(value) => apply(() => updateJoinSource(model.source, join.id, restoreSource(value, join.source)))}
              />
              <button type="button" className="icon-button icon-button--danger sql-remove" aria-label={`Remove join ${index + 1}`} onClick={() => apply(() => removeJoin(model.source, join.id))}>×</button>
            </div>

            {join.readOnlyReason && <p className="sql-inline-note">{join.readOnlyReason}</p>}
            {join.predicates.map((predicate, predicateIndex) => (
              <div className={`sql-row sql-join-predicate${predicate.editable ? '' : ' is-readonly'}`} key={predicate.id}>
                <span className="sql-connector sql-connector--join" aria-hidden="true">{predicateIndex === 0 ? 'ON' : predicate.connector ?? 'AND'}</span>
                {predicate.editable ? (
                  <>
                    <SqlExpressionField
                      model={model}
                      className="sql-field sql-field--predicate"
                      value={predicate.left}
                      ariaLabel={`Left join key ${predicateIndex + 1}`}
                      list={keyListId}
                      sourceLabels={expressionSourceLabels(predicate.left, model, metadata.attributes)}
                      onCommit={(value) => apply(() => updateJoinPredicate(model.source, join.id, predicate.id, { left: value }))}
                    />
                    <SqlOperatorControl
                      value={predicate.operator}
                      options={FILTER_OPERATORS}
                      ariaLabel={`Join operator ${predicateIndex + 1}`}
                      onChange={(operator) => apply(() => updateJoinPredicate(model.source, join.id, predicate.id, { operator }))}
                    />
                    <SqlExpressionField
                      model={model}
                      className="sql-field sql-field--predicate"
                      value={predicate.right}
                      ariaLabel={`Right join key ${predicateIndex + 1}`}
                      list={keyListId}
                      sourceLabels={expressionSourceLabels(predicate.right, model, metadata.attributes)}
                      onCommit={(value) => apply(() => updateJoinPredicate(model.source, join.id, predicate.id, { right: value }))}
                    />
                    {join.predicates.length > 1 && (
                      <button type="button" className="icon-button icon-button--danger sql-remove" aria-label={`Remove join key ${predicateIndex + 1}`} onClick={() => apply(() => removeJoinPredicate(model.source, join.id, predicate.id))}>×</button>
                    )}
                  </>
                ) : (
                  <div className="sql-raw-row">
                    <code>{predicate.raw}</code>
                    {predicate.readOnlyReason && <small>{predicate.readOnlyReason}</small>}
                  </div>
                )}
              </div>
            ))}
            {!join.predicates.length && !join.readOnlyReason && <p className="sql-inline-note">No editable ON predicates were found for this join.</p>}
            {keyListId && (
              <datalist id={keyListId}>
                {keyOptions.flatMap((option) => [option.left, option.right]).filter(Boolean).map((value) => (
                  <option key={value} value={presentMetadataExpression(value, model)} />
                ))}
              </datalist>
            )}
          </article>
        )
      })}

      {metadata.joinCandidates.length > 0 && !adding && (
        <div className="sql-centered-action">
          <button type="button" className="sql-add-button" onClick={() => setAdding(true)}>+ Add join</button>
        </div>
      )}

      {metadata.joinCandidates.length > 0 && adding && (
        <div className="sql-add-panel sql-add-panel--join" aria-label="Add join">
          <div className="sql-add-panel__row">
            <select aria-label="New join type" value={pendingJoinType} onChange={(event) => setPendingJoinType(event.target.value)}>
              {JOIN_TYPES.filter((joinType) => joinType !== 'CROSS').map((joinType) => <option key={joinType} value={joinType}>{joinType}</option>)}
            </select>
            <select
              aria-label="New join source"
              value={pendingSource}
              onChange={(event) => {
                const source = event.target.value
                setPendingSource(source)
                const suggestion = metadata.joinCandidateKeys[source]?.[0]
                if (suggestion) {
                  setPendingLeft(suggestion.left)
                  setPendingRight(suggestion.right)
                } else {
                  setPendingLeft('')
                  setPendingRight('')
                }
              }}
            >
              <option value="">Choose a table…</option>
              {metadata.joinCandidates.map((option) => <option key={option.source} value={option.source}>{displaySource(option.source)}</option>)}
            </select>
          </div>
          {pendingSource && (
            <div className="sql-add-join-keys">
              <SqlExpressionField
                model={pendingModel}
                className="sql-field sql-field--predicate"
                value={pendingLeft}
                ariaLabel="New join left key"
                placeholder="Left key"
                sourceLabels={expressionSourceLabels(pendingLeft, pendingModel, metadata.attributes)}
                onCommit={(value) => { setPendingLeft(value); return true }}
              />
              <SqlOperatorControl
                value="="
                options={['=']}
                ariaLabel="New join operator"
                onChange={() => {}}
              />
              <SqlExpressionField
                model={pendingModel}
                className="sql-field sql-field--predicate"
                value={pendingRight}
                ariaLabel="New join right key"
                placeholder="Right key"
                sourceLabels={expressionSourceLabels(pendingRight, pendingModel, metadata.attributes)}
                onCommit={(value) => { setPendingRight(value); return true }}
              />
            </div>
          )}
          <div className="sql-add-panel__actions">
            <button type="button" className="secondary-button" onClick={() => {
              setAdding(false)
              setPendingSource('')
              setPendingLeft('')
              setPendingRight('')
              setPendingJoinType('INNER')
            }}>Cancel</button>
            <button
              type="button"
              disabled={!pendingSource || !pendingLeft.trim() || !pendingRight.trim()}
              onClick={() => {
                if (apply(() => addJoin(model.source, {
                  joinType: pendingJoinType,
                  source: pendingSource,
                  left: pendingLeft,
                  right: pendingRight,
                }))) {
                  setPendingSource('')
                  setPendingLeft('')
                  setPendingRight('')
                  setPendingJoinType('INNER')
                  setAdding(false)
                }
              }}
            >Add</button>
          </div>
        </div>
      )}

      {metadata.joinCandidates.length > 0 && (
        <datalist id="sql-join-candidates">
          {metadata.joinCandidates.map((option) => <option key={option.source} value={displaySource(option.source)}>{displaySource(option.source)}</option>)}
        </datalist>
      )}
    </div>
  )
}

function withPendingSource(model: SqlEditableModel, expression: string): SqlEditableModel {
  const source: SqlSource = {
    id: 'source-pending-join',
    expression,
    kind: 'join',
    editable: true,
    readOnlyReason: null,
    span: { start: 0, end: 0 },
    joinId: 'pending-join',
  }
  return { ...model, sources: [...model.sources, source] }
}
