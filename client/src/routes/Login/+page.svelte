<script>
	import { PUBLIC_API_URL } from '$env/static/public';
	import { onMount } from 'svelte';

	let email = '';
	let password = '';
	let loggedInUser = null;

	onMount(() => {
		loggedInUser = localStorage.getItem('loggedInUser');
	});

	async function loginUser() {
		const userData = {
			email: email,
			password: password
		};

		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/login`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(userData)
			});

			console.log('Response status:', response.status);

			const responseData = await response.json();
			console.log('Response data:', responseData);

			if (response.ok) {
				console.log(`Login successful. Logged in as ${responseData.name}`);
				loggedInUser = responseData.name;
				localStorage.setItem('loggedInUser', responseData.name);
				window.location.href = '/';
			} else {
				console.error('Login failed:', responseData.message);
				alert('Invalid email or password.');
				email = '';
				password = '';
			}
		} catch (error) {
			console.error('Error during login:', error);
		}
	}

	function logoutUser() {
		localStorage.removeItem('loggedInUser');
		loggedInUser = null;
		email = '';
		password = '';
		window.location.reload();
	}
</script>

{#if loggedInUser === null}
	<div class="container">
		<div class="input-container">
			<i class="fas fa-user icon"></i>
			<input
				type="email"
				id="email"
				class="input-field"
				placeholder="Epostadress"
				bind:value={email}
			/>
		</div>

		<div class="input-container">
			<i class="fas fa-key icon"></i>
			<input
				type="password"
				id="password"
				class="input-field"
				placeholder="Lösenord"
				bind:value={password}
			/>
		</div>

		<button class="login-btn" on:click={loginUser}>Logga in</button>
		<a href="/register-user" class="register">Registrera</a>
		<a href="/forgot-password" class="forgot-password">Glömt lösenord</a>
	</div>
{:else}
	<p>Logged In User: {loggedInUser}</p>
	<button on:click={logoutUser}>Logout</button>
{/if}

<style>
	.container {
		position: fixed;
		top: 50%;
		left: 50%;
		transform: translate(-50%, -50%);
		width: 90%;
		max-width: 400px;
		margin: 0 auto;
		padding: 20px;
		background-color: #fff;
		border: 1px solid #ccc;
		border-radius: 5px;
		box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
	}

	.input-field {
		width: 100%;
		padding: 10px;
		margin-bottom: 10px;
		border: 1px solid #ccc;
		border-radius: 5px;
	}

	.input-container {
		position: relative;
	}

	.icon {
		position: absolute;
		left: 10px;
		top: 50%;
		transform: translateY(-50%);
		font-size: 16px;
		color: #777;
	}

	.input-field {
		padding-left: 30px;
	}

	.login-btn {
		width: 100%;
		padding: 10px;
		background-color: #e3a13d;
		color: #fff;
		border: none;
		border-radius: 5px;
		cursor: pointer;
		text-decoration: none;
		transition: background-color 0.3s ease;
	}

	.login-btn:hover {
		background-color: #0056b3;
	}

	.forgot-password,
	.register {
		display: block;
		text-align: center;
		margin-top: 10px;
		color: #007bff;
		text-decoration: none;
	}

	.forgot-password:hover,
	.register:hover {
		text-decoration: underline;
	}

	@media screen and (min-width: 768px) {
		.container {
			width: 50%;
		}
	}

	@media screen and (min-width: 1024px) {
		.container {
			width: 30%;
		}
	}
</style>
