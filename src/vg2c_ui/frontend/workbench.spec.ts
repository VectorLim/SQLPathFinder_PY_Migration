import { expect, test, type Page } from '@playwright/test'
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
  if (name === 'Configuration' || name === 'Context') {
    const pullTab = page.getByRole('button', { name: `Expand ${name}`, exact: true })
    if (await pullTab.isVisible().catch(() => false)) await pullTab.click()
  }
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
  const output = name.replace(/\.txt$/i, '.py')
  const response = await page.request.post('/api/documents/open', { data: {
    source_path: `inputs/${name}`, output_path: `generated/inputs/${output}`,
  } })
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
  const intakeRegion = page.getByRole('region', { name: 'Workspace source intake' })
  await expect(intakeRegion.getByRole('button', { name: /Sources & translation/i })).toBeVisible()
  await expect(intakeRegion.locator('.source-intake')).toBeHidden()
  await pane(page, 'Configuration')
  await expect(page.getByRole('combobox', { name: /Output file/i })).toBeVisible()
  const tabs = await page.locator('.tabs').boundingBox()
  expect(tabs?.height).toBe(44)
  const intake = await page.getByRole('region', { name: 'Workspace source intake' }).boundingBox()
  expect(intake!.y + intake!.height).toBeLessThanOrEqual(tabs!.y + 1)
}

test('editing, persistence, preview and responsive layout', async ({ page }, testInfo) => {
  const errors: string[] = []
  page.on('pageerror', (error) => errors.push(error.message))
  await translate(page)
  await expect(page.getByRole('tab', { name: /workbench\.txt/ })).toBeVisible()
  await expect(page.getByRole('tab', { name: /workbench\.py/ })).toHaveCount(0)
  if (testInfo.project.name === 'mobile') {
    const actionBoxes = await page.locator('.change-toolbar .toolbar-group button').evaluateAll((buttons) => buttons.map((button) => {
      const rect = button.getBoundingClientRect()
      return { top: Math.round(rect.top), bottom: Math.round(rect.bottom) }
    }))
    expect(new Set(actionBoxes.map((box) => box.top)).size).toBe(1)
  }
  if (testInfo.project.name !== 'mobile') {
    const separator = page.getByRole('separator').first()
    await expect(separator).toBeVisible()
    const before = Number(await separator.getAttribute('aria-valuenow'))
    await separator.focus()
    await separator.press('ArrowRight')
    expect(Number(await separator.getAttribute('aria-valuenow'))).toBeGreaterThan(before)
  }
  const outputMode = page.getByRole('combobox', { name: /Output file/i })
  await outputMode.selectOption('__manual__')
  let outputPath = page.getByLabel('Output file path', { exact: true })
  await outputPath.fill('renamed.csv')
  await page.getByRole('button', { name: 'Undo', exact: true }).click()
  await expect(page.getByLabel('Output file path', { exact: true })).toHaveValue('out.csv')
  await page.getByRole('button', { name: 'Redo', exact: true }).click()
  outputPath = page.getByLabel('Output file path', { exact: true })
  await expect(outputPath).toHaveValue('renamed.csv')

  await expect(page.locator('.sql-column-label').first()).toBeVisible()
  await expect(page.getByRole('textbox', { name: 'Column expression', exact: true })).toHaveCount(0)
  await expect(outputPath).toHaveValue('renamed.csv')

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
  const save = page.getByRole('button', { name: 'Save', exact: true })
  await expect(save).toBeEnabled()
  let releaseSave!: () => void
  const saveGate = new Promise<void>((resolve) => { releaseSave = resolve })
  await page.route('**/api/changes/save', async (route) => { await saveGate; await route.continue() })
  await save.click()
  await expect(outputMode).toBeDisabled()
  await expect(page.getByRole('button', { name: 'Undo', exact: true })).toBeDisabled()
  releaseSave()
  await expect(page.getByText('No pending changes', { exact: true })).toBeVisible()
  await expect(page.getByText('Generate required', { exact: true })).toBeVisible()
  await expect(outputMode).toBeEnabled()

  await page.getByRole('button', { name: 'Generate', exact: true }).click()
  await expect(page.getByText('Generated', { exact: true })).toBeVisible()

  const reopened = await page.request.post('/api/documents/open', { data: { source_path: 'inputs/workbench.txt', output_path: 'generated/inputs/workbench.py' } })
  expect(reopened.ok()).toBeTruthy()
  const reopenedDocument: DocumentView = await reopened.json()
  expect(reopenedDocument.semantic_operations.flatMap((operation) => operation.bindings).find((binding) => binding.name === 'output')?.value).toBe('renamed.csv')

  await pane(page, 'Context')
  await expect(page.getByRole('tab', { name: 'File Flow', exact: true })).toBeVisible()
  await expect(page.getByRole('tab', { name: /Email/ })).toBeVisible()
  await expect(page.getByRole('tab', { name: 'Globals', exact: true })).toBeVisible()
  await expect(page.locator('.file-effect-row').first()).toBeVisible()
  await page.screenshot({ path: testInfo.outputPath('file-flow.png'), fullPage: true })

  await pane(page, 'Configuration')
  await expect(page.getByRole('tablist', { name: 'Query configuration' })).toBeVisible()
  expect(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1)).toBeTruthy()
  const panes = await page.locator('.workbench-pane:visible').evaluateAll((elements) => elements.map((element) => { const rect = element.getBoundingClientRect(); return { left: rect.left, right: rect.right } }))
  for (let index = 1; index < panes.length; index++) expect(panes[index].left).toBeGreaterThanOrEqual(panes[index - 1].right - 1)
  await page.screenshot({ path: testInfo.outputPath('configuration.png'), fullPage: true })
  await page.evaluate(() => { document.documentElement.style.zoom = '2' })
  expect(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1)).toBeTruthy()
  if (testInfo.project.name === 'mobile') {
    expect(await page.evaluate(() => {
      const brand = document.querySelector('.brand')?.getBoundingClientRect()
      const theme = document.querySelector('.theme-selector')?.getBoundingClientRect()
      if (!brand || !theme) return false
      return brand.right <= theme.left || theme.right <= brand.left || brand.bottom <= theme.top || theme.bottom <= brand.top
    })).toBeTruthy()
    const tabFits = await page.locator('.adaptive-pane-tabs button').evaluateAll((buttons) => buttons.every((button) => button.scrollWidth <= button.clientWidth + 1))
    expect(tabFits).toBeTruthy()
    const actionFits = await page.locator('.change-toolbar .toolbar-group button').evaluateAll((buttons) => buttons.every((button) => button.scrollWidth <= button.clientWidth + 1))
    expect(actionFits).toBeTruthy()
  }
  await page.screenshot({ path: testInfo.outputPath('zoom-200.png'), fullPage: true })
  expect(errors).toEqual([])
})

test('secondary panes reflow, stack pull tabs and restore in either collapse order', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await translate(page)

  const workbench = page.locator('.adaptive-workbench-panes')
  const logic = page.locator('#pane-logic')
  const configuration = page.locator('#pane-config')
  const context = page.locator('#pane-context')

  const initialConfig = await configuration.boundingBox()
  const initialContext = await context.boundingBox()
  expect(initialConfig).toBeTruthy()
  expect(initialContext).toBeTruthy()

  await page.getByRole('button', { name: 'Collapse Configuration', exact: true }).click()
  await expect(configuration).toBeHidden()
  await expect(context).toBeVisible()
  await expect(page.getByRole('button', { name: 'Expand Configuration', exact: true })).toBeVisible()
  await expectPullTabsStacked(page)
  await expect.poll(async () => (await context.boundingBox())?.width ?? 0, { timeout: 1500 })
    .toBeGreaterThan(initialContext!.width + 200)
  await page.screenshot({ path: testInfo.outputPath('collapse-config-only.png'), fullPage: true })

  await page.getByRole('button', { name: 'Collapse Context', exact: true }).click()
  await expect(context).toBeHidden()
  await expect(configuration).toBeHidden()
  await expectPullTabsStacked(page)
  const workbenchBox = await workbench.boundingBox()
  await expect.poll(async () => (await logic.boundingBox())?.width ?? 0, { timeout: 1500 })
    .toBeGreaterThanOrEqual(workbenchBox!.width - 2)
  await page.screenshot({ path: testInfo.outputPath('collapse-config-then-context.png'), fullPage: true })

  await page.getByRole('button', { name: 'Expand Configuration', exact: true }).click()
  await page.getByRole('button', { name: 'Expand Context', exact: true }).click()
  await expect(configuration).toBeVisible()
  await expect(context).toBeVisible()

  await page.getByRole('button', { name: 'Collapse Context', exact: true }).click()
  await expect(context).toBeHidden()
  await expect(configuration).toBeVisible()
  await expect.poll(async () => (await configuration.boundingBox())?.width ?? 0, { timeout: 1500 })
    .toBeGreaterThan(initialConfig!.width + 200)
  await page.screenshot({ path: testInfo.outputPath('collapse-context-only.png'), fullPage: true })

  await page.getByRole('button', { name: 'Collapse Configuration', exact: true }).click()
  await expect(configuration).toBeHidden()
  await expect(context).toBeHidden()
  await expectPullTabsStacked(page)
  await page.screenshot({ path: testInfo.outputPath('collapse-context-then-config.png'), fullPage: true })

  await page.getByRole('button', { name: 'Expand Context', exact: true }).click()
  await expect(context).toBeVisible()
  await expect(configuration).toBeHidden()
  await page.getByRole('button', { name: 'Expand Configuration', exact: true }).click()
  await expect(configuration).toBeVisible()
  await expect(context).toBeVisible()
  expect(await page.evaluate(() => document.documentElement.scrollWidth <= innerWidth + 1)).toBeTruthy()
})

async function expectPullTabsStacked(page: Page) {
  const config = page.getByRole('button', { name: /Configuration$/, exact: false }).filter({ has: page.locator('.paper-pull-tab__grip') })
  const context = page.getByRole('button', { name: /Context$/, exact: false }).filter({ has: page.locator('.paper-pull-tab__grip') })

  await expect.poll(async () => {
    const [configTab, contextTab] = await Promise.all([config.boundingBox(), context.boundingBox()])
    if (!configTab || !contextTab) return Number.POSITIVE_INFINITY
    return Math.abs(configTab.x - contextTab.x)
  }, { timeout: 1500 }).toBeLessThanOrEqual(14)

  const [configTab, contextTab] = await Promise.all([config.boundingBox(), context.boundingBox()])
  expect(configTab).toBeTruthy()
  expect(contextTab).toBeTruthy()
  expect(configTab!.y + configTab!.height).toBeLessThanOrEqual(contextTab!.y + 1)
}

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
  const attachments = page.getByRole('checkbox', { name: 'Attachments', exact: true })
  await attachments.check()
  await page.locator('.semantic-file-list input[type=file]').setInputFiles({
    name: 'chart.png',
    mimeType: 'image/png',
    buffer: Buffer.from([0x89, 0x50, 0x4e, 0x47]),
  })
  await expect(page.getByRole('combobox', { name: /Attachments 1/i })).toHaveValue('inputs/chart.png')

  await page.getByRole('button', { name: 'Preview', exact: true }).click()
  await expect(page.getByText('Changes validated', { exact: true })).toBeVisible()
  await page.keyboard.press('Control+s')
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

test('independent writes reorder in saved execution order', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  const name = 'reorder.txt'
  await uploadAndTranslate(page, name,
    '<OPTIONS>\n/WRITE-FILE=Y\n/CSV=first.txt\n</OPTIONS>\nfirst\n'
    + '<---- New Query ---->\n'
    + '<OPTIONS>\n/WRITE-FILE=Y\n/CSV=second.txt\n</OPTIONS>\nsecond\n'
    + '<---- New Query ---->\n')
  const original = await currentDocument(page, name)
  expect(original.semantic_operations).toHaveLength(2)
  const first = original.semantic_operations[0]
  const second = original.semantic_operations[1]
  await page.locator(`[data-semantic-tree-item="${first.id}"]`).locator('..').getByRole('button', { name: /Move .* down/ }).click()
  await expect.poll(async () => (await currentDocument(page, name)).semantic_operations[0].id).toBe(second.id)
  await page.locator(`[data-semantic-tree-item="${first.id}"]`).press('Alt+ArrowUp')
  await expect.poll(async () => (await currentDocument(page, name)).semantic_operations[0].id).toBe(first.id)
  await page.locator(`[data-semantic-tree-item="${first.id}"]`).locator('..').getByRole('button', { name: /Drag .* to reorder/ })
    .dragTo(page.locator(`[data-semantic-tree-item="${second.id}"]`))
  await expect.poll(async () => (await currentDocument(page, name)).semantic_operations[0].id).toBe(second.id)
  await expect(page.getByText('Generate required', { exact: true })).toBeVisible()
  await page.getByRole('button', { name: 'Generate', exact: true }).click()
  await expect(page.getByText('Generated', { exact: true })).toBeVisible()
})

test('SQLite choices come from uploaded table headers and drive typed SQL edits', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await page.goto('/')
  await page.waitForLoadState('networkidle')
  for (const [name, csv] of [
    ['people.csv', 'id,name\n1,Alice\n'],
    ['teams.csv', 'id,team\n1,Blue\n'],
  ]) {
    const response = await page.request.post('/api/workspace/files', {
      multipart: {
        files: { name, mimeType: 'text/csv', buffer: Buffer.from(csv) },
        paths: name,
      },
    })
    expect(response.ok()).toBeTruthy()
  }
  const inventory = await page.request.get('/api/workspace/files')
  expect((await inventory.json()).map((file: { path: string }) => file.path)).toEqual(expect.arrayContaining(['inputs/people.csv', 'inputs/teams.csv']))
  await uploadAndTranslate(
    page,
    'typed-sql.txt',
    '<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n/TABLE=inputs/people.csv:people\n'
    + '/TABLE=inputs/teams.csv:teams\n/HEADERS=output_only\n</OPTIONS>\n'
    + 'SELECT p.id FROM people p\n<---- New Query ---->\n',
  )
  await selectSemanticOperation(page, 'typed-sql.txt', (operation) => operation.bindings.some((binding) => binding.capabilities.includes('structured-sql')))
  await pane(page, 'Configuration')
  await expect(page.getByRole('combobox', { name: 'Input files 1' })).toHaveValue('inputs/people.csv')
  await expect(page.getByText('Table: people', { exact: true })).toBeVisible()

  await page.getByRole('button', { name: 'Add column' }).click()
  await page.getByRole('combobox', { name: 'Column', exact: true }).selectOption({ label: 'p.name' })
  await page.getByRole('button', { name: 'Add column', exact: true }).last().click()
  await expect(page.locator('.sql-column-label', { hasText: 'name' })).toBeVisible()
  await expect(page.getByText('output_only', { exact: true })).toHaveCount(0)

  await page.getByRole('tab', { name: /Joins/ }).click()
  await page.getByRole('button', { name: 'Add join' }).click()
  await page.getByRole('combobox', { name: 'Table', exact: true }).selectOption({ label: 'teams' })
  await page.getByRole('combobox', { name: 'Left key', exact: true }).selectOption({ label: 'p.id' })
  await page.getByRole('combobox', { name: 'Right key', exact: true }).selectOption({ label: 'teams.id' })
  await page.getByRole('button', { name: 'Add join', exact: true }).last().click()
  await expect(page.locator('.sql-join-row')).toContainText('teams')
})

test('SQL columns reorder by identity with pointer, keyboard, and touch controls', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  await uploadAndTranslate(page, 'column-order.txt',
    '<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n'
    + 'SELECT 1 AS one, 2 AS two FROM t\n<---- New Query ---->\n')
  await selectSemanticOperation(page, 'column-order.txt',
    (operation) => operation.bindings.some((binding) => binding.capabilities.includes('structured-sql')))
  await pane(page, 'Configuration')
  await page.getByRole('button', { name: 'Drag item 1' })
    .dragTo(page.locator('.reorderable-item').nth(1))
  await expect(page.locator('.reorderable-item').first()).toContainText('two')
  await expect(page.locator('.reorderable-item').nth(1)).toHaveAttribute('tabindex', '0')
  await page.locator('.reorderable-item').nth(1).press('Alt+ArrowUp')
  await expect(page.locator('.reorderable-item').first()).toContainText('one')
  await expect(page.getByRole('button', { name: 'Move item 1 down' })).toBeEnabled()
  await page.getByRole('button', { name: 'Move item 1 down' }).click()
  await expect(page.locator('.reorderable-item').first()).toContainText('two')
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

test('file-backed SQL filter chooses an uploaded server file', async ({ page }, testInfo) => {
  test.skip(testInfo.project.name !== 'desktop')
  const name = 'file-filter.txt'
  await uploadAndTranslate(page, name,
    '<OPTIONS>\n/OLEDB=SQLite\n/CSV=out.csv\n</OPTIONS>\n'
    + "SELECT * FROM lots t WHERE t.lot IN SQL_Get_CSV_List('old.csv', lot, 't.lot In')\n"
    + '<---- New Query ---->\n')
  const upload = await page.request.post('/api/workspace/files', {
    multipart: {
      files: { name: 'new.csv', mimeType: 'text/csv', buffer: Buffer.from('lot\nA\n') },
      paths: 'new.csv',
    },
  })
  expect(upload.ok()).toBeTruthy()
  const operation = await selectSemanticOperation(page, name,
    (item) => item.bindings.some((binding) => binding.capabilities.includes('structured-sql')))
  await pane(page, 'Configuration')
  await page.getByRole('tab', { name: /Filters/ }).click()
  const selector = page.getByRole('combobox', { name: /File list for t.lot In/ })
  await expect(selector).toBeEnabled()
  await selector.selectOption('inputs/new.csv')
  await expect(selector).toHaveValue('inputs/new.csv')
  await page.getByRole('button', { name: 'Save', exact: true }).click()
  await expect(page.getByText('Generate required', { exact: true })).toBeVisible()
  const document = await currentDocument(page, name)
  expect(document.effects.find((effect) => effect.id.includes('sql-get-csv-list'))?.inputs[0].path).toBe('inputs/new.csv')
  const sql = operation.bindings.find((binding) => binding.capabilities.includes('structured-sql'))!
  const modelResponse = await page.request.post('/api/sql/inspect', {
    data: { ...document, binding_id: sql.id },
  })
  expect(modelResponse.ok()).toBeTruthy()
  expect((await modelResponse.json()).file_lists[0].path).toBe('inputs/new.csv')
})
