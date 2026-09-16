import { useEffect, useState } from 'react'

export const THEMES = [
  { id: 'light', label: 'Light', colorScheme: 'light' },
  { id: 'dark', label: 'Dark', colorScheme: 'dark' },
] as const

export type ThemeName = typeof THEMES[number]['id']
export type ThemePreference = 'system' | ThemeName

const STORAGE_KEY = 'pythonpathfinder.theme'

export function initializeTheme(): ThemeName {
  return applyTheme(readThemePreference())
}

export function useTheme() {
  const [preference, setPreferenceState] = useState<ThemePreference>(readThemePreference)

  useEffect(() => {
    const media = window.matchMedia('(prefers-color-scheme: dark)')
    const apply = () => { applyTheme(preference) }
    apply()
    if (preference !== 'system') return
    media.addEventListener('change', apply)
    return () => media.removeEventListener('change', apply)
  }, [preference])

  function setPreference(next: ThemePreference) {
    setPreferenceState(next)
    try { window.localStorage.setItem(STORAGE_KEY, next) } catch { /* storage is optional */ }
  }

  return { preference, setPreference }
}

export function readThemePreference(): ThemePreference {
  try {
    const stored = window.localStorage.getItem(STORAGE_KEY)
    if (stored === 'system' || THEMES.some((theme) => theme.id === stored)) return stored as ThemePreference
  } catch { /* fall through to system */ }
  return 'system'
}

function resolveTheme(preference: ThemePreference): ThemeName {
  if (preference !== 'system') return preference
  return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light'
}

function applyTheme(preference: ThemePreference): ThemeName {
  const theme = resolveTheme(preference)
  const definition = THEMES.find((item) => item.id === theme) ?? THEMES[0]
  document.documentElement.dataset.theme = theme
  document.documentElement.style.colorScheme = definition.colorScheme
  return theme
}
