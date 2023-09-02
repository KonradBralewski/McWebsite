<script setup lang="ts">
import { IUserPreferencesProvider } from "#root/src/common/services/UserPreferences/IUserPreferencesProvider";
import { UserPreferencesProvider } from "@/common/services/UserPreferences/UserPreferencesProvider";
import { UserPreferences } from "./common/types/preferences/UserPreferences";

const userPreferencesProvider: IUserPreferencesProvider = inject(
  UserPreferencesProvider.injectKey,
  new UserPreferencesProvider(),
);

const preferences = ref(userPreferencesProvider.getPreferences());

const onThemeChanged = (hasDarkMode: boolean) => {
  preferences.value.setDarkMode(hasDarkMode);
  userPreferencesProvider.setPreferences(
    unref(preferences.value) as UserPreferences,
  );
};
</script>

<template>
  <nav
    :class="{
      dark: preferences.getDarkMode(),
      'bg-black': preferences.getDarkMode(),
    }"
    class="flex flex-row gap-5 text-4xl justify-center align-middle relative border-b-black pb-0.5 border-b"
  >
    <RouterLink
      to="/"
      class="ml-3 font-extrabold text-transparent bg-clip-text
       bg-gradient-to-t from-black to-emerald-800 animate-pulse-6 dark:from-white dark:to-emerald-800"
      >McWebsite</RouterLink
    >
    <RouterLink
      to="/docs"
      class="hidden md:block dark:text-white ml-auto"
      active-class="underline underline-offset-6"
      >Docs</RouterLink
    >
    <RouterLink
      to="/about"
      class="hidden md:block dark:text-white"
      active-class="underline underline-offset-6"
      >About</RouterLink
    >
    <icon-devicon-github class="hidden" />
    <ThemeSwitcher
      class="hidden md:flex mt-1.5 gap-3 ml-auto mr-10"
      :dark-mode="preferences.getDarkMode()"
      @theme-changed="onThemeChanged"
    />
    <DropdownMenu :dark-mode="preferences.getDarkMode()" :theme-change-handler="onThemeChanged"/>
  </nav>
  <main :class="{ dark: preferences.getDarkMode() }">
    <div class="dark:bg-gray-800 dark:text-white min-h-screen w-full">
      <RouterView></RouterView>
    </div>
  </main>
</template>
