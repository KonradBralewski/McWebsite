<script setup lang="ts">

import MarkdownIt from "markdown-it";
import sanitizeHtml from 'sanitize-html';

const markdown = new MarkdownIt();

interface Props {
  src: string | null;
}

const props = withDefaults(defineProps<Props>(), {
  src: "#No src was given : ("
});

</script>

<template v-if="src">
    <div v-html="markdown.render(sanitizeHtml(src!))"></div>
</template>
