<script setup lang="ts">
    interface Props{
        darkMode : boolean
    }

    const props = withDefaults(defineProps<Props>(), {
        darkMode : false
    })

    defineEmits(['themeChanged'])
</script>

<template>
     <icon-noto-sun v-if="darkMode" class="self-center w-7 h-7"/>
     <label class="relative inline-flex items-center cursor-pointer place-self-center">
      <input type="checkbox" value="" class="sr-only peer" @click="$emit('themeChanged', !darkMode)">
      <div 
      class="w-11 h-6 bg-gray-200 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full
        peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white
         after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all dark:border-gray-600
          peer-checked:bg-black"></div>
    </label>
    <icon-teenyicons-moon-solid v-if="!darkMode" class="self-center w-6 h-6"/>
</template>
