<script setup lang="ts">
</script>

<template>
  <nav 
  class="flex flex-row gap-5 text-4xl justify-center align-middle relative border-b-black pb-0.5
     border-b">
    <RouterLink 
    to="/" 
    class="ml-2 md:absolute md:left-4 font-extrabold text-transparent bg-clip-text 
     bg-gradient-to-t from-black to-emerald-800 animate-pulse-6
    ">McWebsite</RouterLink>
    <RouterLink to="/docs" class="hidden md:block">Docs</RouterLink>
    <RouterLink to="/about" class="hidden md:block">About</RouterLink>
    <icon-devicon-github class="hidden"/>
    <ThemeSwitcher/>
    <icon-iconoir-menu class="text-2xl self-center mt-1.5 ml-auto mr-2 md:hidden"/>
  </nav>
  <RouterView></RouterView>
</template>