import { ICourse } from "@/types/domain/ICourse";
import { EntityService } from "./EntityService";

export class CourseService extends EntityService<ICourse> {
	constructor() {
		super('courses');
	}

}
