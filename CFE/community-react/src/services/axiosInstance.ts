import axios from 'axios';

// run once to set up the axios instance
const axiosInstance = axios.create({
	baseURL: "http://localhost:5269/api/v1/",
	headers: {
		"Content-Type": "application/json",
		Accept: "application/json",
	},
});

// interceptor to cut between requests and responses
axiosInstance.interceptors.request.use(
	(config) => {
		const token = localStorage.getItem("_jwt");
		console.log(token);

		if (token) {

			config.headers.Authorization = `Bearer ${token}`;
		}

		return config;
	},
	(error) => {
		return Promise.reject(error);
	}
);

export default axiosInstance;
