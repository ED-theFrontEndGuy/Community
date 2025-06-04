<script setup lang="ts"></script>

<template>
	<div class="row">
		<div class="col-4"></div>
		<div class="col-4">
			<form>
				<div
					asp-validation-summary="ModelOnly"
					class="text-danger"
					role="alert"
				></div>
				<div class="form-floating mb-3">
					<input
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
