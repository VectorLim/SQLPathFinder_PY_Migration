import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useReducer,
  useRef,
  type ReactNode,
} from 'react'

import { NotificationViewport } from './NotificationViewport'
import {
  createNotification,
  notificationReducer,
  type NotificationInput,
} from './notifications'

interface NotificationContextValue {
  notify: (input: NotificationInput) => string
  dismiss: (id: string) => void
}

const NotificationContext = createContext<NotificationContextValue | null>(null)

export function NotificationProvider({ children }: { children: ReactNode }) {
  const [notifications, dispatch] = useReducer(notificationReducer, [])
  const timers = useRef(new Map<string, ReturnType<typeof setTimeout>>())
  const sequence = useRef(0)

  const dismiss = useCallback((id: string) => {
    const timer = timers.current.get(id)
    if (timer) clearTimeout(timer)
    timers.current.delete(id)
    dispatch({ type: 'dismiss', id })
  }, [])

  const notify = useCallback((input: NotificationInput) => {
    const id = typeof crypto !== 'undefined' && 'randomUUID' in crypto
      ? crypto.randomUUID()
      : `notification-${Date.now()}-${++sequence.current}`
    const notification = createNotification(input, id)
    dispatch({ type: 'add', notification })
    if (notification.timeoutMs !== null) {
      timers.current.set(id, setTimeout(() => dismiss(id), notification.timeoutMs))
    }
    return id
  }, [dismiss])

  useEffect(() => () => {
    for (const timer of timers.current.values()) clearTimeout(timer)
    timers.current.clear()
  }, [])

  return <NotificationContext.Provider value={{ notify, dismiss }}>
    {children}
    <NotificationViewport notifications={notifications} onDismiss={dismiss} />
  </NotificationContext.Provider>
}

export function useNotifications(): NotificationContextValue {
  const value = useContext(NotificationContext)
  if (!value) throw new Error('useNotifications must be used inside NotificationProvider.')
  return value
}
