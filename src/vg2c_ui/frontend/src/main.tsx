import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { App } from './App'
import { NotificationProvider } from './NotificationProvider'
import './styles.css'
import './sql/sqlResponsive.css'
import './appShell.css'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <NotificationProvider>
      <App />
    </NotificationProvider>
  </StrictMode>,
)
