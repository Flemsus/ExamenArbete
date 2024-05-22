<script>
	import { createEventDispatcher } from 'svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	const dispatch = createEventDispatcher();

	let name = '';
	let email = '';
	let password = '';
	let errorMessage = '';

	async function handleSubmit() {
		const userData = {
			name,
			email,
			password
		};

		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/create`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(userData)
			});

			if (response.ok) {
				dispatch('userCreated');
				alert('User added successfully.');
				name = '';
				email = '';
				password = '';
			} else {
				const errorData = await response.json();
				errorMessage = errorData.message || 'Failed to create user.';
			}
		} catch (error) {
			console.error('Error creating user:', error);
			errorMessage = 'An error occurred while creating user. Please try again.';
		}
	}

	function validateEmail(email) {
		const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
		return emailRegex.test(email);
	}
</script>

<div class="new-user">
	<h2>Create New User</h2>
	{#if errorMessage}
		<p style="color: red;">{errorMessage}</p>
	{/if}
	<form on:submit|preventDefault={handleSubmit}>
		<label for="name">Name:</label>
		<input type="text" id="name" bind:value={name} required />
		<label for="email">Email:</label>
		<input type="email" id="email" bind:value={email} required />
		<label for="password">Password:</label>
		<input type="password" id="password" bind:value={password} required />
		<button type="submit">Create User</button>
	</form>
</div>

<style>
	h2 {
		text-align: center;
		font-size: 2em;
	}

	.new-user {
		margin: 1em;
	}

	form {
		margin-top: 20px;
	}

	label {
		display: block;
	}

	input {
		width: 100%;
		padding: 8px;
		margin-top: 5px;
		margin-bottom: 5px;
		border: 1px solid #ccc;
		border-radius: 4px;
		box-sizing: border-box;
	}

	button {
		background-color: #4caf50;
		color: white;
		padding: 10px 20px;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		margin-top: 10px;
	}

	button:hover {
		background-color: #45a049;
	}

	.new-user {
		margin-top: 5px;
		margin-bottom: 5px;
		border: solid orange 5px;
		padding: 1em;
		background: white;
	}
</style>
