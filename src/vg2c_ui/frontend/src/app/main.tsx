import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'

import { App } from './App'
import { initializeTheme } from '../design/theme/theme'
import '../design/base.css'
import '../design/theme/theme.css'
import '../design/motion.css'

initializeTheme()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
