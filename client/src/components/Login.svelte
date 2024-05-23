<script>
	import { writable } from 'svelte/store';
	import { PUBLIC_API_URL } from '$env/static/public';

	let email = ''; // Declare email variable
	let password = ''; // Declare password variable

	const loggedInUser = writable(null);

	async function loginUser(email, password) {
		const userData = { email, password };

		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/login`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(userData)
			});

			if (response.ok) {
				const responseData = await response.json();
				loggedInUser.set(responseData); // Store the entire user object
				localStorage.setItem('loggedInUser', JSON.stringify(responseData)); //
				window.location.reload();
			} else {
				alert('Invalid email or password.');
			}
		} catch (error) {
			console.error('Error during login:', error);
		}
	}

	function handleKeyPress(event) {
		if (event.key === 'Enter') {
			loginUser(email, password);
		}
	}
</script>

<div class="container">
	<div class="input-container">
		<i class="fas fa-user icon"></i>
		<input
			type="email"
			id="email"
			class="input-field"
			placeholder="Email"
			bind:value={email}
			on:keypress={handleKeyPress}
		/>
	</div>

	<div class="input-container">
		<i class="fas fa-key icon"></i>
		<input
			type="password"
			id="password"
			class="input-field"
			placeholder="Password"
			bind:value={password}
			on:keypress={handleKeyPress}
		/>
	</div>

	<button class="login-btn" on:click={() => loginUser(email, password)}>Login</button>
	<a href="/register-user" class="register">Register here!</a>
	<a href="/forgot-password" class="forgot-password">Forgot your password?</a>
</div>

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
		border: 1px solid #ccc;
		border-radius: 5px;
		box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
		background-color: white;
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
