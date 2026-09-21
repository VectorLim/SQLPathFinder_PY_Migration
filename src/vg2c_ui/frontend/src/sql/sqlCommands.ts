export type SqlCommand =
  | { action: 'add-selection'; arguments: { expression: string } }
  | { action: 'update-selection'; arguments: { selection_id: string; expression?: string; alias?: string | null } }
  | { action: 'remove-selection'; arguments: { selection_id: string } }
  | { action: 'move-selection'; arguments: { selection_id: string; direction: -1 | 1 } }
  | { action: 'reorder-selection'; arguments: { selection_id: string; target_index: number } }
  | { action: 'add-filter'; arguments: { left: string; operator: string; right: string; connector?: 'AND' | 'OR' } }
  | { action: 'update-filter'; arguments: { filter_id: string; left?: string; operator?: string; right?: string; connector?: 'AND' | 'OR' } }
  | { action: 'remove-filter'; arguments: { filter_id: string } }
  | { action: 'add-join'; arguments: { join_type: string; source: string; left: string; right: string; operator?: string } }
  | { action: 'update-join-type'; arguments: { join_id: string; join_type: string } }
  | { action: 'update-join-source'; arguments: { join_id: string; source: string } }
  | { action: 'update-join-predicate'; arguments: { join_id: string; predicate_id: string; left?: string; operator?: string; right?: string } }
  | { action: 'remove-join-predicate'; arguments: { join_id: string; predicate_id: string } }
  | { action: 'remove-join'; arguments: { join_id: string } }
  | { action: 'update-source'; arguments: { source_id: string; source: string } }

export type SqlCommandName = SqlCommand['action']
export type SqlCommandArguments<T extends SqlCommandName> =
  Extract<SqlCommand, { action: T }>['arguments']

export function sqlCommand<T extends SqlCommandName>(
  action: T,
  arguments_: SqlCommandArguments<T>,
): SqlCommand {
  return { action, arguments: arguments_ } as SqlCommand
}
