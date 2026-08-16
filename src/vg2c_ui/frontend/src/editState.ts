export interface EditState {
  values: Record<string, unknown>
  history: Record<string, unknown>[]
  future: Record<string, unknown>[]
}

export const emptyEdits = (): EditState => ({ values: {}, history: [], future: [] })

export function editValue(state: EditState, id: string, value: unknown): EditState {
  return {
    values: { ...state.values, [id]: value },
    history: [...state.history.slice(-99), state.values],
    future: [],
  }
}

export function undo(state: EditState): EditState {
  const previous = state.history.at(-1)
  if (!previous) return state
  return {
    values: previous,
    history: state.history.slice(0, -1),
    future: [state.values, ...state.future],
  }
}

export function redo(state: EditState): EditState {
  const next = state.future[0]
  if (!next) return state
  return {
    values: next,
    history: [...state.history.slice(-99), state.values],
    future: state.future.slice(1),
  }
}
