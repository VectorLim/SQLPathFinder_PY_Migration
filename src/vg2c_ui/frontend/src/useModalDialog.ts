import { useEffect, useRef, type MouseEvent } from 'react'

interface Options {
  onOpened?: () => void
}

export function useModalDialog(open: boolean, onClose: () => void, options: Options = {}) {
  const dialogRef = useRef<HTMLDialogElement>(null)
  const openerRef = useRef<HTMLElement | null>(null)
  const onCloseRef = useRef(onClose)
  const onOpenedRef = useRef(options.onOpened)

  onCloseRef.current = onClose
  onOpenedRef.current = options.onOpened

  useEffect(() => {
    const dialog = dialogRef.current
    if (!dialog) return
    if (open && !dialog.open) {
      openerRef.current = document.activeElement instanceof HTMLElement ? document.activeElement : null
      dialog.showModal()
      queueMicrotask(() => onOpenedRef.current?.())
    } else if (!open && dialog.open) {
      dialog.close()
    }
  }, [open])

  function requestClose() {
    const dialog = dialogRef.current
    if (dialog?.open) dialog.close()
    else onCloseRef.current()
  }

  function handleClose() {
    onCloseRef.current()
    const opener = openerRef.current
    openerRef.current = null
    queueMicrotask(() => { if (opener?.isConnected) opener.focus() })
  }

  function handleBackdropClick(event: MouseEvent<HTMLDialogElement>) {
    if (event.target === event.currentTarget) requestClose()
  }

  return { dialogRef, requestClose, handleClose, handleBackdropClick }
}
