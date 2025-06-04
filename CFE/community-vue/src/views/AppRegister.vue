<script setup lang="ts"></script>

<template>
	<div class="row">
		<div class="col-4"></div>
		<div class="col-4">
			<form>
				<h2>Register</h2>
				<hr />
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
						placeholder="First name"
						type="text"
						id="Input_FirstName"
					/>
					<label class="form-label" htmlFor="Input_FirstName">
						Name
					</label>
				</div>

				<div class="form-floating mb-3">
					<input
						class="form-control"
						aria-required="true"
						placeholder="Last name"
						type="text"
						id="Input_LastName"
					/>
					<label class="form-label" htmlFor="Input_LastName">
						Surname
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
						id="register-submit"
						type="submit"
						class="w-100 btn btn-lg btn-dark"
					>
						Register
					</button>
				</div>
			</form>
		</div>
	</div>
</template>
