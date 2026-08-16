import type { KeyboardEvent, ReactNode } from 'react'

import { OperationEditor } from './OperationEditor'
import { formatOperationLabel, formatScopeLabel } from './operationLabels'
import type { ParameterDescriptor, ScopeNode, ScriptItem, StepNode, WorkflowDocument } from './types'

interface ScriptTreeProps {
  document: WorkflowDocument
  search: string
  expandedScopes: Set<string>
  selectedId: string | null
  values: Record<string, unknown>
  onSelect: (id: string) => void
  onToggleScope: (id: string, expanded?: boolean) => void
  onEdit: (parameter: ParameterDescriptor, value: unknown) => void
}

export function ScriptTree({
  document,
  search,
  expandedScopes,
  selectedId,
  values,
  onSelect,
  onToggleScope,
  onEdit,
}: ScriptTreeProps) {
  const children = childIndex(document)
  const query = search.trim().toLocaleLowerCase()
  const visibleIds = query ? searchVisibility(document, query) : null
  const searchExpanded = query ? ancestorSet(document, visibleIds ?? new Set()) : new Set<string>()
  const effectiveExpanded = new Set([...expandedScopes, ...searchExpanded])
  const roots = children.get('root') ?? []
  const selectedVisible = selectedId
    ? ancestorScopeIds(document, selectedId).every((id) => effectiveExpanded.has(id))
    : false
  const focusableId = selectedVisible ? selectedId : roots[0]?.id ?? null

  function render(items: ScriptItem[], depth: number): ReactNode {
    return items.map((item) => {
      if (visibleIds && !visibleIds.has(item.id)) return null
      const isScope = item.node_kind !== 'step'
      const expanded = isScope && effectiveExpanded.has(item.id)
      const selected = selectedId === item.id
      const parentId = item.parent_scope_id ?? ''

      return (
        <li className={`tree-node${selected ? ' is-selected' : ''}`} key={item.id} role="none">
          <button
            type="button"
            className="tree-row"
            role="treeitem"
            aria-level={depth + 1}
            aria-selected={selected}
            aria-expanded={isScope ? expanded : undefined}
            tabIndex={item.id === focusableId ? 0 : -1}
            data-tree-item
            data-tree-id={item.id}
            data-parent-id={parentId}
            onClick={() => {
              onSelect(item.id)
              if (isScope) onToggleScope(item.id)
            }}
            onKeyDown={(event) => handleTreeKey(event, item, expanded, onToggleScope)}
          >
            <span className={`tree-toggle${isScope ? '' : ' tree-toggle--step'}`} aria-hidden="true">
              {isScope ? <span className="chevron">›</span> : item.block_index + 1}
            </span>
            <TreeLabel item={item} />
            {item.node_kind === 'step' && (
              <span className={`operation-state operation-state--${item.validation_state}`} aria-hidden="true" />
            )}
          </button>

          {item.node_kind === 'step' && selected && (
            <OperationEditor step={item} values={values} onEdit={onEdit} />
          )}

          {isScope && selected && <ScopeSummary scope={item} document={document} />}

          {isScope && (
            <div className={`tree-branch${expanded ? ' is-open' : ''}`} aria-hidden={!expanded}>
              <div className="tree-branch__inner">
                <ul role="group" className="tree-children">
                  {render(children.get(item.id) ?? [], depth + 1)}
                </ul>
              </div>
            </div>
          )}
        </li>
      )
    })
  }

  if (!roots.length) {
    return <div className="empty-state"><strong>No operations found</strong><span>The translated script contains no editable workflow operations.</span></div>
  }

  return (
    <ul className="script-tree" role="tree" aria-label="Script operations">
      {render(roots, 0)}
      {query && visibleIds?.size === 0 && <li className="empty-copy">No operations match “{search.trim()}”.</li>}
    </ul>
  )
}

function TreeLabel({ item }: { item: ScriptItem }) {
  if (item.node_kind !== 'step') {
    return (
      <span className="tree-label">
        <strong>{formatScopeLabel(item)}</strong>
        <small>Blocks {item.start_index + 1}–{item.end_index + 1}</small>
      </span>
    )
  }
  const label = formatOperationLabel(item)
  return (
    <span className="tree-label">
      <strong>{label.primary}</strong>
      {label.secondary && <small>{label.secondary}</small>}
    </span>
  )
}

function ScopeSummary({ scope, document }: { scope: ScopeNode; document: WorkflowDocument }) {
  const nested = nestedSteps(document, scope.id)
  const inputs = unique(nested.flatMap((step) => step.csv_inputs))
  const outputs = unique(nested.flatMap((step) => step.csv_outputs))
  return (
    <section className="scope-summary" aria-label={`${formatScopeLabel(scope)} summary`}>
      <span>{nested.length} nested operation{nested.length === 1 ? '' : 's'}</span>
      {inputs.length > 0 && <small>Reads {inputs.slice(0, 3).join(', ')}{inputs.length > 3 ? ` +${inputs.length - 3}` : ''}</small>}
      {outputs.length > 0 && <small>Produces {outputs.slice(0, 3).join(', ')}{outputs.length > 3 ? ` +${outputs.length - 3}` : ''}</small>}
    </section>
  )
}

function childIndex(document: WorkflowDocument): Map<string, ScriptItem[]> {
  const children = new Map<string, ScriptItem[]>()
  for (const item of [...document.scopes, ...document.steps]) {
    const parent = item.parent_scope_id ?? 'root'
    children.set(parent, [...(children.get(parent) ?? []), item])
  }
  for (const items of children.values()) items.sort(order)
  return children
}

export function ancestorScopeIds(document: WorkflowDocument, itemId: string): string[] {
  const scopes = new Map(document.scopes.map((scope) => [scope.id, scope]))
  const item = [...document.steps, ...document.scopes].find((candidate) => candidate.id === itemId)
  const result: string[] = []
  let parent = item?.parent_scope_id ?? null
  while (parent) {
    result.push(parent)
    parent = scopes.get(parent)?.parent_scope_id ?? null
  }
  return result
}

function searchVisibility(document: WorkflowDocument, query: string): Set<string> {
  const result = new Set<string>()
  for (const item of [...document.scopes, ...document.steps]) {
    const text = item.node_kind === 'step'
      ? [formatOperationLabel(item).primary, formatOperationLabel(item).secondary, item.description, item.display_label]
        .filter(Boolean).join(' ')
      : formatScopeLabel(item)
    if (text.toLocaleLowerCase().includes(query)) {
      result.add(item.id)
      for (const ancestor of ancestorScopeIds(document, item.id)) result.add(ancestor)
    }
  }
  return result
}

function ancestorSet(document: WorkflowDocument, ids: Set<string>): Set<string> {
  const result = new Set<string>()
  for (const id of ids) {
    for (const ancestor of ancestorScopeIds(document, id)) result.add(ancestor)
  }
  return result
}

function nestedSteps(document: WorkflowDocument, scopeId: string): StepNode[] {
  const scopes = new Map(document.scopes.map((scope) => [scope.id, scope]))
  return document.steps.filter((step) => {
    let parent = step.parent_scope_id
    while (parent) {
      if (parent === scopeId) return true
      parent = scopes.get(parent)?.parent_scope_id ?? null
    }
    return false
  })
}

function order(left: ScriptItem, right: ScriptItem): number {
  const leftIndex = left.node_kind === 'step' ? left.block_index : left.start_index
  const rightIndex = right.node_kind === 'step' ? right.block_index : right.start_index
  if (leftIndex !== rightIndex) return leftIndex - rightIndex
  if (left.node_kind === 'step' && right.node_kind !== 'step') return 1
  if (left.node_kind !== 'step' && right.node_kind === 'step') return -1
  return left.id.localeCompare(right.id)
}

function handleTreeKey(
  event: KeyboardEvent<HTMLButtonElement>,
  item: ScriptItem,
  expanded: boolean,
  onToggleScope: (id: string, expanded?: boolean) => void,
) {
  const key = event.key
  const items = visibleTreeItems()
  const index = items.indexOf(event.currentTarget)
  if (key === 'ArrowDown' && index < items.length - 1) {
    event.preventDefault()
    items[index + 1]?.focus()
  } else if (key === 'ArrowUp' && index > 0) {
    event.preventDefault()
    items[index - 1]?.focus()
  } else if (key === 'Home') {
    event.preventDefault()
    items[0]?.focus()
  } else if (key === 'End') {
    event.preventDefault()
    items.at(-1)?.focus()
  } else if (key === 'ArrowRight' && item.node_kind !== 'step') {
    event.preventDefault()
    if (!expanded) onToggleScope(item.id, true)
    else items.find((candidate) => candidate.dataset.parentId === item.id)?.focus()
  } else if (key === 'ArrowLeft') {
    if (item.node_kind !== 'step' && expanded) {
      event.preventDefault()
      onToggleScope(item.id, false)
      return
    }
    const parentId = event.currentTarget.dataset.parentId
    if (parentId) {
      event.preventDefault()
      items.find((candidate) => candidate.dataset.treeId === parentId)?.focus()
    }
  }
}

function visibleTreeItems(): HTMLButtonElement[] {
  return [...document.querySelectorAll<HTMLButtonElement>('[data-tree-item]')]
    .filter((element) => element.offsetParent !== null && getComputedStyle(element).visibility !== 'hidden')
}

function unique(values: string[]): string[] {
  return [...new Set(values)].sort()
}
