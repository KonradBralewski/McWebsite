<script setup lang="ts">
    interface Props{
        title : string,
        description : string
    }

    const props = withDefaults(defineProps<Props>(), {
        title : 'No title was given, please contact us.',
        description : 'No description was given, please contact us.'
    })

</script>

<template>
    <div role="alert">
        <div class="bg-red-500 text-white font-bold rounded-t px-4 py-2">
            {{ title }}
        </div>
        <div class="border border-t-0 border-red-400 rounded-b bg-red-100 px-4 py-3 text-red-700">
            <p>{{ description }}</p>
        </div>
    </div>
</template>
