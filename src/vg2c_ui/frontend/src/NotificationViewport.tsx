import { notificationVariants, type NotificationItem } from './notifications'

interface Props {
  notifications: NotificationItem[]
  onDismiss: (id: string) => void
}

export function NotificationViewport({ notifications, onDismiss }: Props) {
  return <aside className="notification-viewport" aria-label="Notifications">
    {notifications.map((notification) => {
      const variant = notificationVariants[notification.type]
      return <section
        className={`notification notification--${notification.type}`}
        key={notification.id}
        role={variant.live === 'assertive' ? 'alert' : 'status'}
        aria-live={variant.live}
        aria-atomic="true"
      >
        <span className="notification__icon" aria-hidden="true">{variant.icon}</span>
        <div className="notification__copy">
          <strong>{notification.title}</strong>
          {notification.message && <p>{notification.message}</p>}
        </div>
        <button
          className="notification__dismiss"
          type="button"
          onClick={() => onDismiss(notification.id)}
          aria-label={`Dismiss ${notification.title}`}
        >×</button>
      </section>
    })}
  </aside>
}
