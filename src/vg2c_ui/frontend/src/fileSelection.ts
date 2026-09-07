import type { TranslationFileRequest } from './contracts.generated'

export const supportedSourceExtensions = ['.txt', '.vg2'] as const

export interface SourceFileLike {
  name: string
  text: () => Promise<string>
}

export interface RejectedSourceFile {
  name: string
  message: string
}

export interface PreparedSourceFiles {
  accepted: TranslationFileRequest[]
  rejected: RejectedSourceFile[]
}

export function isSupportedSourceFile(name: string): boolean {
  const lower = name.toLowerCase()
  return supportedSourceExtensions.some((extension) => lower.endsWith(extension))
}

export async function prepareSourceFiles(files: readonly SourceFileLike[]): Promise<PreparedSourceFiles> {
  const accepted: TranslationFileRequest[] = []
  const rejected: RejectedSourceFile[] = []
  const names = new Set<string>()

  for (const file of files) {
    if (!isSupportedSourceFile(file.name)) {
      rejected.push({
        name: file.name,
        message: 'Unsupported VG2 source file type. Select a .txt or .vg2 file.',
      })
      continue
    }
    const key = file.name.toLowerCase()
    if (names.has(key)) {
      rejected.push({ name: file.name, message: 'Duplicate file name in selection.' })
      continue
    }
    names.add(key)
    try {
      accepted.push({ name: file.name, content: await file.text() })
    } catch (error) {
      rejected.push({
        name: file.name,
        message: error instanceof Error ? error.message : 'Could not read the selected file.',
      })
    }
  }

  return { accepted, rejected }
}
