interface SqlOperatorControlProps {
  value: string
  options: readonly string[]
  ariaLabel: string
  onChange: (value: string) => void
}

export function SqlOperatorControl({ value, options, ariaLabel, onChange }: SqlOperatorControlProps) {
  return (
    <div className="sql-operator-control">
      <span className="sql-operator-chain" aria-hidden="true" />
      <select
        className="sql-operator"
        aria-label={ariaLabel}
        value={value}
        onChange={(event) => onChange(event.target.value)}
      >
        {options.map((option) => <option key={option} value={option}>{option}</option>)}
      </select>
    </div>
  )
}
