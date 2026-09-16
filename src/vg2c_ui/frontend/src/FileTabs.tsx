import type { KeyboardEvent } from 'react'

import { baseName } from './operationLabels'
import type { TabState } from './workspaceState'

interface Props {
  tabs: TabState[]
  activeId: string | null
  onActivate: (id: string) => void
  onClose: (id: string) => void
}

export function FileTabs({ tabs, activeId, onActivate, onClose }: Props) {
  return <nav className="tabs" aria-label="Open translated files" role="tablist">
    {tabs.map((tab, index) => {
      const active = tab.document.id === activeId
      const name = baseName(tab.document.output_path || tab.document.source_path)
      return <div className={`tab${active ? ' is-active' : ''}`} key={tab.document.id}>
        <button
          type="button"
          role="tab"
          aria-selected={active}
          aria-controls="script-workspace"
          tabIndex={active ? 0 : -1}
          title={tab.document.output_path}
          onClick={() => onActivate(tab.document.id)}
          onKeyDown={(event) => handleTabKeys(event, tabs, index, onActivate)}
        >
          <span className="tab-name">{name}</span>
          <span className={`tab-state tab-state--${tab.status}`} aria-hidden="true" />
          <span className="sr-only">{tab.status}</span>
        </button>
        <button
          className="tab-close"
          type="button"
          tabIndex={active ? 0 : -1}
          onClick={() => onClose(tab.document.id)}
          aria-label={`Close ${name}`}
        >×</button>
      </div>
    })}
  </nav>
}

function handleTabKeys(
  event: KeyboardEvent<HTMLButtonElement>,
  tabs: TabState[],
  index: number,
  activate: (id: string) => void,
) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  let nextIndex = index
  if (event.key === 'ArrowLeft') nextIndex = (index - 1 + tabs.length) % tabs.length
  if (event.key === 'ArrowRight') nextIndex = (index + 1) % tabs.length
  if (event.key === 'Home') nextIndex = 0
  if (event.key === 'End') nextIndex = tabs.length - 1
  const next = tabs[nextIndex]
  if (!next) return
  activate(next.document.id)
  event.currentTarget.closest('[role="tablist"]')?.querySelectorAll<HTMLButtonElement>('[role="tab"]')[nextIndex]?.focus()
}
