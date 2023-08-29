<script setup lang="ts">
import category from '../types/category';
import clickableCategory from '../types/clickableCategory';


const emits = defineEmits(["categorySelected"])

interface Props {
  categories: clickableCategory[] | null;
}

const props = withDefaults(defineProps<Props>(), {
    categories: null,
});

const clickableCategories = ref(props.categories)

</script>

<template v-if="clickableCategories !== null" >
    <div :class="$attrs.class">
        <div v-for="ctg in clickableCategories" :key="ctg.id">
        <p 
            :style="{fontSize : (Math.pow(ctg.level, -1  ) * 45) + 'px'}" 
            class="h-fit w-fit hover:underline underline-offset-4" 
            @click="() => {$emit('categorySelected', ctg.docsUri); ctg.wasClicked = !ctg.wasClicked}">
            {{ ctg.name }}
        </p>
     
        <div v-if="ctg.subCategories !== null && ctg.wasClicked" class="ml-10">
            <DocumentationCategories :categories="ctg.subCategories"/>
        </div>
    </div>
    </div>
   
</template>
