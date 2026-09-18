import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { App } from './App'
import { initializeTheme } from './theme'
import './styles.css'
import './theme.css'
import './motion.css'

initializeTheme()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
