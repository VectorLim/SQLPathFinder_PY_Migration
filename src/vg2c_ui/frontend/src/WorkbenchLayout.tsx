import { useEffect, useRef, useState, type ReactNode } from 'react'
import { PanelLeftClose, PanelLeftOpen, PanelRightClose, PanelRightOpen } from 'lucide-react'

export type WorkbenchPane = 'logic' | 'config' | 'context'

export function WorkbenchLayout({
  activePane,
  onActivePaneChange,
  actions,
  logic,
  configuration,
  context,
}: {
  activePane: WorkbenchPane
  onActivePaneChange: (pane: WorkbenchPane) => void
  actions: ReactNode
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

  useEffect(() => {
    const root = rootRef.current
    if (!root) return
    const observer = new ResizeObserver(([entry]) => setWidth(entry.contentRect.width))
    observer.observe(root)
    return () => observer.disconnect()
  }, [])

  useEffect(() => {
    if (activePane === 'config') setManualConfigCollapsed(false)
    if (activePane === 'context') setManualContextCollapsed(false)
  }, [activePane])

  const narrow = width < 720
  const autoConfigCollapsed = width < 1200
  const autoContextCollapsed = width < 930
  const showConfig = !manualConfigCollapsed && !autoConfigCollapsed
  const showContext = !manualContextCollapsed && !autoContextCollapsed

  function resizeLogic(delta: number) {
    setLogicWidth((current) => clamp(current + delta, 260, 560))
  }

  function resizeConfig(delta: number) {
    setConfigWidth((current) => clamp(current + delta, 320, 680))
  }

  return <div ref={rootRef} className="adaptive-workbench">
    {actions}
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
    </> : <>
      <div className="workbench-pane-controls" aria-label="Pane visibility">
        <span>Script Logic</span>
        <button type="button" aria-pressed={showConfig} title={autoConfigCollapsed ? 'Configuration is hidden automatically at this width' : undefined} onClick={() => setManualConfigCollapsed((value) => !value)} disabled={autoConfigCollapsed}>
          {showConfig ? <PanelLeftClose size={14} /> : <PanelLeftOpen size={14} />} Configuration
        </button>
        <button type="button" aria-pressed={showContext} title={autoContextCollapsed ? 'Context is hidden automatically at this width' : undefined} onClick={() => setManualContextCollapsed((value) => !value)} disabled={autoContextCollapsed}>
          {showContext ? <PanelRightClose size={14} /> : <PanelRightOpen size={14} />} Context
        </button>
      </div>
      <div
        className="workbench-panes adaptive-workbench-panes"
        style={{ gridTemplateColumns: gridColumns(logicWidth, configWidth, showConfig, showContext) }}
      >
        {logic}
        {showConfig && <><PaneSeparator label="Resize Script Logic and Configuration" value={logicWidth} min={260} max={560} onDelta={resizeLogic} />{configuration}</>}
        {showContext && <><PaneSeparator label="Resize Configuration and Context" value={configWidth} min={320} max={680} onDelta={resizeConfig} />{context}</>}
      </div>
    </>}
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
  function pointerDown(event: React.PointerEvent<HTMLDivElement>) {
    const startX = event.clientX
    const target = event.currentTarget
    target.setPointerCapture(event.pointerId)
    let lastX = startX
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
  if (showConfig) return `${logicWidth}px 6px minmax(360px, 1fr)`
  if (showContext) return `${logicWidth}px 6px minmax(340px, 1fr)`
  return 'minmax(0, 1fr)'
}

function clamp(value: number, min: number, max: number): number {
  return Math.min(max, Math.max(min, value))
}
