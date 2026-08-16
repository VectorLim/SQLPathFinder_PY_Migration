import type { SqlAttributeOption } from '../metadata'
import type { SqlEditableModel } from '../model'
import { attributeSourceLabels, presentMetadataExpression } from '../presentation'

interface SqlAttributePickerProps {
  model: SqlEditableModel
  options: readonly SqlAttributeOption[]
  value: string
  ariaLabel: string
  onChange: (value: string) => void
}

export function SqlAttributePicker({ model, options, value, ariaLabel, onChange }: SqlAttributePickerProps) {
  const selected = options.find((option) => option.expression === value) ?? null
  return (
    <details className="sql-attribute-picker">
      <summary aria-label={ariaLabel}>
        {selected ? <AttributeOption model={model} option={selected} /> : <span className="sql-picker-placeholder">Choose an attribute…</span>}
        <span className="sql-picker-chevron" aria-hidden="true">⌄</span>
      </summary>
      <div className="sql-attribute-menu" role="listbox" aria-label={ariaLabel}>
        {options.map((option, index) => (
          <button
            type="button"
            role="option"
            aria-selected={option.expression === value}
            className={option.expression === value ? 'is-selected' : ''}
            key={`${option.expression}-${index}`}
            onClick={(event) => {
              onChange(option.expression)
              const details = event.currentTarget.closest('details') as HTMLDetailsElement | null
              if (details) details.open = false
            }}
          >
            <AttributeOption model={model} option={option} />
          </button>
        ))}
      </div>
    </details>
  )
}

function AttributeOption({ model, option }: { model: SqlEditableModel; option: SqlAttributeOption }) {
  const sources = attributeSourceLabels(option, model)
  return (
    <span className="sql-attribute-option">
      <span className="sql-attribute-option__label">{presentMetadataExpression(option.label || option.expression, model)}</span>
      {sources.length > 0 && (
        <span className="sql-source-chips" aria-label="Available source tables">
          {sources.map((source) => <span className="sql-source-chip" key={source}>{source}</span>)}
        </span>
      )}
    </span>
  )
}
