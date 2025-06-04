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
	<div class="row">
		<div class="col-4"></div>
		<div class="col-4">
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
					<label class="form-label" htmlFor="Input_Email">
						Email
					</label>
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
	</div>
</template>
