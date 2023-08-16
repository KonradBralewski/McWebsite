import { createApp } from "vue";
import App from "./App.vue";
import vueRouter from "./common/infrastracture/vueRouter/vueRouterSetup";
import { provideServices } from "#root/src/common/services/servicesContainer";
import "./main.css";

const app = createApp(App);

app.use(vueRouter);

// IOC container
provideServices(app);

app.mount("#app");
