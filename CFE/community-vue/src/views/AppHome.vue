<script setup lang="ts">
import {useRouter} from 'vue-router';
import {useUserDataStore} from '@/stores/userDataStore';

const router = useRouter();
const userStore = useUserDataStore();

if (!userStore.jwt) {
	router.push('login');
}
</script>

<template>Home</template>
