import { useEffect, useMemo, useRef, useState, type KeyboardEvent } from 'react'

import type { CommandGroup, WorkbenchCommand } from './commands'
import './commandPalette.css'

interface Props {
  open: boolean
  commands: WorkbenchCommand[]
  onClose: () => void
}

const GROUPS: CommandGroup[] = ['Navigation', 'Editing', 'View']

export function CommandPalette({ open, commands, onClose }: Props) {
  const dialogRef = useRef<HTMLDialogElement>(null)
  const openerRef = useRef<HTMLElement | null>(null)
  const inputRef = useRef<HTMLInputElement>(null)
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
    const dialog = dialogRef.current
    if (!dialog) return
    if (open && !dialog.open) {
      openerRef.current = document.activeElement instanceof HTMLElement ? document.activeElement : null
      setQuery('')
      dialog.showModal()
      queueMicrotask(() => inputRef.current?.focus())
    } else if (!open && dialog.open) {
      dialog.close()
    }
  }, [open])

  useEffect(() => {
    setActiveId((current) => current && enabled.some((command) => command.id === current)
      ? current
      : enabled[0]?.id ?? null)
  }, [enabledSignature])

  function requestClose() {
    const dialog = dialogRef.current
    if (dialog?.open) dialog.close()
    else onClose()
  }

  function execute(command: WorkbenchCommand) {
    if (command.disabled) return
    command.run()
    requestClose()
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
    ref={dialogRef}
    className="command-dialog"
    aria-labelledby="command-dialog-title"
    onClose={() => {
      onClose()
      const opener = openerRef.current
      openerRef.current = null
      queueMicrotask(() => opener?.focus())
    }}
    onClick={(event) => { if (event.target === event.currentTarget) requestClose() }}
  >
    <div className="command-panel">
      <header className="command-header">
        <div>
          <span className="eyebrow">Workbench</span>
          <h2 id="command-dialog-title">Commands</h2>
        </div>
        <button className="icon-button" type="button" onClick={requestClose} aria-label="Close command palette">×</button>
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
        />
      </label>
      <div className="command-results" aria-live="polite">
        {GROUPS.map((group) => {
          const items = filtered.filter((command) => command.group === group)
          if (!items.length) return null
          return <section className="command-group" key={group} aria-labelledby={`command-group-${group.toLowerCase()}`}>
            <h3 id={`command-group-${group.toLowerCase()}`}>{group}</h3>
            <div>
              {items.map((command) => <button
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
