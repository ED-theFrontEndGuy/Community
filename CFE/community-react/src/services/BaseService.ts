import { ILoginDto } from '@/types/DTOs/ILoginDto';
import axios, { AxiosInstance } from 'axios';

export abstract class BaseService {
	protected axiosInstance: AxiosInstance;

	constructor() {
		this.axiosInstance = axios.create({
			baseURL: "http://localhost:5269/api/v1/",
			headers: {
				"Content-Type": "application/json",
				Accept: "application/json",
			},
		});

		this.axiosInstance.interceptors.request.use(
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

		this.axiosInstance.interceptors.response.use(
			(response) => {
				return response;
			},

			async (error) => {
				const originalRequest = error.config;

				if (error.response && error.response.status === 401 && !originalRequest._retry) {
					originalRequest._retry = true;

					try {
						const jwt = localStorage.getItem("_jwt");
						const refreshToken = localStorage.getItem("_refreshToken");
						const response = await axios.post<ILoginDto>(
							"http://localhost:5269/api/v1/account/renewRefreshToken?jwtExpiresInSeconds=5",
							{
								jwt: jwt,
								refreshToken: refreshToken,
							}
						);

						console.log("renewRefreshToken", response);


						if (response && response.status <= 300) {
							localStorage.setItem("_jwt", response.data.jwt);
							localStorage.setItem("_refreshToken", response.data.refreshToken);
							originalRequest.headers.Authorization = `Beaer ${response.data.jwt}`;

							return this.axiosInstance(originalRequest);
						}

						return Promise.reject(error);
					} catch (error) {
						console.log("Error refreshing token:", error);
						return Promise.reject(error);
					}
				}

				return Promise.reject(error);
			}
		);
	}
}
