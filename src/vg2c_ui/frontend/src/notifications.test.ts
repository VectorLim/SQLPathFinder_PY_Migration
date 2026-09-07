import assert from 'node:assert/strict'
import { createNotification, notificationReducer, notificationVariants } from './notifications.ts'

assert.deepEqual(
  Object.keys(notificationVariants),
  ['success', 'info', 'warning', 'error', 'progress'],
  'notification variants must be registered centrally',
)

const success = createNotification({ type: 'success', title: 'Done' }, 'stable-success')
const error = createNotification({ type: 'error', title: 'Failed', message: 'boom' }, 'stable-error')
assert.equal(success.id, 'stable-success')
assert.equal(success.timeoutMs, notificationVariants.success.defaultTimeoutMs)
assert.equal(error.timeoutMs, null, 'errors should remain until dismissed by default')

let state = notificationReducer([], { type: 'add', notification: success })
state = notificationReducer(state, { type: 'add', notification: error })
assert.deepEqual(state.map((item) => item.id), ['stable-success', 'stable-error'], 'notifications must stack predictably')
state = notificationReducer(state, { type: 'dismiss', id: 'stable-success' })
assert.deepEqual(state.map((item) => item.id), ['stable-error'], 'dismissal must only remove the selected notification')
state = notificationReducer(state, { type: 'clear' })
assert.deepEqual(state, [])

const persistentInfo = createNotification({ type: 'info', title: 'Pinned', timeoutMs: null }, 'custom-timeout')
assert.equal(persistentInfo.timeoutMs, null, 'callers may override a variant timeout without changing rendering code')

console.log('notification tests passed')
