import { useEffect, useRef, useState, type KeyboardEvent } from 'react'

interface SqlFieldProps {
  value: string
  ariaLabel: string
  onCommit: (value: string) => boolean
  disabled?: boolean
  className?: string
  list?: string
  placeholder?: string
}

export function SqlField({
  value,
  ariaLabel,
  onCommit,
  disabled = false,
  className,
  list,
  placeholder,
}: SqlFieldProps) {
  const [draft, setDraft] = useState(value)
  const cancelBlurCommit = useRef(false)
  useEffect(() => setDraft(value), [value])

  function commit() {
    if (draft === value) return true
    const accepted = onCommit(draft)
    if (!accepted) setDraft(value)
    return accepted
  }

  function handleKeyDown(event: KeyboardEvent<HTMLInputElement>) {
    if (event.key === 'Enter') {
      event.preventDefault()
      event.currentTarget.blur()
    } else if (event.key === 'Escape') {
      event.preventDefault()
      cancelBlurCommit.current = true
      setDraft(value)
      event.currentTarget.blur()
    }
  }

  return (
    <input
      className={className}
      type="text"
      value={draft}
      aria-label={ariaLabel}
      disabled={disabled}
      list={list}
      placeholder={placeholder}
      onChange={(event) => setDraft(event.target.value)}
      onBlur={() => {
        if (cancelBlurCommit.current) {
          cancelBlurCommit.current = false
          setDraft(value)
          return
        }
        commit()
      }}
      onKeyDown={handleKeyDown}
    />
  )
}
