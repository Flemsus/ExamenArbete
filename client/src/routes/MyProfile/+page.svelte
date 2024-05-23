<script>
	import { onMount } from 'svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let user = null;

	onMount(async () => {
		try {
			const loggedInUserEmail = JSON.parse(localStorage.getItem('loggedInUser')).email;

			const response = await fetch(`${PUBLIC_API_URL}/user/${loggedInUserEmail}`);

			if (response.ok) {
				user = await response.json();
				console.log('User data:', user);
			} else {
				console.error('Failed to fetch user:', response.statusText);
			}
		} catch (error) {
			console.error('Error fetching user:', error);
		}
	});
</script>

<h1>User Information</h1>

{#if user}
	<div>
		<p>Name: {user.name}</p>
		<p>Email: {user.email}</p>
		<h2>Artworks</h2>
		{#if user.userArt.length > 0}
			<ul>
				{#each user.userArt as artwork}
					<li>
						<p><strong>{artwork.artName}</strong></p>
						<p>{artwork.artDescription}</p>
						<p>Price: {artwork.artPrice}</p>
					</li>
				{/each}
			</ul>
		{:else}
			<p>No artworks found.</p>
		{/if}
	</div>
{:else}
	<p>No user logged in.</p>
{/if}
