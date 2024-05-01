<script>
	import { onMount } from 'svelte';

	let loggedInUser = null;

	onMount(() => {
		loggedInUser = localStorage.getItem('loggedInUser');
	});

	async function logoutUser() {
		localStorage.removeItem('loggedInUser');
		loggedInUser = null;
		window.location.reload();
	}
</script>

{#if loggedInUser !== null}
	<div class="logged-in-user">
		Logged In User: {loggedInUser}
		<button on:click={logoutUser}>Logout</button>
	</div>
{/if}

<style>
	.logged-in-user {
		position: fixed;
		top: 20px;
		left: 20px;
		background-color: #fff;
		padding: 5px 10px;
		border-radius: 5px;
		box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
	}

	button {
		display: block;
		margin: 0 auto;
	}
</style>
