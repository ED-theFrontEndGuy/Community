import { createRouter, createWebHistory } from 'vue-router';
import Home from '@/views/AppHome.vue';
import Page1 from '@/views/AppPage1.vue';
import Login from '@/views/AppLogin.vue';
import Register from '@/views/AppRegister.vue';

const routes = [
	{
		path: '/',
		name: 'Home',
		component: Home,
	},
	{
		path: '/page1',
		name: 'Page1',
		component: Page1,
	},
	{
		path: '/login',
		name: 'Login',
		component: Login,
	},
	{
		path: '/register',
		name: 'Register',
		component: Register,
	},
]

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes,
});

export default router;
