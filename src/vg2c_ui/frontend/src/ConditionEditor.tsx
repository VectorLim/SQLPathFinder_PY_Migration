import type {
  ConditionOperatorView,
  SemanticBindingView,
  SemanticOperationView,
  SymbolView,
} from './contracts.generated'
import { OptionalValue, SymbolSelector, ValidationMessage } from './shared/SemanticControls'
import { effectiveBindingValue, type FieldPath } from './workspaceState'

interface Props {
  operation: SemanticOperationView
  values: Record<string, unknown>
  symbols: SymbolView[]
  operators: ConditionOperatorView[]
  saving: boolean
  onEdit: (binding: SemanticBindingView, value: unknown, clearDraftPaths?: FieldPath[]) => void
}

export function ConditionEditor({ operation, values, symbols, operators, saving, onEdit }: Props) {
  const bindings = new Map(operation.bindings.map((binding) => [binding.name, binding]))
  const lhs = bindings.get('lhs')
  const op = bindings.get('op')
  const rhs = bindings.get('rhs')
  const conj = bindings.get('conj')
  const lhs2 = bindings.get('lhs2')
  const op2 = bindings.get('op2')
  const rhs2 = bindings.get('rhs2')
  if (!lhs || !op || !rhs) return <p className="validation-error" role="alert">Condition metadata is incomplete.</p>

  const secondEnabled = [conj, lhs2, op2, rhs2].some((binding) => binding && effectiveBindingValue(values, binding) !== null)
  const disabled = saving || operation.validation_state === 'unsupported'

  function value(binding: SemanticBindingView | undefined): unknown {
    return binding ? effectiveBindingValue(values, binding) : null
  }

  function setSecond(enabled: boolean) {
    if (!conj || !lhs2 || !op2 || !rhs2) return
    if (!enabled) {
      for (const binding of [conj, lhs2, op2, rhs2]) onEdit(binding, null)
      return
    }
    onEdit(conj, 'AND')
    onEdit(lhs2, '')
    onEdit(op2, operators[0]?.code ?? op2.value ?? 'EQ')
    onEdit(rhs2, '')
  }

  return <div className="condition-editor">
    <div className="condition-clause">
      <SymbolSelector label={lhs.display_label} value={value(lhs)} symbols={symbols} disabled={disabled || !lhs.editable} onChange={(next) => onEdit(lhs, next)} />
      <OperatorSelect binding={op} value={value(op)} operators={operators} disabled={disabled || !op.editable} onChange={(next) => onEdit(op, next)} />
      <SymbolSelector label={rhs.display_label} value={value(rhs)} symbols={symbols} disabled={disabled || !rhs.editable} onChange={(next) => onEdit(rhs, next)} />
    </div>
    {conj && lhs2 && op2 && rhs2 && <OptionalValue label="Add second condition" enabled={secondEnabled} disabled={disabled} onEnabledChange={setSecond}>
      <div className="condition-clause condition-clause--second">
        <label>{conj.display_label}
          <select value={String(value(conj) ?? 'AND')} disabled={disabled || !conj.editable} onChange={(event) => onEdit(conj, event.target.value)}>
            {(conj.value_schema?.choices ?? ['AND', 'OR']).map((choice) => <option key={String(choice)} value={String(choice)}>{String(choice)}</option>)}
          </select>
        </label>
        <SymbolSelector label={lhs2.display_label} value={value(lhs2)} symbols={symbols} disabled={disabled || !lhs2.editable} onChange={(next) => onEdit(lhs2, next)} />
        <OperatorSelect binding={op2} value={value(op2)} operators={operators} disabled={disabled || !op2.editable} onChange={(next) => onEdit(op2, next)} />
        <SymbolSelector label={rhs2.display_label} value={value(rhs2)} symbols={symbols} disabled={disabled || !rhs2.editable} onChange={(next) => onEdit(rhs2, next)} />
      </div>
    </OptionalValue>}
    {operation.validation_state === 'unresolved' && <ValidationMessage message="One or more condition values reference an unresolved symbol." />}
  </div>
}

function OperatorSelect({
  binding,
  value,
  operators,
  disabled,
  onChange,
}: {
  binding: SemanticBindingView
  value: unknown
  operators: ConditionOperatorView[]
  disabled: boolean
  onChange: (value: string) => void
}) {
  return <label>{binding.display_label}
    <select value={String(value ?? '')} disabled={disabled} onChange={(event) => onChange(event.target.value)}>
      {operators.map((operator) => <option key={operator.code} value={operator.code}>{operator.symbol} ({operator.code})</option>)}
    </select>
  </label>
}
