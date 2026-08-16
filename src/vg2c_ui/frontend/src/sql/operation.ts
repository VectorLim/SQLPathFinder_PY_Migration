import type { ParameterDescriptor, StepNode } from '../types'

export interface SqlOperationParameters {
  sql: ParameterDescriptor | null
  output: ParameterDescriptor | null
  inputs: ParameterDescriptor | null
}

export interface EffectiveStepFiles {
  inputs: string[]
  outputs: string[]
}

export function isSqlOperation(step: StepNode): boolean {
  return step.functional_kind === 'SQL_QUERY' || step.functional_kind === 'SQLITE_QUERY'
}

export function sqlOperationParameters(step: StepNode): SqlOperationParameters {
  if (!isSqlOperation(step)) return { sql: null, output: null, inputs: null }
  return {
    sql: parameterNamed(step, 'sql'),
    output: parameterNamed(step, 'output'),
    inputs: parameterNamed(step, 'inputs'),
  }
}

export function effectiveParameterValue(
  parameter: ParameterDescriptor | null,
  values: Record<string, unknown>,
): unknown {
  if (!parameter) return null
  return Object.hasOwn(values, parameter.id) ? values[parameter.id] : parameter.value
}

export function effectiveSql(step: StepNode, values: Record<string, unknown>): string | null {
  const parameter = sqlOperationParameters(step).sql
  const value = effectiveParameterValue(parameter, values)
  return typeof value === 'string' ? value : null
}

export function effectiveStepFiles(step: StepNode, values: Record<string, unknown> = {}): EffectiveStepFiles {
  if (!isSqlOperation(step)) return { inputs: step.csv_inputs, outputs: step.csv_outputs }
  const parameters = sqlOperationParameters(step)
  const outputValue = effectiveParameterValue(parameters.output, values)
  const inputValue = effectiveParameterValue(parameters.inputs, values)
  const outputs = typeof outputValue === 'string' && outputValue.trim()
    ? [outputValue.trim()]
    : step.csv_outputs
  const inputs = Array.isArray(inputValue) && inputValue.every((item) => typeof item === 'string')
    ? inputValue.map((item) => item.trim()).filter(Boolean)
    : step.csv_inputs
  return { inputs, outputs }
}

function parameterNamed(step: StepNode, name: string): ParameterDescriptor | null {
  return step.parameters.find((parameter) => parameter.name.toLocaleLowerCase() === name) ?? null
}
