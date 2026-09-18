import './themeSelector.css'

import { THEMES, type ThemePreference } from './theme'

interface Props {
  preference: ThemePreference
  onChange: (preference: ThemePreference) => void
}

export function ThemeSelector({ preference, onChange }: Props) {
  return <label className="theme-selector">
    <span className="sr-only">Theme</span>
    <select value={preference} onChange={(event) => onChange(event.target.value as ThemePreference)}>
      <option value="system">System</option>
      {THEMES.map((theme) => <option key={theme.id} value={theme.id}>{theme.label}</option>)}
    </select>
  </label>
}
