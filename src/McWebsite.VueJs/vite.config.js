// vite.config.js

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import Components from 'unplugin-vue-components/vite'
import Pages from 'vite-plugin-pages'

// https://vitejs.dev/config/
const path = require("path");

export default defineConfig({
  plugins: [
    vue(),
    Pages({
      extensions : ['vue'],
      pagesDir: [
        { dir: 'src/**/pages', baseRoute: '' },
      ],
    }), 
    Components({
      dirs : 'src/**/components',
      dts: 'src/components.d.ts',
      deep : true,
      include: [/\.vue$/, /\.vue\?vue/]
    })
  ],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "./src"),
    },
  },
});
