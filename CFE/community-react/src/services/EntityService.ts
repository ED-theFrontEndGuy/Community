import { IResultObject } from "@/types/DTOs/IResultObject";
import { BaseService } from "./BaseService";
import { AxiosError } from "axios";

export abstract class EntityService<TEntity> extends BaseService {
	constructor(private basePath: string) {
		super();
	}

	async getAllAsync(): Promise<IResultObject<TEntity[]>> {
		try {
			const response = await this.axiosInstance.get<TEntity[]>(this.basePath);

			console.log('getAll response', response);

			if (response.status <= 300) {
				return {
					statusCode: response.status,
					data: response.data
				}
			} else {
				console.log(response.status);

			}

			return {
				statusCode: response.status,
				errors: [(response.status.toString() + ' ' + response.statusText).trim()]
			}
		} catch (error) {
			console.log('error: ', (error as AxiosError).message);

			return {
				statusCode: (error as AxiosError).status ?? 0,
				errors: [(error as AxiosError).code ?? "???"]
			}
		}
	}
}
