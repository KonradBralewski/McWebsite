<script setup lang="ts">

interface Props {
    darkMode: boolean,
    themeChangeHandler : (hasDarkMode : boolean) => void
}

const props = withDefaults(defineProps<Props>(), {
    darkMode: false,
    themeChangeHandler : () => {}
});

const isDropdownVisible = ref(false);

</script>

<template>
  <div 
    class="relative text-left text-2xl self-center mt-1.5 ml-auto mr-2 md:hidden"
  >
    <icon-iconoir-menu 
    :style="{color : darkMode ? 'white' : 'black'}"
    @click="isDropdownVisible = !isDropdownVisible"/>
    <div
      v-if="isDropdownVisible"
      class="absolute right-0 z-10 mt-2 w-40 origin-top-right rounded-md bg-white shadow-lg ring-1
       ring-black ring-opacity-5 focus:outline-none"
    >
      <div class="py-1">
        <ThemeSwitcher class="gap-3 ml-1" :dark-mode="darkMode" @theme-changed="themeChangeHandler"/>
        <RouterLink to="/docs" class="text-gray-700 block px-4 py-2 text-sm" @click="isDropdownVisible = false"
          >Docs</RouterLink
        >
        <RouterLink to="/about" class="text-gray-700 block px-4 py-2 text-sm" @click="isDropdownVisible = false"
          >About</RouterLink
        >        
      </div>
    </div>
  </div>
</template>
