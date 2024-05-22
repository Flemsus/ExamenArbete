<script>
	import { onMount } from 'svelte';

	let user = null;

	onMount(() => {
		const loggedInUser = localStorage.getItem('loggedInUser');
		if (loggedInUser) {
			try {
				user = JSON.parse(loggedInUser);
				console.log('User data:', user);
			} catch (error) {
				console.error('Error parsing user data:', error);
			}
		} else {
			console.warn('No user data found in localStorage.');
		}
	});
</script>

<h1>User Information</h1>

{#if user}
	<div>
		<p>Name: {user.name}</p>
		<p>Email: {user.email}</p>
	</div>
{:else}
	<p>No user logged in.</p>
{/if}
