// vite.config.js

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import Components from 'unplugin-vue-components/vite'

// https://vitejs.dev/config/
const path = require("path");

export default defineConfig({
  plugins: [
    vue(),
    Components({
      dirs : ['src/components', 'src/pages'],
      dts: 'components.d.ts'
    })
  ],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
});
