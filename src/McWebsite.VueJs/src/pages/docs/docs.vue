<script setup lang="ts">

import category from "#root/src/pages/docs/types/category"
import clickableCategory from "./types/clickableCategory";

// Temporary hard-coded categories.
const categories : category[] = [
  {
    id : 0,
    name : "Domain Models",
    level : 1,
    docsUri : null,
    subCategories : [
      {
        id : 3,
        name : "Aggregates",
        level : 2,
        docsUri : null,
        subCategories : [
          {
            id : 5,
            name : "User",
            level : 3,
            docsUri : "Aggregates/User.md",
            subCategories : null
          },
          {
            id : 6,
            name : "Conversation",
            level : 3,
            docsUri : "Aggregates/Conversation.md",
            subCategories : [{
              id : 999,
              name : "test",
              level : 3,
              docsUri : null,
              subCategories : null
            }]
          },
          {
            id : 7,
            name : "Game Server",
            level : 3,
            docsUri : "Aggregates/GameServer.md",
            subCategories : null
          },
          {
            id : 8,
            name : "Game Server Report",
            level : 3,
            docsUri : null,
            subCategories : null
          },
          {
            id : 9,
            name : "Game Server Subscription",
            level : 3,
            docsUri : null,
            subCategories : null
          },
          {
            id : 10,
            name : "In Game Event Order",
            level : 3,
            docsUri : null,
            subCategories : null
          }
        ]
      },
      {
        id : 4,
        name : "Entitites",
        level : 2,
        docsUri : null,
        subCategories : null
      },
    ]
  },
  {
    id : 1,
    name : "API",
    level : 1,
    docsUri : null,
    subCategories : [
      {
        id : 2,
        name : "API Documentation",
        level : 2,
        docsUri : null, 
        subCategories : null
      }
    ]
  }
]

// 'enhance' props with additional 'clicked' property
const createClickableCategories = (categories : category[] | null) : clickableCategory[] | null => {
    if(categories === null) 
        return null;

    const clickableCategories : clickableCategory[] =  categories.map((item) => {
        const newItem : any  = { ...item, wasClicked: false };
        if (newItem.subCategories) {
        newItem.subCategories = createClickableCategories(newItem.subCategories);
        }
        return newItem;
    })

    return clickableCategories;
}

const clickableCategories : clickableCategory[] | null = createClickableCategories(categories)

const selectedCategory : Ref<string | null> = ref(null)
const categoryMarkdownSource : Ref<string | null> = ref(null)

watchEffect(async () => {
  if(selectedCategory.value === null)
    return

  const response = await fetch(`https://raw.githubusercontent.com/KonradBralewski/McWebsite/main/docs/${selectedCategory.value}`)

  categoryMarkdownSource.value = await response.json()
})

const onCategorySelect = (event : Event) => {
  console.log(event)  
}

</script>

<template>
  <DocumentationCategories class="p-0 md:p-10" :categories="clickableCategories" @category-selected="onCategorySelect"/>
  <MarkdownPlaceholder :src=" categoryMarkdownSource"/>
</template>
