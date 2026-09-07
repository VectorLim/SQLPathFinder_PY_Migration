import { useRef, type ChangeEvent } from 'react'

interface Props {
  onFiles: (files: File[]) => void
  disabled?: boolean
}

export function AddFilesButton({ onFiles, disabled = false }: Props) {
  const inputRef = useRef<HTMLInputElement>(null)

  function selectFiles(event: ChangeEvent<HTMLInputElement>) {
    const files = Array.from(event.target.files ?? [])
    event.target.value = ''
    if (files.length) onFiles(files)
  }

  return <>
    <input
      ref={inputRef}
      className="sr-only"
      type="file"
      accept=".txt,.vg2,text/plain"
      multiple
      tabIndex={-1}
      aria-hidden="true"
      onChange={selectFiles}
    />
    <button
      className="add-files-button"
      type="button"
      aria-label="Add VG2 files"
      title="Add VG2 files"
      data-tooltip="Add VG2 files"
      disabled={disabled}
      onClick={() => inputRef.current?.click()}
    >
      <span aria-hidden="true">+</span>
    </button>
  </>
}
