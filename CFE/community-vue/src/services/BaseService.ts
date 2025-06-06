import axios from 'axios';
import { useUserDataStore } from '@/stores/userDataStore';

export abstract class BaseService {
	protected static axios = axios.create({
		baseURL: 'http://localhost:5269/api/v1/',
		headers: {
			Accept: 'application/json',
			'Content-Type': 'application/json',
		},
	});

	static {
		this.axios.interceptors.request.use((config) => {
			const userDataStore = useUserDataStore();

			if (userDataStore.jwt) {
				config.headers['Authorization'] = `Bearer ${userDataStore.jwt}`;
			}

			if (userDataStore.refreshToken) {
				config.headers['RefreshToken'] = userDataStore.refreshToken;
			}

			return config;
		});
	}
}
