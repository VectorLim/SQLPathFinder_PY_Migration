import assert from 'node:assert/strict'

import { solvePanes } from './paneSolver.ts'

const narrowPanes = solvePanes(390, 360, 460, false, false, null)
assert.equal(narrowPanes.single, true)

const twoPanes = solvePanes(800, 360, 460, false, false, null)
assert.deepEqual([twoPanes.showConfig, twoPanes.showContext], [false, true])
assert.ok(twoPanes.logicWidth + 6 + 320 <= 800)

const focusedConfig = solvePanes(800, 360, 460, false, false, 'config')
assert.deepEqual([focusedConfig.showConfig, focusedConfig.showContext], [true, false])

const threePanes = solvePanes(1200, 360, 460, false, false, null)
assert.deepEqual([threePanes.showConfig, threePanes.showContext], [true, true])

const manuallyClosed = solvePanes(1200, 360, 460, true, false, 'config')
assert.deepEqual([manuallyClosed.showConfig, manuallyClosed.showContext], [false, true])

console.log('paneSolver tests passed')
