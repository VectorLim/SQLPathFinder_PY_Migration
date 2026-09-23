export type SqlCommand =
  | { action: 'add-selection'; arguments: { column_choice_id: string } }
  | { action: 'update-selection'; arguments: { selection_id: string; column_choice_id?: string; alias?: string | null } }
  | { action: 'remove-selection'; arguments: { selection_id: string } }
  | { action: 'reorder-selection'; arguments: { selection_id: string; target_index: number } }
  | { action: 'add-filter'; arguments: { left_choice_id: string; operator: string; right: string; connector?: string } }
  | { action: 'update-filter'; arguments: { filter_id: string; left_choice_id?: string; operator?: string; right?: string; connector?: string } }
  | { action: 'remove-filter'; arguments: { filter_id: string } }
  | { action: 'add-join'; arguments: { join_type: string; table_choice_id: string; left_choice_id: string; right_choice_id: string; operator?: string } }
  | { action: 'update-join-type'; arguments: { join_id: string; join_type: string } }
  | { action: 'update-join-predicate'; arguments: { join_id: string; predicate_id: string; left_choice_id?: string; operator?: string; right_choice_id?: string } }
  | { action: 'remove-join-predicate'; arguments: { join_id: string; predicate_id: string } }
  | { action: 'remove-join'; arguments: { join_id: string } }

export type SqlCommandName = SqlCommand['action']
export type SqlCommandArguments<T extends SqlCommandName> =
  Extract<SqlCommand, { action: T }>['arguments']

export function sqlCommand<T extends SqlCommandName>(
  action: T,
  arguments_: SqlCommandArguments<T>,
): SqlCommand {
  return { action, arguments: arguments_ } as SqlCommand
}
