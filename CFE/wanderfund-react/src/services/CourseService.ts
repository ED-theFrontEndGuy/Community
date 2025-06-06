import { ICourse } from "@/types/domain/ICourse";
import { EntityService } from "./EntityService";
import { ICourseAdd } from "@/types/domain/ICourseAdd";


export class CourseService extends EntityService<ICourse, ICourseAdd> {
	constructor() {
		super('courses');
	}
}
