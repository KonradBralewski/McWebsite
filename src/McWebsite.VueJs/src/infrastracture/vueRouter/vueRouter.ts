import HomePage from '@/pages/HomePage.vue'
import NotFoundPage from '@/pages/NotFoundPage.vue'
import DocsPage from "@/pages/DocsPage.vue"
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
    {
        path : "/",
        component :  HomePage
    },
    {
        path : "/docs",
        component : DocsPage
    },
    {
        path: '/:pathMatch(.*)*',
        component: NotFoundPage
    }
]

const router = createRouter(
    {
        history : createWebHistory('/'),
        routes
    }
)

export default router