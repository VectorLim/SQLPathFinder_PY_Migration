export type SecondaryPane = 'config' | 'context'

export interface PaneConstraints {
  logicMin: number
  configMin: number
  contextMin: number
  separator: number
}

export interface PaneSolution {
  single: boolean
  showConfig: boolean
  showContext: boolean
  logicWidth: number
  configWidth: number
}

export const PANE_CONSTRAINTS: PaneConstraints = {
  logicMin: 260,
  configMin: 320,
  contextMin: 320,
  separator: 6,
}

export function solvePanes(
  width: number,
  preferredLogicWidth: number,
  preferredConfigWidth: number,
  manualConfigCollapsed: boolean,
  manualContextCollapsed: boolean,
  focusedSecondary: SecondaryPane | null,
  constraints: PaneConstraints = PANE_CONSTRAINTS,
): PaneSolution {
  const { logicMin, configMin, contextMin, separator } = constraints
  const minPair = logicMin + Math.min(configMin, contextMin) + separator
  if (width < minPair) {
    return { single: true, showConfig: false, showContext: false,
      logicWidth: width, configWidth: 0 }
  }

  const bothFit = width >= logicMin + configMin + contextMin + separator * 2
  const showBoth = bothFit && !manualConfigCollapsed && !manualContextCollapsed
  let showConfig = showBoth
  let showContext = showBoth
  if (!showBoth) {
    if (focusedSecondary === 'config' && !manualConfigCollapsed) showConfig = true
    else if (focusedSecondary === 'context' && !manualContextCollapsed) showContext = true
    else if (!manualContextCollapsed) showContext = true
    else if (!manualConfigCollapsed) showConfig = true
  }

  if (showConfig && showContext) {
    const logicWidth = Math.min(Math.max(preferredLogicWidth, logicMin),
      width - configMin - contextMin - separator * 2)
    const configWidth = Math.min(Math.max(preferredConfigWidth, configMin),
      width - logicWidth - contextMin - separator * 2)
    return { single: false, showConfig, showContext, logicWidth, configWidth }
  }
  const remainingMin = showConfig ? configMin : showContext ? contextMin : 0
  const logicWidth = remainingMin
    ? Math.min(Math.max(preferredLogicWidth, logicMin), width - remainingMin - separator)
    : width
  return { single: false, showConfig, showContext, logicWidth, configWidth: 0 }
}
