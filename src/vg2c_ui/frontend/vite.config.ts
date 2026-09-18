import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig(({ mode }) => ({
  plugins: [react()],
  server: {
    proxy: {
      '/api': loadEnv(mode, '.', 'VG2C_').VG2C_API_URL ?? 'http://127.0.0.1:8765',
    },
  },
  build: {
    outDir: '../static',
    emptyOutDir: true,
  },
}))
