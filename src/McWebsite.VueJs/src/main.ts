import { createApp } from "vue";
import App from "./App.vue";
import vueRouter from './common/infrastracture/vueRouter/vueRouterSetup'
import serviceContainer from "#root/src/common/services/servicesContainer"
import "./main.css"

const app = createApp(App);

app.use(vueRouter);

// IOC container
app.provide(serviceContainer)

app.mount("#app");
