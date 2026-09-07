import assert from 'node:assert/strict'
import { readFileSync } from 'node:fs'

const shell = readFileSync(new URL('./appShell.css', import.meta.url), 'utf8')
const sql = readFileSync(new URL('./sql/sqlResponsive.css', import.meta.url), 'utf8')
const app = readFileSync(new URL('./App.tsx', import.meta.url), 'utf8')

assert.match(shell, /overflow-x:\s*clip/, 'page shell must guard against unintended horizontal page overflow')
assert.match(shell, /grid-template-columns:\s*minmax\(0, 1fr\)/, 'workspace primary column must be shrinkable')
assert.match(shell, /env\(safe-area-inset-left\)/, 'floating controls must respect mobile safe areas')
assert.match(shell, /\.add-files-button\s*\{[\s\S]*position:\s*fixed/, 'add button must remain visible while navigating')
assert.match(shell, /\.context-sidebar\s*\{[\s\S]*transform:\s*translateY/, 'narrow context UI must become a bottom sheet')
assert.match(shell, /\.notification-viewport\s*\{[\s\S]*width:\s*min\(/, 'notification stack must stay inside the viewport')
assert.match(sql, /container-type:\s*inline-size/, 'structured SQL must respond to its actual container width')
assert.match(sql, /@container\s*\(max-width:\s*760px\)/, 'structured SQL must reflow before fixed controls can clip')
assert.doesNotMatch(app, /translate-box|VG2 source paths|parseSourcePaths/, 'obsolete path-entry UI must be removed from App')
assert.doesNotMatch(app, />Open<|>Translate</, 'obsolete Open and Translate buttons must be removed')

console.log('responsive layout tests passed')
