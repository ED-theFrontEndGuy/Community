import { Timestamp } from "next/dist/server/lib/cache-handlers/types";

export interface ITimelogAdd {
	name: string;
	startTime: Timestamp;
	endTime?: Timestamp;
	duration?: number;
	courseId: string;
	courseName: string;
	assignmentId: string;
	assignmentName: string;
}
