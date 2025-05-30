import { ICourse } from "@/types/domain/ICourse";
import { EntityService } from "./EntityService";
import { ICourseAdd } from "@/types/DTOs/ICourseAdd";


export class CourseService extends EntityService<ICourse, ICourseAdd> {
	constructor() {
		super('courses');
	}
}
