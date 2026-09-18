import type { TabState } from './workspaceState'
import { getChangeActionState } from './workspaceGuards'
import { baseName, formatOperationLabel } from './operationLabels'

export type CommandGroup = 'Workspace' | 'Navigation' | 'Editing' | 'View'

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

interface WorkspaceCommands {
  canStage: boolean
  queuedCount: number
  canTranslate: boolean
  openFiles: () => void
  openFolder: () => void
  uploadQueued: () => void
  translateSelected: () => void
}

interface BuildCommandsInput {
  tabs: TabState[]
  active: TabState | null
  actions: CommandActions
  workspace: WorkspaceCommands
}

export function buildCommands({ tabs, active, actions, workspace }: BuildCommandsInput): WorkbenchCommand[] {
  const commands: WorkbenchCommand[] = [
    {
      id: 'workspace.upload-files',
      group: 'Workspace',
      label: 'Upload files',
      keywords: ['browse', 'source', 'data'],
      disabled: !workspace.canStage,
      run: workspace.openFiles,
    },
    {
      id: 'workspace.upload-folder',
      group: 'Workspace',
      label: 'Upload folder',
      keywords: ['browse', 'directory', 'workspace'],
      disabled: !workspace.canStage,
      run: workspace.openFolder,
    },
    {
      id: 'workspace.upload-queued',
      group: 'Workspace',
      label: workspace.queuedCount ? `Upload queued files (${workspace.queuedCount})` : 'Upload queued files',
      keywords: ['queue', 'send'],
      disabled: !workspace.queuedCount,
      run: workspace.uploadQueued,
    },
    {
      id: 'workspace.translate-selected',
      group: 'Workspace',
      label: 'Translate selected sources',
      keywords: ['compile', 'generate', 'python'],
      disabled: !workspace.canTranslate,
      run: workspace.translateSelected,
    },
  ]

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
      commands.push({
        id: `navigation.item:${scope.id}`,
        group: 'Navigation',
        label: `Go to ${scope.label}`,
        keywords: [scope.scope_kind, `${scope.start_index + 1}`, `${scope.end_index + 1}`],
        run: () => actions.selectItem(scope.id),
      })
    }
    for (const step of active.document.steps) {
      for (const operation of step.operations) {
        const label = formatOperationLabel(step, operation, active.document.effects)
        commands.push({
          id: `navigation.item:${operation.id}`,
          group: 'Navigation',
          label: `Go to ${label.primary}`,
          keywords: [label.secondary ?? '', step.description, step.function_name, operation.utility.title],
          run: () => actions.selectItem(operation.id),
        })
      }
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

  const changeActions = getChangeActionState(active)
  commands.push(
    {
      id: 'editing.undo',
      group: 'Editing',
      label: 'Undo change',
      shortcut: 'Ctrl/⌘+Z',
      disabled: !changeActions.canUndo,
      run: actions.undo,
    },
    {
      id: 'editing.redo',
      group: 'Editing',
      label: 'Redo change',
      shortcut: 'Ctrl/⌘+Y',
      disabled: !changeActions.canRedo,
      run: actions.redo,
    },
    {
      id: 'editing.preview',
      group: 'Editing',
      label: 'Preview changes',
      shortcut: 'Ctrl/⌘+S',
      disabled: !changeActions.canPreview,
      run: actions.validate,
    },
    {
      id: 'editing.apply',
      group: 'Editing',
      label: 'Apply validated changes',
      shortcut: 'Ctrl/⌘+S',
      disabled: !changeActions.canApply,
      run: actions.apply,
    },
    {
      id: 'editing.reload',
      group: 'Editing',
      label: 'Reload conflicted document',
      disabled: !changeActions.canReload,
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
