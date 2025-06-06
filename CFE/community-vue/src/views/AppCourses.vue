<script setup lang="ts">
import type {ICourseType} from '@/domain/ICourseType';
import {CourseService} from '@/services/CourseService';
import type {IResultObject} from '@/types/IResultObject';
import {onMounted, reactive, ref} from 'vue';

const requesting = ref(false);
const courseData = reactive<IResultObject<ICourseType[]>>({});
const courseService = new CourseService();

const fetchPageData = async () => {
	requesting.value = true;

	try {
		const result = await courseService.getAllAsync();

		console.log(result.data);

		courseData.data = result.data;
		courseData.errors = result.errors;
	} catch (error) {
		console.log('Error fetching data:', error);
	} finally {
		requesting.value = false;
	}
};

onMounted(async () => {
	await fetchPageData();
});
</script>

<template>
	<h1>Courses</h1>

	<p>
		<RouterLink to="/courses/create">Create New</RouterLink>
	</p>
	<table class="table">
		<thead>
			<tr>
				<th>Course Name</th>
				<th></th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="course in courseData.data" :key="course.id">
				<td>
					{{ course.name }}
				</td>

				<td>
					<a href="/courses/edit/">Edit</a>
					|
					<a href="/courses/edit/">Delete</a>
				</td>
			</tr>
		</tbody>
	</table>
</template>
