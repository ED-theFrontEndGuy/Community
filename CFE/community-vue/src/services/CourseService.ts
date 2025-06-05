import type { ICourseType } from "@/domain/ICourseType";
import type { IResultObject } from "@/types/IResultObject";
import { BaseEntityService } from "./BaseEntityService";

export class CourseService extends BaseEntityService<ICourseType> {
	constructor() {
		super('courses');
	}
}