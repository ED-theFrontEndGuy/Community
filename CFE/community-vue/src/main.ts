import './assets/site.css'
import './assets/logo.svg'
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import router from '@/router';
import App from './App.vue';

const app = createApp(App);

app.use(createPinia());
app.use(router);

app.mount('#app');
