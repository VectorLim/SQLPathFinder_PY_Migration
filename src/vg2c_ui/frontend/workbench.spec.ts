import { expect, test, type Page } from '@playwright/test'
import { writeFile } from 'node:fs/promises'
import { dirname, join } from 'node:path'
import type { DocumentView } from './src/contracts.generated'

async function pane(page: Page, name: 'Script Logic' | 'Configuration' | 'Context') {
  const target = page.locator(name === 'Script Logic' ? '#pane-logic' : name === 'Configuration' ? '#pane-config' : '#pane-context')
  if (await target.isVisible().catch(() => false)) return
  const tabs = page.getByRole('navigation', { name: 'Workbench views' })
  const tabButton = tabs.getByRole('button', { name, exact: true })
  if (await tabButton.isVisible().catch(() => false)) {
    await tabButton.click()
    return
  }
  const visibility = page.locator('.workbench-pane-controls').getByRole('button', { name, exact: true })
  if (await visibility.isVisible().catch(() => false)) await visibility.click()
}

async function uploadAndTranslate(page: Page, name: string, source: string) {
  await page.goto('/')
  await expect(page.getByRole('button', { name: 'Upload files', exact: true })).toBeEnabled()
  await page.locator('input[type=file]').first().setInputFiles({
    name, mimeType: 'text/plain', buffer: Buffer.from(source),
  })
  await page.getByRole('button', { name: /Upload queued/ }).click()
  await page.getByRole('checkbox', { name: `inputs/${name}`, exact: true }).check()
  await page.getByRole('button', { name: 'Translate selected', exact: true }).click()
  await expect(page.getByRole('treeitem').first()).toBeVisible()
}


async function currentDocument(page: Page, name: string): Promise<DocumentView> {
  const response = await page.request.post('/api/documents/open', { data: { source_path: `inputs/${name}` } })
  expect(response.ok()).toBeTruthy()
  return response.json() as Promise<DocumentView>
}

async function selectSemanticOperation(
  page: Page,
  name: string,
  predicate: (operation: DocumentView['semantic_operations'][number]) => boolean,
): Promise<DocumentView['semantic_operations'][number]> {
  const document = await currentDocument(page, name)
  const operation = document.semantic_operations.find(predicate)
  expect(operation, 'Expected semantic operation was not present in the translated document.').toBeTruthy()
  await page.locator(`[data-semantic-tree-item="${operation!.id}"]`).click()
  return operation!
}

async function translate(page: Page) {
  await uploadAndTranslate(
    page,
    'workbench.txt',
    '<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\nSELECT 1 AS value\n<---- New Query ---->\n',
  )
  await selectSemanticOperation(page, 'workbench.txt', (operation) => operation.bindings.some((binding) => binding.capabilities.includes('structured-sql')))
  await pane(page, 'Configuration')
  await expect(page.getByLabel('Output file', { exact: true })).toBeVisible()
  const tabs = await page.locator('.tabs').boundingBox()
  expect(tabs?.height).toBe(44)
  const intake = await page.getByRole('region', { name: 'Workspace source intake' }).boundingBox()
  expect(intake!.y + intake!.height).toBeLessThanOrEqual(tabs!.y + 1)
}

test('editing, persistence, preview and responsive layout', async ({ page }, testInfo) => {
  const errors: string[] = []
  page.on('pageerror', (error) => errors.push(error.message))
  await translate(page)
  if (testInfo.project.name !== 'mobile') {
    const separator = page.getByRole('separator').first()
    await expect(separator).toBeVisible()
    const before = Number(await separator.getAttribute('aria-valuenow'))
    await separator.focus()
    await separator.press('ArrowRight')
    expect(Number(await separator.getAttribute('aria-valuenow'))).toBeGreaterThan(before)
  }
  const output = page.getByLabel('Output file', { exact: true })
  await output.fill('renamed.csv')
  await page.getByRole('button', { name: 'Undo', exact: true }).click()
  await expect(output).toHaveValue('out.csv')
  await page.getByRole('button', { name: 'Redo', exact: true }).click()
  await expect(output).toHaveValue('renamed.csv')

  const expression = page.getByRole('textbox', { name: 'Column expression', exact: true }).first()
  await expect(expression).toBeEnabled()
  await expression.fill('2')
  await expression.press('Tab')
  await expect(page.getByRole('button', { name: 'Reset SQL to generated value' })).toBeEnabled()
  await page.getByRole('button', { name: 'Reset SQL to generated value' }).click()
  await expect(expression).toHaveValue('1')
  await expect(output).toHaveValue('renamed.csv')

  await expect(page.getByText('Generated information', { exact: true })).toHaveCount(0)
  const advanced = page.getByText('Advanced', { exact: true }).last()
  if (await advanced.isVisible().catch(() => false)) {
    await advanced.click()
    const rawSql = page.getByText('View raw SQL', { exact: true })
    if (await rawSql.isVisible().catch(() => false)) await rawSql.click()
  }

  await page.getByRole('button', { name: 'Preview', exact: true }).click()
  await expect(page.getByText('Validated Python diff', { exact: true })).toHaveCount(0)
  await expect(page.getByText('Changes validated', { exact: true })).toBeVisible()
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
  expect(reopenedDocument.semantic_operations.flatMap((operation) => operation.bindings).find((binding) => binding.name === 'output')?.value).toBe('renamed.csv')

  await pane(page, 'Context')
  await expect(page.getByRole('tab', { name: 'File Flow', exact: true })).toBeVisible()
  await expect(page.getByRole('tab', { name: /Email/ })).toBeVisible()
  await expect(page.getByRole('tab', { name: 'Globals', exact: true })).toBeVisible()
  await page.getByRole('button', { name: 'Preview CSV', exact: true }).first().click()
  await expect(page.locator('.on-disk-preview [role=alert]')).toBeVisible()
  const session = (await page.context().cookies()).find((cookie) => cookie.name === 'vg2c_workspace')!
  await writeFile(join(process.env.VG2C_TEST_ROOT!, 'workspaces', session.value, dirname(reopenedDocument.output_path), 'renamed.csv'), 'value\nverified\n')
  await page.getByRole('button', { name: 'Preview CSV', exact: true }).first().click()
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

test('Email context limits bulk edits to enable state and accepts image attachments', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await uploadAndTranslate(
    page,
    'email.txt',
    '<OPTIONS>\n/UTILITIES="SQLPathFinder_Email.va" "person@example.com" "Report" "Body"\n</OPTIONS>\n<---- New Query ---->\n',
  )
  await selectSemanticOperation(page, 'email.txt', (operation) => operation.capabilities.includes('email'))

  await pane(page, 'Context')
  await page.getByRole('tab', { name: /Email/ }).click()
  await expect(page.getByRole('button', { name: 'Enable selected', exact: true })).toBeVisible()
  await expect(page.getByRole('button', { name: 'Disable selected', exact: true })).toBeVisible()
  await expect(page.getByRole('button', { name: 'Enable all', exact: true })).toBeVisible()
  await page.getByRole('button', { name: 'Disable all', exact: true }).click()
  await page.screenshot({ path: testInfo.outputPath('email-context.png'), fullPage: true })

  await pane(page, 'Configuration')
  const attachments = page.getByRole('checkbox', { name: 'Set Attachments', exact: true })
  await attachments.check()
  await page.locator('.semantic-file-list input[type=file]').setInputFiles({
    name: 'chart.png',
    mimeType: 'image/png',
    buffer: Buffer.from([0x89, 0x50, 0x4e, 0x47]),
  })
  await expect(page.getByLabel('Attachments 1', { exact: true })).toHaveValue('inputs/chart.png')

  await page.getByRole('button', { name: 'Preview', exact: true }).click()
  await page.getByRole('button', { name: 'Apply', exact: true }).click()
  await expect(page.getByText('No pending changes', { exact: true })).toBeVisible()

  const reopened = await page.request.post('/api/documents/open', {
    data: { source_path: 'inputs/email.txt', output_path: 'generated/inputs/email.py' },
  })
  expect(reopened.ok()).toBeTruthy()
  const document: DocumentView = await reopened.json()
  const email = document.semantic_operations.find((operation) => operation.capabilities.includes('email'))
  expect(email?.bindings.find((binding) => binding.name === 'enabled')?.value).toBe(false)
  expect(email?.bindings.find((binding) => binding.name === 'attachments')?.value).toEqual(['inputs/chart.png'])
})

test('Embedded Python edits stay modal and must validate before commit', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await uploadAndTranslate(
    page,
    'embedded.txt',
    '<OPTIONS>\n/WRITE-FILE=Y\n/CSV=embedded.py\n</OPTIONS>\nprint("before")\n<---- New Query ---->\n',
  )
  await selectSemanticOperation(page, 'embedded.txt', (operation) => operation.capabilities.includes('embedded-python'))
  await pane(page, 'Configuration')

  await page.getByRole('button', { name: 'Edit Python', exact: true }).click()
  const dialog = page.getByRole('dialog')
  await expect(dialog).toBeVisible()
  const source = dialog.getByRole('textbox', { name: 'Embedded Python source' })
  await source.fill('if :')
  await dialog.getByRole('button', { name: 'Update Python', exact: true }).click()
  await expect(dialog.getByRole('alert')).toBeVisible()
  await page.screenshot({ path: testInfo.outputPath('embedded-python-validation.png'), fullPage: true })

  await source.fill('print("after")')
  await dialog.getByRole('button', { name: 'Update Python', exact: true }).click()
  await expect(dialog).toBeHidden()
  await page.getByRole('button', { name: 'Preview', exact: true }).click()
  await expect(page.getByText('Changes validated', { exact: true })).toBeVisible()
})

test('HTML preview renders the current draft without exposing generated Python', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await uploadAndTranslate(
    page,
    'report.txt',
    '<OPTIONS>\n/REPORT=HTML-LAYOUT\n/INSTANCE=101\n</OPTIONS>\n:FILE:preview.html\n:TITLE:Preview\n<h1>Hello Preview</h1>\n<---- New Query ---->\n',
  )
  await selectSemanticOperation(page, 'report.txt', (operation) => operation.capabilities.includes('html-preview'))
  await pane(page, 'Configuration')

  const preview = page.locator('.html-preview')
  await expect(preview).toBeVisible()
  await preview.getByRole('button', { name: 'Preview', exact: true }).click()
  await expect(preview.getByText('Exact preview', { exact: true })).toBeVisible()
  await expect(preview.locator('iframe[title="HTML report preview"]')).toBeVisible()
  await expect(page.getByText('Generated information', { exact: true })).toHaveCount(0)
  await page.screenshot({ path: testInfo.outputPath('html-preview.png'), fullPage: true })
})
