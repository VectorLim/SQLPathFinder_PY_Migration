export const notificationVariants = {
  success: { icon: '✓', defaultTimeoutMs: 4500, live: 'polite' },
  info: { icon: 'i', defaultTimeoutMs: 5000, live: 'polite' },
  warning: { icon: '!', defaultTimeoutMs: 7000, live: 'polite' },
  error: { icon: '!', defaultTimeoutMs: null, live: 'assertive' },
  progress: { icon: '…', defaultTimeoutMs: null, live: 'polite' },
} as const

export type NotificationType = keyof typeof notificationVariants

export interface NotificationInput {
  type: NotificationType
  title: string
  message?: string
  timeoutMs?: number | null
}

export interface NotificationItem extends NotificationInput {
  id: string
  timeoutMs: number | null
}

export type NotificationAction =
  | { type: 'add'; notification: NotificationItem }
  | { type: 'dismiss'; id: string }
  | { type: 'clear' }

export function createNotification(input: NotificationInput, id: string): NotificationItem {
  return {
    ...input,
    id,
    timeoutMs: input.timeoutMs === undefined
      ? notificationVariants[input.type].defaultTimeoutMs
      : input.timeoutMs,
  }
}

export function notificationReducer(
  state: NotificationItem[],
  action: NotificationAction,
): NotificationItem[] {
  if (action.type === 'add') return [...state, action.notification]
  if (action.type === 'dismiss') return state.filter((item) => item.id !== action.id)
  if (action.type === 'clear') return []
  return state
}
