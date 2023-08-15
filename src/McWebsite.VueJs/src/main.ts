import { createApp } from "vue";
import App from "./App.vue";
import vueRouter from './common/infrastracture/vueRouter/vueRouterSetup'
import "./main.css"

const app = createApp(App);

app.use(vueRouter);

app.mount("#app");
