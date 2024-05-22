<script>
	import { onMount } from 'svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let users = [];
	let userToDelete = null;

	onMount(async () => {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/all`);
			if (response.ok) {
				users = await response.json();
			} else {
				console.error('Failed to fetch users:', response.statusText);
			}
		} catch (error) {
			console.error('Error fetching users:', error);
		}
	});

	async function deleteUser(id) {
		userToDelete = users.find((user) => user.id === id);
	}

	async function confirmDelete() {
		if (userToDelete) {
			try {
				const response = await fetch(`${PUBLIC_API_URL}/user/delete/${userToDelete.id}`, {
					method: 'DELETE'
				});
				if (response.ok) {
					users = users.filter((user) => user.id !== userToDelete.id);
					userToDelete = null;
				} else {
					console.error('Failed to delete user:', response.statusText);
				}
			} catch (error) {
				console.error('Error deleting user:', error);
			}
		}
	}

	async function cancelDelete() {
		userToDelete = null;
	}
</script>

<svelte:head>
	<title>Show Users</title>
	<meta name="description" content="Users" />
</svelte:head>

<div class="container">
	<ul class="user-list">
		<h2>All Users</h2>
		{#each users as user}
			<li class="user-item">
				<div>ID: {user.id}</div>
				<div>Name: {user.name}</div>
				<div>Email: {user.email}</div>
				<div>Password: {user.password}</div>
				<button on:click={() => deleteUser(user.id)}>Delete</button>
				{#if userToDelete && userToDelete.id === user.id}
					<div class="confirmation-modal">
						<p>
							Are you sure you want to delete this user?<br />
							ID: {user.id}<br />
							Name: {user.name}<br />
							Email: {user.email}<br />
							Password: {user.password}
						</p>
						<div class="buttons">
							<button on:click={confirmDelete}>Yes</button>
							<button on:click={cancelDelete}>No</button>
						</div>
					</div>
				{/if}
			</li>
		{/each}
	</ul>
</div>

<style>
	h2 {
		text-align: center;
		font-size: 2em;
	}

	.container {
		margin: 20px;
	}

	.user-list {
		list-style: none;
		padding: 0;
		border: solid red 5px;
		background-color: white;
	}

	.user-item {
		padding: 10px;
		border-top: solid red 5px;
	}

	.user-item div {
		margin-bottom: 5px;
	}

	button {
		background-color: #f44336;
		color: white;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		padding: 5px 10px;
		margin-top: 5px;
	}

	.confirmation-modal {
		position: fixed;
		top: 50%;
		left: 50%;
		transform: translate(-50%, -50%);
		background-color: white;
		padding: 20px;
		border-radius: 8px;
		box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
		z-index: 999;
	}

	.buttons {
		display: flex;
		justify-content: space-between;
		margin-top: 20px;
	}
</style>
