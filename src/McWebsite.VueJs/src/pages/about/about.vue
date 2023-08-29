<script setup lang="ts">

const openGithubProfile = () => {
    window.open("https://github.com/KonradBralewski", "_blank")
}

</script>

 <template>
    <main class="">
        <div class="flex flex-col align-middle items-center absolute left-1/2 top-1/2 -translate-x-1/2 -translate-y-1/2">
            <icon-fa6-brands-github class="w-32 h-32 pt-5 hover:opacity-70" @click="openGithubProfile"/>
            <a href="https://github.com/KonradBralewski/McWebsite" target="_blank" class="text-center font-bold underline underline-offset-4">McWebsite repository</a>
            <p class="mt-14 text-center font-bold text-xl w-72 sm:w-80 m-auto">
            McWebsite is an amateur attempt at delving into the Clean Architecture with the paradigm of Domain Driven Design.
             Feel free to get in touch with me through my GitHub if anything seems suboptimal to you or if you would have a different solution
              to any of the challenges.
        </p>
        </div>
    </main>
  </template>
  