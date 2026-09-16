import { useEffect, useMemo, useRef, useState, type KeyboardEvent } from 'react'

import type { CommandGroup, WorkbenchCommand } from './commands'
import { useModalDialog } from './useModalDialog'
import './commandPalette.css'

interface Props {
  open: boolean
  commands: WorkbenchCommand[]
  onClose: () => void
}

const GROUPS: CommandGroup[] = ['Workspace', 'Navigation', 'Editing', 'View']
const RESULTS_ID = 'command-results'

export function CommandPalette({ open, commands, onClose }: Props) {
  const inputRef = useRef<HTMLInputElement>(null)
  const modal = useModalDialog(open, onClose, { onOpened: () => inputRef.current?.focus() })
  const [query, setQuery] = useState('')
  const [activeId, setActiveId] = useState<string | null>(null)

  const filtered = useMemo(() => {
    const needle = query.trim().toLowerCase()
    if (!needle) return commands
    return commands.filter((command) => [command.label, command.group, ...(command.keywords ?? [])]
      .join(' ')
      .toLowerCase()
      .includes(needle))
  }, [commands, query])
  const enabled = filtered.filter((command) => !command.disabled)
  const enabledSignature = enabled.map((command) => command.id).join('|')

  useEffect(() => {
    if (open) setQuery('')
  }, [open])

  useEffect(() => {
    setActiveId((current) => current && enabled.some((command) => command.id === current)
      ? current
      : enabled[0]?.id ?? null)
  }, [enabledSignature])

  useEffect(() => {
    if (!open || !activeId) return
    modal.dialogRef.current?.querySelector<HTMLElement>('.command-item.is-active')?.scrollIntoView({ block: 'nearest' })
  }, [open, activeId])

  function execute(command: WorkbenchCommand) {
    if (command.disabled) return
    modal.requestClose()
    queueMicrotask(command.run)
  }

  function handleKeyDown(event: KeyboardEvent<HTMLInputElement>) {
    if (!enabled.length) return
    const index = Math.max(0, enabled.findIndex((command) => command.id === activeId))
    if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
      event.preventDefault()
      const delta = event.key === 'ArrowDown' ? 1 : -1
      const next = enabled[(index + delta + enabled.length) % enabled.length]
      if (next) setActiveId(next.id)
      return
    }
    if (event.key === 'Enter') {
      event.preventDefault()
      const command = enabled.find((item) => item.id === activeId) ?? enabled[0]
      if (command) execute(command)
    }
  }

  return <dialog
    ref={modal.dialogRef}
    className="command-dialog"
    aria-labelledby="command-dialog-title"
    onClose={modal.handleClose}
    onClick={modal.handleBackdropClick}
  >
    <div className="command-panel">
      <header className="command-header">
        <div>
          <span className="eyebrow">Workbench</span>
          <h2 id="command-dialog-title">Commands</h2>
        </div>
        <button className="icon-button" type="button" onClick={modal.requestClose} aria-label="Close command palette">×</button>
      </header>
      <label className="command-search">
        <span className="sr-only">Search commands</span>
        <input
          ref={inputRef}
          type="search"
          value={query}
          onChange={(event) => setQuery(event.target.value)}
          onKeyDown={handleKeyDown}
          placeholder="Search files, operations, and actions…"
          autoComplete="off"
          aria-controls={RESULTS_ID}
          aria-activedescendant={activeId ? commandItemId(activeId) : undefined}
        />
      </label>
      <div id={RESULTS_ID} className="command-results" aria-live="polite">
        {GROUPS.map((group) => {
          const items = filtered.filter((command) => command.group === group)
          if (!items.length) return null
          return <section className="command-group" key={group} aria-labelledby={`command-group-${group.toLowerCase()}`}>
            <h3 id={`command-group-${group.toLowerCase()}`}>{group}</h3>
            <div>
              {items.map((command) => <button
                id={commandItemId(command.id)}
                key={command.id}
                className={`command-item${activeId === command.id && !command.disabled ? ' is-active' : ''}`}
                type="button"
                disabled={command.disabled}
                onMouseEnter={() => { if (!command.disabled) setActiveId(command.id) }}
                onClick={() => execute(command)}
              >
                <span>{command.label}</span>
                {command.shortcut && <kbd>{command.shortcut}</kbd>}
              </button>)}
            </div>
          </section>
        })}
        {!filtered.length && <p className="command-empty">No matching commands.</p>}
      </div>
    </div>
  </dialog>
}

function commandItemId(id: string): string {
  return `command-item-${encodeURIComponent(id)}`
}
