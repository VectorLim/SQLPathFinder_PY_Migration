import './workbench.css'

import { useEffect, useRef, useState, type PointerEvent as ReactPointerEvent, type ReactNode } from 'react'
import { ChevronLeft, ChevronRight, GripVertical } from 'lucide-react'
import { solvePanes, type SecondaryPane } from './paneSolver'

export type WorkbenchPane = 'logic' | 'config' | 'context'

export function WorkbenchLayout({
  activePane,
  onActivePaneChange,
  logic,
  configuration,
  context,
}: {
  activePane: WorkbenchPane
  onActivePaneChange: (pane: WorkbenchPane) => void
  logic: ReactNode
  configuration: ReactNode
  context: ReactNode
}) {
  const rootRef = useRef<HTMLDivElement>(null)
  const [width, setWidth] = useState(1400)
  const [logicWidth, setLogicWidth] = useState(360)
  const [configWidth, setConfigWidth] = useState(460)
  const [manualConfigCollapsed, setManualConfigCollapsed] = useState(false)
  const [manualContextCollapsed, setManualContextCollapsed] = useState(false)
  const [focusedSecondary, setFocusedSecondary] = useState<SecondaryPane | null>(null)

  useEffect(() => {
    const root = rootRef.current
    if (!root) return
    const observer = new ResizeObserver(([entry]) => setWidth(entry.contentRect.width))
    observer.observe(root)
    return () => observer.disconnect()
  }, [])

  const layout = solvePanes(
    width, logicWidth, configWidth,
    manualConfigCollapsed, manualContextCollapsed, focusedSecondary,
  )
  const { single: narrow, showConfig, showContext } = layout

  useEffect(() => {
    if (narrow) return
    if (activePane === 'config') {
      setManualConfigCollapsed(false)
      setFocusedSecondary('config')
    } else if (activePane === 'context') {
      setManualContextCollapsed(false)
      setFocusedSecondary('context')
    }
  }, [activePane, narrow])

  function toggleSecondary(pane: SecondaryPane) {
    const visible = pane === 'config' ? showConfig : showContext
    if (pane === 'config') setManualConfigCollapsed(visible)
    else setManualContextCollapsed(visible)
    setFocusedSecondary(visible ? null : pane)
  }

  function resizeLogic(delta: number) {
    setLogicWidth((current) => clamp(current + delta, 260, 560))
  }

  function resizeConfig(delta: number) {
    setConfigWidth((current) => clamp(current + delta, 320, 680))
  }

  return <div ref={rootRef} className="adaptive-workbench">
    {narrow ? <>
      <nav className="pane-tabs adaptive-pane-tabs" aria-label="Workbench views">
        {([
          ['logic', 'Script Logic'],
          ['config', 'Configuration'],
          ['context', 'Context'],
        ] as const).map(([id, label]) => <button key={id} type="button" aria-pressed={activePane === id} onClick={() => onActivePaneChange(id)}>{label}</button>)}
      </nav>
      <div className="workbench-panes workbench-panes--single">
        {activePane === 'logic' ? logic : activePane === 'config' ? configuration : context}
      </div>
    </> : <div
      className="workbench-panes adaptive-workbench-panes"
      data-config-collapsed={!showConfig}
      data-context-collapsed={!showContext}
      style={{ gridTemplateColumns: gridColumns(layout.logicWidth, layout.configWidth, showConfig, showContext) }}
    >
      {logic}
      <SeparatorSlot visible={showConfig}>
        <PaneSeparator label="Resize Script Logic and Configuration" value={layout.logicWidth} min={260} max={560} onDelta={resizeLogic} />
      </SeparatorSlot>
      <PaperPaneSlot pane="config" label="Configuration" expanded={showConfig} onToggle={() => toggleSecondary('config')}>
        {configuration}
      </PaperPaneSlot>
      <SeparatorSlot visible={showContext}>
        <PaneSeparator
          label={showConfig ? 'Resize Configuration and Context' : 'Resize Script Logic and Context'}
          value={showConfig ? layout.configWidth : layout.logicWidth}
          min={showConfig ? 320 : 260}
          max={showConfig ? 680 : 560}
          onDelta={showConfig ? resizeConfig : resizeLogic}
        />
      </SeparatorSlot>
      <PaperPaneSlot pane="context" label="Context" expanded={showContext} onToggle={() => toggleSecondary('context')}>
        {context}
      </PaperPaneSlot>
    </div>}
  </div>
}

function PaperPaneSlot({
  pane,
  label,
  expanded,
  onToggle,
  children,
}: {
  pane: SecondaryPane
  label: string
  expanded: boolean
  onToggle: () => void
  children: ReactNode
}) {
  const action = expanded ? 'Collapse' : 'Expand'
  return <div className={`paper-pane-slot paper-pane-slot--${pane} ${expanded ? 'is-expanded' : 'is-collapsed'}`} data-pane-slot={pane}>
    <button
      className="paper-pull-tab"
      type="button"
      aria-label={`${action} ${label}`}
      aria-expanded={expanded}
      aria-controls={pane === 'config' ? 'pane-config' : 'pane-context'}
      title={`${action} ${label}`}
      onClick={onToggle}
    >
      <GripVertical className="paper-pull-tab__grip" size={11} aria-hidden="true" />
      {expanded ? <ChevronRight size={15} aria-hidden="true" /> : <ChevronLeft size={15} aria-hidden="true" />}
    </button>
    <div className="paper-pane-surface" aria-hidden={!expanded}>
      {children}
    </div>
  </div>
}

function SeparatorSlot({ visible, children }: { visible: boolean; children: ReactNode }) {
  return <div className={`pane-separator-slot${visible ? ' is-active' : ''}`}>
    {visible ? children : null}
  </div>
}

function PaneSeparator({
  label,
  value,
  min,
  max,
  onDelta,
}: {
  label: string
  value: number
  min: number
  max: number
  onDelta: (delta: number) => void
}) {
  function pointerDown(event: ReactPointerEvent<HTMLDivElement>) {
    const target = event.currentTarget
    target.setPointerCapture(event.pointerId)
    let lastX = event.clientX
    const move = (moveEvent: PointerEvent) => {
      const delta = moveEvent.clientX - lastX
      lastX = moveEvent.clientX
      onDelta(delta)
    }
    const done = () => {
      target.removeEventListener('pointermove', move)
      target.removeEventListener('pointerup', done)
      target.removeEventListener('pointercancel', done)
    }
    target.addEventListener('pointermove', move)
    target.addEventListener('pointerup', done)
    target.addEventListener('pointercancel', done)
  }

  return <div
    className="pane-separator"
    role="separator"
    aria-label={label}
    aria-orientation="vertical"
    aria-valuemin={min}
    aria-valuemax={max}
    aria-valuenow={Math.round(value)}
    tabIndex={0}
    onPointerDown={pointerDown}
    onKeyDown={(event) => {
      if (event.key === 'ArrowLeft') {
        event.preventDefault()
        onDelta(event.shiftKey ? -48 : -16)
      } else if (event.key === 'ArrowRight') {
        event.preventDefault()
        onDelta(event.shiftKey ? 48 : 16)
      }
    }}
  />
}

function gridColumns(logicWidth: number, configWidth: number, showConfig: boolean, showContext: boolean): string {
  if (showConfig && showContext) return `${logicWidth}px 6px ${configWidth}px 6px minmax(320px, 1fr)`
  if (showConfig) return `${logicWidth}px 6px minmax(320px, 1fr) 0px 0px`
  if (showContext) return `${logicWidth}px 0px 0px 6px minmax(320px, 1fr)`
  return 'minmax(0, 1fr) 0px 0px 0px 0px'
}

function clamp(value: number, min: number, max: number): number {
  return Math.min(max, Math.max(min, value))
}
