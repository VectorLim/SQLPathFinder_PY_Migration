import assert from 'node:assert/strict'
import { prepareSourceFiles, type SourceFileLike } from './fileSelection.ts'

function file(name: string, content = name): SourceFileLike {
  return { name, text: async () => content }
}

const cancelled = await prepareSourceFiles([])
assert.deepEqual(cancelled, { accepted: [], rejected: [] }, 'cancelling the picker must be a no-op')

const single = await prepareSourceFiles([file('one.txt', 'alpha')])
assert.deepEqual(single.accepted, [{ name: 'one.txt', content: 'alpha' }])
assert.deepEqual(single.rejected, [])

const multiple = await prepareSourceFiles([file('one.txt'), file('two.vg2'), file('three.TXT')])
assert.equal(multiple.accepted.length, 3, 'multiple supported files must all be prepared')

const unsupported = await prepareSourceFiles([file('bad.csv')])
assert.equal(unsupported.accepted.length, 0)
assert.equal(unsupported.rejected[0]?.name, 'bad.csv')
assert.match(unsupported.rejected[0]?.message ?? '', /Unsupported VG2 source file type/)

const duplicates = await prepareSourceFiles([file('same.txt'), file('SAME.TXT')])
assert.equal(duplicates.accepted.length, 1)
assert.equal(duplicates.rejected.length, 1)
assert.match(duplicates.rejected[0]?.message ?? '', /Duplicate/)

const unreadable: SourceFileLike = { name: 'broken.txt', text: async () => { throw new Error('read failed') } }
const failedRead = await prepareSourceFiles([unreadable, file('after.txt')])
assert.equal(failedRead.rejected[0]?.message, 'read failed')
assert.equal(failedRead.accepted[0]?.name, 'after.txt', 'one unreadable file must not block another selected file')

console.log('fileSelection tests passed')
