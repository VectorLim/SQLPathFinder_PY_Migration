import type {
  ConditionOperatorView,
  SemanticOperationView,
  SymbolView,
} from './api/contracts.generated'
import type { BindingEdit } from './semanticEditorSession'
import { OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { effectiveBindingValue } from './workspaceState'

interface Props {
  operation: SemanticOperationView
  values: Record<string, unknown>
  symbols: SymbolView[]
  operators: ConditionOperatorView[]
  saving: boolean
  onEdit: (request: BindingEdit) => void
}

export function ConditionEditor({ operation, values, symbols, operators, saving, onEdit }: Props) {
  const bindings = new Map(operation.bindings.map((binding) => [binding.name, binding]))
  const get = (name: string) => bindings.get(name)
  const value = (name: string) => {
    const binding = get(name)
    return binding ? effectiveBindingValue(values, binding) : null
  }
  const secondEnabled = ['conj', 'lhs2', 'op2', 'rhs2'].some((name) => {
    const current = value(name)
    return current !== null && current !== undefined && current !== ''
  })

  function symbolField(name: string) {
    const binding = get(name)
    if (!binding) return null
    return <div className="parameter semantic-binding" key={binding.id}>
      <SymbolSelector
        label={binding.display_label}
        value={value(name)}
        symbolId={binding.symbol_id}
        symbols={symbols}
        disabled={saving || !binding.editable}
        onChange={(next) => onEdit({ binding, value: next })}
      />
      {binding.validation_state === 'unresolved' && <ValidationMessage message="Unknown symbol. Choose a known symbol or enter a literal value." />}
    </div>
  }

  function operatorField(name: string) {
    const binding = get(name)
    if (!binding) return null
    const allowedCodes = new Set((binding.value_schema?.choices ?? []).map(String))
    const choices = operators.filter((operator) => !allowedCodes.size || allowedCodes.has(operator.code))
    return <label className="parameter semantic-binding" key={binding.id}>{binding.display_label}
      <select value={String(value(name) ?? '')} disabled={saving || !binding.editable} onChange={(event) => onEdit({ binding, value: event.target.value || null })}>
        {!binding.required && <option value="">Not set</option>}
        {choices.map((operator) => <option key={operator.code} value={operator.code}>{operator.symbol} ({operator.code})</option>)}
      </select>
    </label>
  }

  function connectorField() {
    const binding = get('conj')
    if (!binding) return null
    const choices = binding.value_schema?.choices ?? ['AND', 'OR']
    return <label className="parameter semantic-binding" key={binding.id}>{binding.display_label}
      <select value={String(value('conj') ?? '')} disabled={saving || !binding.editable} onChange={(event) => onEdit({ binding, value: event.target.value || null })}>
        <option value="">Not set</option>
        {choices.map((choice) => <option key={String(choice)} value={String(choice)}>{String(choice)}</option>)}
      </select>
    </label>
  }

  function setSecond(enabled: boolean) {
    for (const name of ['conj', 'lhs2', 'op2', 'rhs2']) {
      const binding = get(name)
      if (!binding) continue
      if (!enabled) onEdit({ binding, value: null })
      else if (name === 'conj') onEdit({ binding, value: 'AND' })
      else if (name === 'op2') onEdit({ binding, value: operators[0]?.code ?? binding.value_schema?.choices[0] ?? null })
      else onEdit({ binding, value: '' })
    }
  }

  return <div className="condition-editor">
    <div className="condition-clause">{symbolField('lhs')}{operatorField('op')}{symbolField('rhs')}</div>
    <OptionalValue label="Add second condition" enabled={secondEnabled} disabled={saving} onEnabledChange={setSecond}>
      <div className="condition-clause condition-clause--secondary">{connectorField()}{symbolField('lhs2')}{operatorField('op2')}{symbolField('rhs2')}</div>
    </OptionalValue>
    {operation.validation_state === 'unresolved' && <ValidationMessage message="One or more condition values reference an unresolved symbol." />}
  </div>
}
