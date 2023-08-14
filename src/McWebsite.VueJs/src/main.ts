import { createApp } from "vue";
import App from "./App.vue";
import vueRouter from './infrastracture/vueRouter/vueRouter'
import "./main.css"

const app = createApp(App);

app.use(vueRouter);
app.mount("#app");
