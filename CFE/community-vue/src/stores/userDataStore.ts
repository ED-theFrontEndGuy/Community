import { ref } from 'vue';
import { defineStore } from 'pinia';

export const useUserDataStore = defineStore('userData', () => {
	const jwt = ref('');
	const refreshToken = ref('');

	return { jwt, refreshToken };
});
