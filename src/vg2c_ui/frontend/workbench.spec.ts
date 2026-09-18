import { expect, test, type Page } from '@playwright/test'
import { writeFile } from 'node:fs/promises'
import { dirname, join } from 'node:path'
import type { DocumentView } from './src/contracts.generated'

async function pane(page: Page, name: string) {
  const button = page.getByRole('navigation', { name: 'Workbench views' }).getByRole('button', { name, exact: true })
  if (await button.isVisible()) await button.click()
}

async function translate(page: Page) {
  await page.goto('/')
  await expect(page.getByRole('button', { name: 'Upload files', exact: true })).toBeEnabled()
  await page.locator('input[type=file]').first().setInputFiles({
    name: 'workbench.txt', mimeType: 'text/plain',
    buffer: Buffer.from('<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1 AS value\n<---- New Query ---->\n'),
  })
  await page.getByRole('button', { name: /Upload queued/ }).click()
  await page.getByRole('checkbox', { name: 'inputs/workbench.txt', exact: true }).check()
  await page.getByRole('button', { name: 'Translate selected', exact: true }).click()
  await expect(page.getByRole('treeitem').first()).toBeVisible()
  await page.getByRole('treeitem').first().click()
  await pane(page, 'Configuration')
  await expect(page.getByLabel('Output', { exact: true })).toBeVisible()
  const tabs = await page.locator('.tabs').boundingBox()
  expect(tabs?.height).toBe(44)
  const intake = await page.getByRole('region', { name: 'Workspace source intake' }).boundingBox()
  expect(intake!.y + intake!.height).toBeLessThanOrEqual(tabs!.y + 1)
}

test('editing, persistence, preview and responsive layout', async ({ page }, testInfo) => {
  const errors: string[] = []
  page.on('pageerror', (error) => errors.push(error.message))
  await translate(page)
  const output = page.getByLabel('Output', { exact: true })
  await output.fill('renamed.csv')
  await page.getByRole('button', { name: 'Undo', exact: true }).click()
  await expect(output).toHaveValue('out.csv')
  await page.getByRole('button', { name: 'Redo', exact: true }).click()
  await expect(output).toHaveValue('renamed.csv')

  const expression = page.getByRole('textbox', { name: 'Selected expression', exact: true }).first()
  await expect(expression).toBeEnabled()
  await expression.fill('2')
  await expression.press('Tab')
  await expect(page.getByRole('button', { name: 'Reset SQL to generated value' })).toBeEnabled()
  await page.getByRole('button', { name: 'Reset SQL to generated value' }).click()
  await expect(expression).toHaveValue('1')
  await expect(output).toHaveValue('renamed.csv')

  await page.getByRole('button', { name: 'Preview', exact: true }).click()
  const apply = page.getByRole('button', { name: 'Apply', exact: true })
  await expect(apply).toBeEnabled()
  let releaseSave!: () => void
  const saveGate = new Promise<void>((resolve) => { releaseSave = resolve })
  await page.route('**/api/changes/apply', async (route) => { await saveGate; await route.continue() })
  await apply.click()
  await expect(output).toBeDisabled()
  await expect(page.getByRole('button', { name: 'Undo', exact: true })).toBeDisabled()
  releaseSave()
  await expect(page.getByText('No pending changes', { exact: true })).toBeVisible()
  await expect(output).toBeEnabled()

  const reopened = await page.request.post('/api/documents/open', { data: { source_path: 'inputs/workbench.txt', output_path: 'generated/inputs/workbench.py' } })
  expect(reopened.ok()).toBeTruthy()
  const reopenedDocument: DocumentView = await reopened.json()
  expect(reopenedDocument.steps.flatMap((step) => step.operations).flatMap((operation) => operation.parameters).find((parameter) => parameter.name === 'output')?.value).toBe('renamed.csv')

  await pane(page, 'File Flow')
  await page.getByRole('button', { name: 'Preview on-disk renamed.csv', exact: true }).click()
  await expect(page.locator('.on-disk-preview [role=alert]')).toBeVisible()
  const session = (await page.context().cookies()).find((cookie) => cookie.name === 'vg2c_workspace')!
  await writeFile(join(process.env.VG2C_TEST_ROOT!, 'workspaces', session.value, dirname(reopenedDocument.output_path), 'renamed.csv'), 'value\nverified\n')
  await page.getByRole('button', { name: 'Preview on-disk renamed.csv', exact: true }).click()
  await expect(page.getByRole('cell', { name: 'verified', exact: true })).toBeVisible()
  await page.screenshot({ path: testInfo.outputPath('file-flow.png'), fullPage: true })

  await pane(page, 'Configuration')
  expect(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1)).toBeTruthy()
  const panes = await page.locator('.workbench-pane:visible').evaluateAll((elements) => elements.map((element) => { const rect = element.getBoundingClientRect(); return { left: rect.left, right: rect.right } }))
  for (let index = 1; index < panes.length; index++) expect(panes[index].left).toBeGreaterThanOrEqual(panes[index - 1].right - 1)
  await page.screenshot({ path: testInfo.outputPath('configuration.png'), fullPage: true })
  await page.evaluate(() => { document.documentElement.style.zoom = '2' })
  expect(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1)).toBeTruthy()
  await page.screenshot({ path: testInfo.outputPath('zoom-200.png'), fullPage: true })
  expect(errors).toEqual([])
})