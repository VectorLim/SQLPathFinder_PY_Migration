import { ArrowRightLeft, Copy, Eye, FileInput, FileOutput, GitMerge, HelpCircle, Shuffle, Trash2, type LucideIcon } from 'lucide-react'
import type { FileEffectView } from '../contracts.generated'

export const flowRenderers = {
  read: { label: 'Read', icon: FileInput },
  observe: { label: 'Observe', icon: Eye },
  write: { label: 'Create or overwrite', icon: FileOutput },
  copy: { label: 'Copy', icon: Copy },
  move: { label: 'Move or replace', icon: ArrowRightLeft },
  transform: { label: 'Transform', icon: Shuffle },
  append: { label: 'Create or append', icon: GitMerge },
  delete: { label: 'Delete', icon: Trash2 },
  unknown: { label: 'Unknown effects', icon: HelpCircle },
} satisfies Record<FileEffectView['kind'], { label: string; icon: LucideIcon }>