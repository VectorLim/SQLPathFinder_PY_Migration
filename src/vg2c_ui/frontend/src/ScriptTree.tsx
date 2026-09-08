import { type KeyboardEvent, type ReactNode } from 'react'

import type {
  DocumentView,
  ParameterView,
  ScopeView,
  SqlActionRequest,
  SqlModelView,
  StepView,
  WorkspaceProjectionView,
} from './contracts.generated'
import { OperationEditor } from './OperationEditor'
import { formatOperationLabel, formatScopeLabel } from './operationLabels'

type ScriptItem = StepView | ScopeView
interface Props {
  tabId: string
  document: DocumentView
  projection: WorkspaceProjectionView | null
  search: string
  expandedScopes: Set<string>
  selectedId: string | null
  values: Record<string, unknown>
  onSelect: (id: string) => void
  onToggleScope: (id: string, expanded?: boolean) => void
  onEdit: (parameter: ParameterView, value: unknown) => void
  inspectSql: (tabId: string, parameterId: string) => Promise<SqlModelView>
  runSqlAction: (tabId: string, parameterId: string, action: SqlActionRequest['action'], args: Record<string, unknown>) => Promise<SqlModelView>
}

export function ScriptTree({ tabId, document, projection, search, expandedScopes, selectedId, values, onSelect, onToggleScope, onEdit, inspectSql, runSqlAction }: Props) {
  const children = childIndex(document)
  const query = search.trim().toLowerCase()
  const visibleIds = query ? searchVisibility(document, query) : null
  const searchExpanded = query ? ancestorSet(document, visibleIds ?? new Set()) : new Set<string>()
  const effectiveExpanded = new Set([...expandedScopes, ...searchExpanded])
  const roots = children.get('root') ?? []
  const selectedVisible = selectedId ? ancestorScopeIds(document, selectedId).every((id) => effectiveExpanded.has(id)) : false
  const focusableId = selectedVisible ? selectedId : roots[0]?.id ?? null
  const projectedArtifacts = projection?.documents.find((item) => item.document_id === document.id)?.artifacts ?? document.artifacts
  const documentIssues = projection?.issues.filter((item) => item.document_id === document.id) ?? []

  function render(items: ScriptItem[], depth: number): ReactNode {
    return items.map((item) => {
      if (visibleIds && !visibleIds.has(item.id)) return null
      const isScope = item.node_kind !== 'step'
      const expanded = isScope && effectiveExpanded.has(item.id)
      const selected = selectedId === item.id
      const diagnostics = item.node_kind === 'step' ? documentIssues.filter((issue) => issue.step_id === item.id) : []
      const hasError = diagnostics.length > 0
      const files = item.node_kind === 'step' ? filesForStep(projectedArtifacts, item.id) : { inputs: [], outputs: [] }
      return <li className={`tree-node${selected ? ' is-selected' : ''}${hasError ? ' has-error' : ''}`} key={item.id} role="none">
        <button type="button" className="tree-row" role="treeitem" aria-level={depth + 1} aria-selected={selected} aria-expanded={isScope ? expanded : undefined} aria-invalid={hasError || undefined} tabIndex={item.id === focusableId ? 0 : -1} data-tree-item data-tree-id={item.id} data-parent-id={item.parent_scope_id ?? ''} onClick={() => { onSelect(item.id); if (isScope) onToggleScope(item.id) }} onKeyDown={(event) => handleTreeKey(event, item, expanded, onToggleScope)}>
          <span className={`tree-toggle${isScope ? '' : ' tree-toggle--step'}`} aria-hidden="true">{isScope ? <span className="chevron">›</span> : item.block_index + 1}</span>
          <TreeLabel item={item} />
          {item.node_kind === 'step' && hasError && <span className="operation-warning" aria-label="Dependency error">!</span>}
          {item.node_kind === 'step' && !hasError && <span className={`operation-state operation-state--${item.validation_state}`} aria-hidden="true" />}
        </button>
        {item.node_kind === 'step' && selected && <OperationEditor tabId={tabId} step={item} values={values} files={files} diagnostics={diagnostics} onEdit={onEdit} inspectSql={inspectSql} runSqlAction={runSqlAction} />}
        {isScope && selected && <ScopeSummary scope={item} document={document} artifacts={projectedArtifacts} />}
        {isScope && <div className={`tree-branch${expanded ? ' is-open' : ''}`} aria-hidden={!expanded}><div className="tree-branch__inner"><ul role="group" className="tree-children">{render(children.get(item.id) ?? [], depth + 1)}</ul></div></div>}
      </li>
    })
  }

  if (!roots.length) return <div className="empty-state"><strong>No operations found</strong><span>The translated script contains no workflow operations.</span></div>
  return <ul className="script-tree" role="tree" aria-label="Script operations">{render(roots, 0)}{query && visibleIds?.size === 0 && <li className="empty-copy">No operations match “{search.trim()}”.</li>}</ul>
}

function TreeLabel({ item }: { item: ScriptItem }) {
  if (item.node_kind !== 'step') return <span className="tree-label"><strong>{formatScopeLabel(item)}</strong><small>Blocks {item.start_index + 1}–{item.end_index + 1}</small></span>
  const label = formatOperationLabel(item)
  return <span className="tree-label"><strong>{label.primary}</strong>{label.secondary && <small>{label.secondary}</small>}</span>
}

function ScopeSummary({ scope, document, artifacts }: { scope: ScopeView; document: DocumentView; artifacts: DocumentView['artifacts'] }) {
  const nested = nestedSteps(document, scope.id)
  const ids = new Set(nested.map((step) => step.id))
  const inputs = unique(artifacts.filter((artifact) => artifact.consumer_step_ids.some((id) => ids.has(id))).map((artifact) => artifact.path))
  const outputs = unique(artifacts.filter((artifact) => artifact.producer_step_ids.some((id) => ids.has(id))).map((artifact) => artifact.path))
  return <section className="scope-summary" aria-label={`${formatScopeLabel(scope)} summary`}><span>{nested.length} nested operation{nested.length === 1 ? '' : 's'}</span>{inputs.length > 0 && <small>Reads {inputs.slice(0, 3).join(', ')}{inputs.length > 3 ? ` +${inputs.length - 3}` : ''}</small>}{outputs.length > 0 && <small>Produces {outputs.slice(0, 3).join(', ')}{outputs.length > 3 ? ` +${outputs.length - 3}` : ''}</small>}</section>
}

function filesForStep(artifacts: DocumentView['artifacts'], stepId: string) {
  return {
    inputs: unique(artifacts.filter((artifact) => artifact.consumer_step_ids.includes(stepId)).map((artifact) => artifact.path)),
    outputs: unique(artifacts.filter((artifact) => artifact.producer_step_ids.includes(stepId)).map((artifact) => artifact.path)),
  }
}

function childIndex(document: DocumentView): Map<string, ScriptItem[]> {
  const children = new Map<string, ScriptItem[]>()
  for (const item of [...document.scopes, ...document.steps]) {
    const parent = item.parent_scope_id ?? 'root'
    children.set(parent, [...(children.get(parent) ?? []), item])
  }
  for (const items of children.values()) items.sort(order)
  return children
}

export function ancestorScopeIds(document: DocumentView, itemId: string): string[] {
  const scopes = new Map(document.scopes.map((scope) => [scope.id, scope]))
  const item = [...document.steps, ...document.scopes].find((candidate) => candidate.id === itemId)
  const result: string[] = []
  let parent = item?.parent_scope_id ?? null
  while (parent) { result.push(parent); parent = scopes.get(parent)?.parent_scope_id ?? null }
  return result
}

function searchVisibility(document: DocumentView, query: string): Set<string> {
  const result = new Set<string>()
  for (const item of [...document.scopes, ...document.steps]) {
    const text = item.node_kind === 'step' ? [formatOperationLabel(item).primary, formatOperationLabel(item).secondary, item.description, item.display_label].filter(Boolean).join(' ') : formatScopeLabel(item)
    if (text.toLowerCase().includes(query)) { result.add(item.id); for (const ancestor of ancestorScopeIds(document, item.id)) result.add(ancestor) }
  }
  return result
}
function ancestorSet(document: DocumentView, ids: Set<string>): Set<string> { const result = new Set<string>(); for (const id of ids) for (const ancestor of ancestorScopeIds(document, id)) result.add(ancestor); return result }
function nestedSteps(document: DocumentView, scopeId: string): StepView[] { const scopes = new Map(document.scopes.map((scope) => [scope.id, scope])); return document.steps.filter((step) => { let parent = step.parent_scope_id; while (parent) { if (parent === scopeId) return true; parent = scopes.get(parent)?.parent_scope_id ?? null } return false }) }
function order(left: ScriptItem, right: ScriptItem): number { const a = left.node_kind === 'step' ? left.block_index : left.start_index; const b = right.node_kind === 'step' ? right.block_index : right.start_index; if (a !== b) return a - b; if (left.node_kind === 'step' && right.node_kind !== 'step') return 1; if (left.node_kind !== 'step' && right.node_kind === 'step') return -1; return left.id.localeCompare(right.id) }
function handleTreeKey(event: KeyboardEvent<HTMLButtonElement>, item: ScriptItem, expanded: boolean, toggle: (id: string, expanded?: boolean) => void) { const items = visibleTreeItems(); const index = items.indexOf(event.currentTarget); if (event.key === 'ArrowDown' && index < items.length - 1) { event.preventDefault(); items[index + 1]?.focus() } else if (event.key === 'ArrowUp' && index > 0) { event.preventDefault(); items[index - 1]?.focus() } else if (event.key === 'Home') { event.preventDefault(); items[0]?.focus() } else if (event.key === 'End') { event.preventDefault(); items.at(-1)?.focus() } else if (event.key === 'ArrowRight' && item.node_kind !== 'step') { event.preventDefault(); if (!expanded) toggle(item.id, true); else items.find((candidate) => candidate.dataset.parentId === item.id)?.focus() } else if (event.key === 'ArrowLeft') { if (item.node_kind !== 'step' && expanded) { event.preventDefault(); toggle(item.id, false); return } const parentId = event.currentTarget.dataset.parentId; if (parentId) { event.preventDefault(); items.find((candidate) => candidate.dataset.treeId === parentId)?.focus() } } }
function visibleTreeItems(): HTMLButtonElement[] { return [...document.querySelectorAll<HTMLButtonElement>('[data-tree-item]')].filter((element) => element.offsetParent !== null && getComputedStyle(element).visibility !== 'hidden') }
function unique(values: string[]): string[] { return [...new Set(values)].sort() }
