<script setup lang="ts">

const openGithubRepository = () => {
    window.open("https://github.com/KonradBralewski/McWebsite", "_blank")
}

const openPostmanCollection = () => {
  window.open("https://app.getpostman.com/run-collection/25919237-0f152d61-1600-4b4d-b39e-e3b2d8efe3ab?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D25919237-0f152d61-1600-4b4d-b39e-e3b2d8efe3ab%26entityType%3Dcollection%26workspaceId%3D53d7e159-6288-43ed-a1cc-1a63af57b518", "_blank")
}

</script>

 <template>
    <main class="">
        <div 
          class="flex flex-row align-middle items-center absolute 
            top-1/2 -translate-y-1/2 left-1/2 -translate-x-1/2 gap-3">
          <div class="h-40 flex flex-col md:h-72 md:w-60">
            <icon-fa6-brands-github 
              class="w-32 h-32 pt-5 md:w-60 md:h-60 hover:opacity-70 hover:animate-pulse-0.8 self-center"
              @click="openGithubRepository"/>
            <p
              class="text-center font-bold underline underline-offset-4 mt-auto hover:no-underline hover:animate-pulse-0.8"
              @click="openGithubRepository">Repository</p>
          </div>
          <div class="h-40 w-32 md:h-72 md:w-60 flex flex-col">
            <img 
              src="https://run.pstmn.io/button.svg" alt="postman" 
              class="w-40 h-14 md:w-60 md:h-20 pt-5 hover:opacity-70 hover:animate-pulse-0.8 self-center mt-auto"
              @click="openPostmanCollection"/>
            <p
              class="text-center font-bold underline underline-offset-4 mt-auto hover:no-underline hover:animate-pulse-0.8"
              @click="openPostmanCollection">Run in Postman</p>
          </div>
        </div>
    </main>
  </template>
  