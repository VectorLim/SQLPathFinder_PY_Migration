import { defineConfig } from '@playwright/test'
import { mkdtempSync } from 'node:fs'
import { createServer } from 'node:net'
import { tmpdir } from 'node:os'
import { join, resolve } from 'node:path'

async function freePort(): Promise<number> {
  const server = createServer()
  await new Promise<void>((done) => server.listen(0, '127.0.0.1', done))
  const address = server.address()
  if (!address || typeof address === 'string') throw new Error('No local test port available')
  await new Promise<void>((done) => server.close(() => done()))
  return address.port
}

const apiPort = process.env.VG2C_TEST_API_PORT ??= String(await freePort())
const uiPort = process.env.VG2C_TEST_UI_PORT ??= String(await freePort())
process.env.NO_PROXY = [process.env.NO_PROXY, process.env.no_proxy, '127.0.0.1', 'localhost'].filter(Boolean).join(',')
process.env.no_proxy = process.env.NO_PROXY
const root = process.env.VG2C_TEST_ROOT ??= mkdtempSync(join(tmpdir(), 'vg2c-browser-'))
const python = process.env.VG2C_PYTHON ?? resolve('../../../.venv', process.platform === 'win32' ? 'Scripts/python.exe' : 'bin/python')
const channel = process.env.VG2C_BROWSER_CHANNEL ?? (process.platform === 'win32' ? 'msedge' : undefined)

export default defineConfig({
  testDir: '.',
  testMatch: 'workbench.spec.ts',
  outputDir: join(root, 'results'),
  workers: 1,
  timeout: 60_000,
  use: { baseURL: `http://127.0.0.1:${uiPort}`, trace: 'retain-on-failure' },
  projects: [
    { name: 'desktop', use: { browserName: 'chromium', channel, viewport: { width: 1440, height: 900 } } },
    { name: 'tablet', use: { browserName: 'chromium', channel, viewport: { width: 1024, height: 768 } } },
    { name: 'mobile', use: { browserName: 'chromium', channel, viewport: { width: 390, height: 844 }, reducedMotion: 'reduce' } },
    { name: 'firefox', use: { browserName: 'firefox', viewport: { width: 1440, height: 900 } } },
  ],
  webServer: [
    { command: `"${python}" -m vg2c_ui --host 127.0.0.1 --port ${apiPort} --data-dir "${root}"`, url: `http://127.0.0.1:${apiPort}/api/health`, reuseExistingServer: false },
    { command: `npm run dev -- --host 127.0.0.1 --port ${uiPort} --strictPort`, url: `http://127.0.0.1:${uiPort}`, env: { VG2C_API_URL: `http://127.0.0.1:${apiPort}` }, reuseExistingServer: false },
  ],
})