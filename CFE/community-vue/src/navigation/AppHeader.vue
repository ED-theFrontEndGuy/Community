<!-- eslint-disable vue/multi-word-component-names -->
<script setup lang="ts">
import {RouterLink} from 'vue-router';
import {useUserDataStore} from '@/stores/userDataStore';

const userStore = useUserDataStore();
</script>

<template>
	<nav
		v-if="userStore.jwt"
		class="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3"
	>
		<div class="container-fluid">
			<RouterLink class="navbar-brand" to="/">Community</RouterLink>
			<button
				class="navbar-toggler"
				type="button"
				data-bs-toggle="collapse"
				data-bs-target=".navbar-collapse"
				aria-controls="navbarSupportedContent"
				aria-expanded="false"
				aria-label="Toggle navigation"
			>
				<span class="navbar-toggler-icon"></span>
			</button>
			<div
				class="navbar-collapse collapse d-sm-inline-flex justify-conten t-between"
			>
				<ul class="navbar-nav flex-grow-1">
					<li class="nav-item">
						<RouterLink
							class="nav-link text-dark"
							to="/declarations"
						>
							Declarations
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink class="nav-link text-dark" to="/courses">
							Courses
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink class="nav-link text-dark" to="/timelogs">
							Timelogs
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink
							class="nav-link text-dark"
							to="/attachments"
						>
							Attachments
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink
							class="nav-link text-dark"
							to="/assignments"
						>
							Assignments
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink class="nav-link text-dark" to="/rooms">
							Rooms
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink
							class="nav-link text-dark"
							to="/studysessions"
						>
							StudySessions
						</RouterLink>
					</li>
					<li class="nav-item">
						<RouterLink
							class="nav-link text-dark"
							to="/studygroups"
						>
							StudyGroups
						</RouterLink>
					</li>
				</ul>

				<ul class="navbar-nav">
					<li>
						<a href="/" class="navbar-brand">Logout</a>
					</li>
				</ul>
			</div>
		</div>
	</nav>
</template>
