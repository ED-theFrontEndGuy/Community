import { Timestamp } from "next/dist/server/lib/cache-handlers/types";
import { IDomainId } from "../IDomainId";

export interface ITimelog extends IDomainId {
	name: string;
	startTime: Timestamp;
	endTime?: Timestamp;
	duration?: number;
	courseId: string;
	courseName: string;
	assignmentId: string;
	assignmentName: string;
}
