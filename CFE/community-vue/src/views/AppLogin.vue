<script setup lang="ts">
import {ref} from 'vue';
import {useUserDataStore} from '@/stores/userDataStore';
import {IdentityService} from '@/services/IdentityService';
import {useRouter} from 'vue-router';

const store = useUserDataStore();
const router = useRouter();
const email = ref('');
const password = ref('');
const error = ref<string | null>(null);

const doLogin = async () => {
	const response = await IdentityService.login(email.value, password.value);

	if (response.data) {
		store.jwt = response.data.jwt;
		store.refreshToken = response.data.refreshToken;
		router.push({name: 'Home'});
	} else {
		error.value = response.errors?.[1] || 'Login failed';
	}
};
</script>

<template>
	<div class="content">
		<div v-if="error" class="alert alert-warning" role="alert">
			{{ error }}
		</div>
		<form @submit.prevent="doLogin">
			<div class="form-floating mb-3">
				<input
					v-model="email"
					class="form-control"
					aria-required="true"
					placeholder="name@example.com"
					type="email"
					id="Input_Email"
				/>
				<label class="form-label" htmlFor="Input_Email">Email</label>
			</div>
			<div class="form-floating mb-3">
				<input
					v-model="password"
					class="form-control"
					aria-required="true"
					placeholder="password"
					type="password"
					id="Input_Password"
				/>
				<label class="form-label" htmlFor="Input_Password">
					Password
				</label>
			</div>
			<div>
				<button
					id="login-submit"
					type="submit"
					class="w-100 btn btn-lg btn-dark"
				>
					Log in
				</button>
			</div>
			<RouterLink
				to="/register"
				class="link-body-emphasis link-offset-2 link-underline-opacity-25 link-underline-opacity-75-hover mt-2"
			>
				Register
			</RouterLink>
		</form>
	</div>
</template>

<style scoped>
form {
	width: 30%;
}

.content {
	height: 100%;
	display: flex;
	justify-content: center;
	align-items: center;
	position: relative;
	background: url('@/assets/logo.svg') top center no-repeat;
	background-size: 50% 30%;
}

.content::before {
	position: absolute;
	content: '';
	left: auto;
	top: 36%;
	width: 60%;
	height: 30%;
	background: #cecece;
	border-radius: 62% 47% 82% 35% / 45% 45% 80% 66%;
	will-change: border-radius, transform, opacity;
	animation: sliderShape 5s linear infinite;
	display: block;
	z-index: -1;
	-webkit-animation: sliderShape 5s linear infinite;
}

@keyframes sliderShape {
	0%,
	100% {
		border-radius: 42% 58% 70% 30% / 45% 45% 55% 55%;
		transform: translate3d(0, 0, 0) rotateZ(0.01deg);
	}

	34% {
		border-radius: 70% 30% 46% 54% / 30% 29% 71% 70%;
		transform: translate3d(0, 5px, 0) rotateZ(0.01deg);
	}

	50% {
		transform: translate3d(0, 0, 0) rotateZ(0.01deg);
	}

	67% {
		border-radius: 100% 60% 60% 100% / 100% 100% 60% 60%;
		transform: translate3d(0, -3px, 0) rotateZ(0.01deg);
	}
}
</style>
