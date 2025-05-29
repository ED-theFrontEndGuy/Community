import axios from 'axios';

// run once to set up the axios instance
const axiosInstance = axios.create({
	baseURL: "http://localhost:5269/api/v1/",
	headers: {
		'Content-Type': 'application/json',
		'Accept': 'application/json',
	},
});

export default axiosInstance;
