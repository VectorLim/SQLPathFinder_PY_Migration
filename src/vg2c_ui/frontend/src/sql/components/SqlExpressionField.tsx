import type { SqlEditableModel } from '../model'
import { presentExpression, restoreExpression } from '../presentation'
import { SqlField } from './SqlField'

interface SqlExpressionFieldProps {
  model: SqlEditableModel
  value: string
  ariaLabel: string
  onCommit: (value: string) => boolean
  className?: string
  list?: string
  placeholder?: string
  disabled?: boolean
  sourceLabels?: readonly string[]
  sourcePlacement?: 'inline' | 'stacked'
}

export function SqlExpressionField({
  model,
  value,
  ariaLabel,
  onCommit,
  className,
  list,
  placeholder,
  disabled = false,
  sourceLabels,
  sourcePlacement = 'inline',
}: SqlExpressionFieldProps) {
  const presentation = presentExpression(value, model)
  const sources = unique(sourceLabels?.length ? [...sourceLabels] : presentation.sources)
  return (
    <div className={`sql-expression-control sql-expression-control--${sourcePlacement}${disabled ? ' is-disabled' : ''}`}>
      <div className="sql-expression-shell">
        <SqlField
          className={`${className ?? 'sql-field'} sql-expression-input`}
          value={presentation.display}
          ariaLabel={ariaLabel}
          list={list}
          placeholder={placeholder}
          disabled={disabled}
          onCommit={(displayValue) => onCommit(restoreExpression(displayValue, value, model))}
        />
        {sources.length > 0 && (
          <span className="sql-source-chips sql-source-chips--inside" aria-label="Source tables">
            {sources.map((source) => (
              <span className="sql-source-chip" key={source}>{source}</span>
            ))}
          </span>
        )}
      </div>
    </div>
  )
}

function unique(values: readonly string[]): string[] {
  return [...new Set(values.filter(Boolean))]
}
