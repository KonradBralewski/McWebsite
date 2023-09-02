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
            docsUri : "DomainModels/Aggregates/Aggregates.User.md",
            subCategories : null
          },
          {
            id : 6,
            name : "Conversation",
            level : 3,
            docsUri : "DomainModels/Aggregates/Aggregates.Conversation.md",
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
            docsUri : "DomainModels/Aggregates/Aggregates.GameServer.md",
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
            docsUri : "DomainModels/Aggregates/Aggregates.GameServerSubscription.md",
            subCategories : null
          },
          {
            id : 10,
            name : "In Game Event Order",
            level : 3,
            docsUri : "DomainModels/Aggregates/Aggregates.InGameEventOrder.md",
            subCategories : null
          }
        ]
      },
      {
        id : 4,
        name : "Entities",
        level : 2,
        docsUri : null,
        subCategories : [
          {
            id : 11,
            name : "Message",
            level : 2,
            docsUri : "DomainModels/Entities/Entities.Message.md",
            subCategories : null
          },
          {
            id : 12,
            name : "In Game Event",
            level : 2,
            docsUri : "DomainModels/Entities/Entities.InGameEvent.md",
            subCategories : null
          },
        ]
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
        docsUri : "API.md", 
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

  window.open(`https://github.com/KonradBralewski/McWebsite/tree/main/docs/${selectedCategory.value}`, "_blank")
})

const onCategorySelect = (category : string) => {
  if(category !== null){
    selectedCategory.value = category
  }
}

</script>

<template>
  <DocumentationCategories class="p-0 md:p-10" :categories="clickableCategories" @category-selected="onCategorySelect"/>
</template>
