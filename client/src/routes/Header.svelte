<script>
	import { onMount } from 'svelte';
	import { onDestroy } from 'svelte';
	import Popup from './../components/Popup.svelte';
	import Login from './../components/Login.svelte';
	import { writable } from 'svelte/store';

	const isOpen = writable(false);
	const loggedInUser = writable(null);

	function handlePageChange(event) {
		isOpen.set(false);
	}

	onMount(() => {
		window.addEventListener('popstate', handlePageChange);

		loggedInUser.set(localStorage.getItem('loggedInUser'));

		onDestroy(() => {
			window.removeEventListener('popstate', handlePageChange);
		});
	});

	async function logoutUser() {
		localStorage.removeItem('loggedInUser');
		loggedInUser.set(null);
		window.location.reload();
	}

	function redirectToProfile() {
		window.location.href = '/MyProfile';
	}
</script>

<div class="header">
	<div class="logo"><a href="/">Art Reveals Talent</a></div>
	<div class="nav">
		<a href="/">Home</a>
		<a href="/Gallery">Gallery</a>
	</div>
	{#if $loggedInUser !== null}
		<div class="user-dropdown">
			<span>{$loggedInUser}</span>
			<div class="dropdown-content">
				<a href="/MyProfile" on:click={redirectToProfile}>My Profile</a>
				<a href="/" on:click={logoutUser}>Logout</a>
			</div>
		</div>
	{:else if $isOpen}
		<Popup bind:isOpen={$isOpen}>
			<div class="popup-content">
				<Login />
			</div>
		</Popup>
	{:else}
		<button class="login-btn" on:click={() => isOpen.set(true)}>Login</button>
	{/if}
</div>

<style>
	.header {
		background-color: #333;
		color: white;
		padding: 1rem;
		display: flex;
		justify-content: space-between;
		align-items: center;
		position: sticky;
		top: 0;
		z-index: 1000;
	}

	.logo {
		font-size: 1.5rem;
		font-weight: bold;
	}

	.nav {
		display: flex;
		gap: 1rem;
		justify-content: center;
	}

	.nav a {
		color: white;
		text-decoration: none;
		padding: 0.5rem 1rem;
		border-radius: 5px;
		transition: background-color 0.3s;
	}

	.nav a:hover {
		background-color: #555;
	}

	.login-btn {
		background-color: #ff7b00;
		border: none;
		color: white;
		padding: 0.5rem 1rem;
		border-radius: 5px;
		cursor: pointer;
		transition: background-color 0.3s;
	}

	.login-btn:hover {
		background-color: #ff9500;
	}
</style>
