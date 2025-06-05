import type { IResultObject } from '@/types/IResultObject';
import { BaseService } from '@/services/BaseService';

export abstract class BaseEntityService<TEntity> extends BaseService {
	constructor(private basePath: string) {
		super();
	}

	async getAllAsync(): Promise<IResultObject<TEntity[]>> {
		try {
			const response = await BaseService.axios.get<TEntity[]>(this.basePath);

			console.log('getAll response', response);

			if (response.status <= 300) {
				return { data: response.data }
			}

			return {
				errors: [(response.status.toString() + ' ' + response.statusText).trim()]
			}
		} catch (error) {
			console.log('error: ', (error as Error).message);

			return {
				errors: [JSON.stringify(error)],
			}
		}
	}
}
