import { labelFor, scopeLabel } from './graph'
import type { ScopeNode, StepNode, WorkflowDocument } from './types'

interface WorkflowNavigatorProps {
  document: WorkflowDocument
  search: string
  expandedScopes: Set<string>
  onSelect: (id: string) => void
  onToggleScope: (id: string, expanded: boolean) => void
}

type NavItem = ScopeNode | StepNode

export function WorkflowNavigator({
  document,
  search,
  expandedScopes,
  onSelect,
  onToggleScope,
}: WorkflowNavigatorProps) {
  const query = search.trim().toLocaleLowerCase()
  if (query) {
    const matches: NavItem[] = [...document.scopes, ...document.steps]
      .filter((item) => labelFor(item).toLocaleLowerCase().includes(query))
      .sort(order)
    return (
      <div className="navigator-list">
        {matches.map((item) => <NavButton key={item.id} item={item} onSelect={onSelect} />)}
      </div>
    )
  }

  const children = new Map<string, NavItem[]>()
  for (const item of [...document.scopes, ...document.steps]) {
    const parent = item.parent_scope_id ?? 'root'
    children.set(parent, [...(children.get(parent) ?? []), item])
  }
  for (const items of children.values()) items.sort(order)

  function render(items: NavItem[]): ReactNode {
    return items.map((item) => {
      if (item.node_kind === 'step') {
        return <NavButton key={item.id} item={item} onSelect={onSelect} />
      }
      return (
        <details
          className="scope-tree"
          key={item.id}
          open={expandedScopes.has(item.id)}
          onToggle={(event) => onToggleScope(item.id, event.currentTarget.open)}
        >
          <summary onClick={() => onSelect(item.id)}>
            <span aria-hidden="true">{item.node_kind === 'loop' ? '↻' : '◇'}</span>
            <code>{scopeLabel(item)}</code>
          </summary>
          <div className="scope-children">{render(children.get(item.id) ?? [])}</div>
        </details>
      )
    })
  }

  return (
    <div className="navigator-tree">
      {render(children.get('root') ?? [])}
      {document.artifacts.length > 0 && (
        <details className="data-files">
          <summary>Data files <span>{document.artifacts.length}</span></summary>
          <div className="scope-children">
            {document.artifacts.map((artifact) => (
              <button key={artifact.id} type="button" onClick={() => onSelect(artifact.id)}>
                <span aria-hidden="true">▤</span>{artifact.label}
              </button>
            ))}
          </div>
        </details>
      )}
    </div>
  )
}

function NavButton({ item, onSelect }: { item: NavItem; onSelect: (id: string) => void }) {
  return (
    <button type="button" onClick={() => onSelect(item.id)}>
      <span>{item.node_kind === 'step' ? item.block_index + 1 : '◇'}</span>
      {labelFor(item)}
    </button>
  )
}

function order(left: NavItem, right: NavItem): number {
  const leftIndex = left.node_kind === 'step' ? left.block_index : left.start_index
  const rightIndex = right.node_kind === 'step' ? right.block_index : right.start_index
  if (leftIndex !== rightIndex) return leftIndex - rightIndex
  if (left.node_kind === 'step' && right.node_kind !== 'step') return 1
  if (left.node_kind !== 'step' && right.node_kind === 'step') return -1
  return left.id.localeCompare(right.id)
}
import type { ReactNode } from 'react'
