import { baseName } from './pathDisplay'
import type { TabState } from './workspaceState'
import { getChangeActionState } from './workspaceGuards'

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
  save: () => void
  generate: () => void
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
      keywords: ['translate', 'source', 'script'],
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
    for (const operation of active.document.semantic_operations.filter((item) => item.visibility !== 'internal')) {
      commands.push({
        id: `navigation.item:${operation.id}`,
        group: 'Navigation',
        label: `Go to ${operation.display_name}`,
        keywords: [
          operation.kind,
          operation.description,
          ...operation.comments,
          ...operation.bindings.flatMap((binding) => [
            binding.display_label,
            binding.name,
            String(binding.value ?? ''),
          ]),
        ],
        run: () => actions.selectItem(operation.id),
      })
    }
  }

  commands.push({
    id: 'navigation.inspector',
    group: 'Navigation',
    label: 'Open context',
    keywords: ['context', 'file flow', 'email', 'globals', 'dependencies'],
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
      disabled: !changeActions.canPreview,
      run: actions.validate,
    },
    {
      id: 'editing.save',
      group: 'Editing',
      label: 'Save changes',
      shortcut: 'Ctrl/⌘+S',
      disabled: !changeActions.canSave,
      run: actions.save,
    },
    {
      id: 'editing.generate',
      group: 'Editing',
      label: 'Generate Python',
      disabled: !changeActions.canGenerate,
      run: actions.generate,
    },
    {
      id: 'editing.reload',
      group: 'Editing',
      label: 'Reload conflicted document',
      disabled: !changeActions.canReload,
      run: actions.reload,
    },
  )

  const operations = active?.document.semantic_operations ?? []
  const hasGroups = operations.some((operation) =>
    operations.some((child) => child.parent_operation_id === operation.id),
  )
  commands.push(
    {
      id: 'view.expand-all',
      group: 'View',
      label: 'Expand all groups',
      disabled: !hasGroups,
      run: actions.expandAll,
    },
    {
      id: 'view.collapse-all',
      group: 'View',
      label: 'Collapse all groups',
      disabled: !hasGroups,
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
