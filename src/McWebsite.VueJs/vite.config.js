// vite.config.js

import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";
import Components from "unplugin-vue-components/vite";
import Pages from "vite-plugin-pages";
import Icons from "unplugin-icons/vite";
import IconsResolver from "unplugin-icons/resolver";
import AutoImport from "unplugin-auto-import/vite";

// https://vitejs.dev/config/
const path = require("path");

export default defineConfig({
  plugins: [
    vue({
      include: [/\.vue$/]
    }),
    Pages({
      extensions: ["vue"],
      dirs: [
        { dir: 'src/pages', baseRoute: '' },
        { dir: 'src/pages/**', baseRoute: '' }
      ]
    }),

    Components({
      dirs: ["src/**/components", "src/pages/**/components"],
      dts: "src/components.d.ts",
      deep: true,
      include: [/\.vue$/, /\.vue\?vue/],
      resolvers: [
        IconsResolver({
          prefix: "icon",
        }),
      ],
    }),
    Icons({
      autoInstall: true,
    }),
    AutoImport({
      imports: ["vue", "vue-router"],
      dts: "src/auto-imports.d.ts",
    }),
  ],
  resolve: {
    alias: {
      "@": path.resolve(__dirname, "src"),
      "#root": path.resolve(__dirname),
    },
  },
});
