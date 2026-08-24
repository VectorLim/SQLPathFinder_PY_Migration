import { createHash } from 'node:crypto'
import { existsSync, mkdtempSync, readFileSync, rmSync, writeFileSync } from 'node:fs'
import { tmpdir } from 'node:os'
import { dirname, join, resolve } from 'node:path'
import { fileURLToPath } from 'node:url'
import { spawnSync } from 'node:child_process'

const here = dirname(fileURLToPath(import.meta.url))
const repoRoot = resolve(here, '../..')
const frontendRoot = join(repoRoot, 'src/vg2c_ui/frontend')
const tsc = join(frontendRoot, 'node_modules/typescript/bin/tsc')
const runner = join(here, 'sql_parity_capture.ts')
const fixturePath = join(repoRoot, 'tests/fixtures/sql_semantic_parity.v1.json')
const write = process.argv.includes('--write')

if (!existsSync(tsc)) {
  throw new Error('TypeScript is not installed. Run npm install in src/vg2c_ui/frontend first.')
}

const buildRoot = mkdtempSync(join(tmpdir(), 'sqlpathfinder-parity-'))
try {
  run(process.execPath, [
    tsc,
    '--target', 'ES2022',
    '--module', 'commonjs',
    '--moduleResolution', 'node',
    '--strict',
    '--skipLibCheck',
    '--rootDir', repoRoot,
    '--outDir', buildRoot,
    runner,
  ])

  const compiled = join(buildRoot, 'tests/parity/sql_parity_capture.js')
  const captured = spawnSync(process.execPath, [compiled], { encoding: 'utf8' })
  if (captured.status !== 0) {
    throw new Error(captured.stderr || 'Parity fixture runner failed.')
  }

  const payload = JSON.parse(captured.stdout)
  const fixtureHash = createHash('sha256')
    .update(JSON.stringify(stable(payload)))
    .digest('hex')
  const content = `${JSON.stringify({ ...payload, fixture_hash: `sha256:${fixtureHash}` })}\n`

  if (write) {
    writeFileSync(fixturePath, content, 'utf8')
    console.log(`Updated ${fixturePath}`)
  } else {
    const expected = readFileSync(fixturePath, 'utf8')
    if (content !== expected) {
      throw new Error('SQL parity fixture is stale. Run node tests/parity/capture_sql_parity.mjs --write and review the diff.')
    }
    console.log('SQL parity fixture matches the current TypeScript semantics.')
  }
} finally {
  rmSync(buildRoot, { recursive: true, force: true })
}

function run(command, args) {
  const result = spawnSync(command, args, { encoding: 'utf8' })
  if (result.status !== 0) {
    throw new Error(result.stderr || `${command} failed.`)
  }
}

function stable(value) {
  if (Array.isArray(value)) return value.map(stable)
  if (value && typeof value === 'object') {
    return Object.fromEntries(Object.keys(value).sort().map((key) => [key, stable(value[key])]))
  }
  return value
}
