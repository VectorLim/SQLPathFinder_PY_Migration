import type { TabState } from './workspaceState'
import { baseName, formatOperationLabel, formatScopeLabel } from './operationLabels'

export type CommandGroup = 'Navigation' | 'Editing' | 'View'

export interface WorkbenchCommand {
  id: string
  group: CommandGroup
  label: string
  keywords?: string[]
  shortcut?: string
  disabled?: boolean
  run: () => void
}

interface CommandActions {
  activateDocument: (id: string) => void
  selectItem: (id: string) => void
  openInspector: () => void
  undo: () => void
  redo: () => void
  validate: () => void
  apply: () => void
  reload: () => void
  expandAll: () => void
  collapseAll: () => void
}

interface BuildCommandsInput {
  tabs: TabState[]
  active: TabState | null
  actions: CommandActions
}

export function buildCommands({ tabs, active, actions }: BuildCommandsInput): WorkbenchCommand[] {
  const commands: WorkbenchCommand[] = []

  for (const tab of tabs) {
    const name = baseName(tab.document.output_path || tab.document.source_path)
    commands.push({
      id: `navigation.document:${tab.document.id}`,
      group: 'Navigation',
      label: `Open ${name}`,
      keywords: [tab.document.source_path, tab.document.output_path],
      run: () => actions.activateDocument(tab.document.id),
    })
  }

  if (active) {
    for (const scope of active.document.scopes) {
      const label = formatScopeLabel(scope)
      commands.push({
        id: `navigation.item:${scope.id}`,
        group: 'Navigation',
        label: `Go to ${label}`,
        keywords: [scope.scope_kind, `${scope.start_index + 1}`, `${scope.end_index + 1}`],
        run: () => actions.selectItem(scope.id),
      })
    }
    for (const step of active.document.steps) {
      const label = formatOperationLabel(step)
      commands.push({
        id: `navigation.item:${step.id}`,
        group: 'Navigation',
        label: `Go to ${label.primary}`,
        keywords: [label.secondary ?? '', step.description, step.function_name, step.utility.title],
        run: () => actions.selectItem(step.id),
      })
    }
  }

  commands.push({
    id: 'navigation.inspector',
    group: 'Navigation',
    label: 'Open file context',
    keywords: ['inspector', 'data flow', 'dependencies'],
    disabled: !active,
    run: actions.openInspector,
  })

  const editCount = active ? Object.keys(active.edits.values).length : 0
  commands.push(
    {
      id: 'editing.undo',
      group: 'Editing',
      label: 'Undo change',
      shortcut: 'Ctrl/⌘+Z',
      disabled: !active?.edits.history.length,
      run: actions.undo,
    },
    {
      id: 'editing.redo',
      group: 'Editing',
      label: 'Redo change',
      shortcut: 'Ctrl/⌘+Y',
      disabled: !active?.edits.future.length,
      run: actions.redo,
    },
    {
      id: 'editing.preview',
      group: 'Editing',
      label: 'Preview changes',
      shortcut: 'Ctrl/⌘+S',
      disabled: !active || !editCount || active.status === 'validating' || !active.document.synchronized,
      run: actions.validate,
    },
    {
      id: 'editing.apply',
      group: 'Editing',
      label: 'Apply validated changes',
      shortcut: 'Ctrl/⌘+S',
      disabled: !active?.preview?.valid || active?.status === 'saving',
      run: actions.apply,
    },
    {
      id: 'editing.reload',
      group: 'Editing',
      label: 'Reload conflicted document',
      disabled: active?.status !== 'conflict',
      run: actions.reload,
    },
  )

  const hasScopes = Boolean(active?.document.scopes.length)
  commands.push(
    {
      id: 'view.expand-all',
      group: 'View',
      label: 'Expand all scopes',
      disabled: !hasScopes,
      run: actions.expandAll,
    },
    {
      id: 'view.collapse-all',
      group: 'View',
      label: 'Collapse all scopes',
      disabled: !hasScopes,
      run: actions.collapseAll,
    },
  )

  return commands
}

export function executeCommand(commands: WorkbenchCommand[], id: string): boolean {
  const command = commands.find((item) => item.id === id)
  if (!command || command.disabled) return false
  command.run()
  return true
}
