import { AxiosError } from 'axios';
import { BaseService } from './BaseService';
import { ILoginDto } from '@/types/DTOs/ILoginDto';
import { IResultObject } from '@/types/DTOs/IResultObject';

export class AccountService extends BaseService {
	async loginAsync(email: string, password: string): Promise<IResultObject<ILoginDto>> {
		const url = '/account/login';

		try {
			const loginData = {
				email,
				password,
			}

			const response = await this.axiosInstance.post<ILoginDto>(url + "?jwtExpiresInSeconds=5", loginData);

			console.log('login response', response);

			if (response.status <= 300) {
				return {
					statusCode: response.status,
					data: response.data,
				}
			}

			return {
				statusCode: response.status,
				errors: [(response.status.toString() + ' ' + response.statusText).trim()],
			}
		} catch (error) {
			console.log('error', (error as Error).message);
			return {
				statusCode: (error as AxiosError)?.status,
				errors: [(error as AxiosError).code ?? ""],
			}
		}
	}
}
