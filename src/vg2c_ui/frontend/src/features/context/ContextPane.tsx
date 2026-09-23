import './context.css'

import { useState, type KeyboardEvent, type ReactNode } from 'react'
import { FileClock, Globe2, Mail } from 'lucide-react'

import type { ContextProps } from './contextTypes'
import { EmailContext, isEmail } from './EmailContext'
import { FileContext } from './FileContext'
import { GlobalsContext } from './GlobalsContext'

type ContextTab = 'files' | 'email' | 'globals'

export function ContextPane(props: ContextProps) {
  const [tab, setTab] = useState<ContextTab>('files')
  const emailCount = props.document.semantic_operations.filter(isEmail).length
  return <section className="context-pane">
    <nav className="context-tabs" aria-label="Context views" role="tablist">
      <ContextTabButton id="files" current={tab} onSelect={setTab} icon={<FileClock size={15} />}>File Flow</ContextTabButton>
      <ContextTabButton id="email" current={tab} onSelect={setTab} icon={<Mail size={15} />}>Email{emailCount ? ` (${emailCount})` : ''}</ContextTabButton>
      <ContextTabButton id="globals" current={tab} onSelect={setTab} icon={<Globe2 size={15} />}>Globals</ContextTabButton>
    </nav>
    <div className="context-content">
      {tab === 'files' && <FileContext {...props} />}
      {tab === 'email' && <EmailContext key={props.document.id} document={props.document} values={props.values} onNavigate={props.onNavigate} onEdit={props.onEdit} />}
      {tab === 'globals' && <GlobalsContext document={props.document} values={props.values} onNavigate={props.onNavigate} onEdit={props.onEdit} />}
    </div>
  </section>
}

const CONTEXT_TABS: ContextTab[] = ['files', 'email', 'globals']

function ContextTabButton({ id, current, onSelect, icon, children }: { id: ContextTab; current: ContextTab; onSelect: (id: ContextTab) => void; icon: ReactNode; children: ReactNode }) {
  const index = CONTEXT_TABS.indexOf(id)
  return <button
    type="button"
    role="tab"
    aria-selected={current === id}
    tabIndex={current === id ? 0 : -1}
    onClick={() => onSelect(id)}
    onKeyDown={(event) => handleContextTabKey(event, index, onSelect)}
  >{icon}<span>{children}</span></button>
}

function handleContextTabKey(event: KeyboardEvent<HTMLButtonElement>, index: number, onSelect: (id: ContextTab) => void) {
  if (!['ArrowLeft', 'ArrowRight', 'Home', 'End'].includes(event.key)) return
  event.preventDefault()
  let nextIndex = index
  if (event.key === 'ArrowLeft') nextIndex = (index - 1 + CONTEXT_TABS.length) % CONTEXT_TABS.length
  if (event.key === 'ArrowRight') nextIndex = (index + 1) % CONTEXT_TABS.length
  if (event.key === 'Home') nextIndex = 0
  if (event.key === 'End') nextIndex = CONTEXT_TABS.length - 1
  const next = CONTEXT_TABS[nextIndex]
  if (!next) return
  onSelect(next)
  event.currentTarget.closest('[role="tablist"]')?.querySelectorAll<HTMLButtonElement>('[role="tab"]')[nextIndex]?.focus()
}
