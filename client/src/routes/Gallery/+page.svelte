<script>
	import { onMount } from 'svelte';
	import ArtworkCard from './../../components/ArtworkCard.svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let loggedInUser = null;
	let errorMessage = '';

	let artworks = [];
	let users = {};

	onMount(async () => {
		loggedInUser = localStorage.getItem('loggedInUser');

		try {
			const [artworksResponse, usersResponse] = await Promise.all([
				fetch(`${PUBLIC_API_URL}/user/allArt`),
				fetch(`${PUBLIC_API_URL}/user/all`) 
			]);

			if (!artworksResponse.ok || !usersResponse.ok) {
				throw new Error('Failed to fetch data');
			}

			const [artworksData, usersData] = await Promise.all([
				artworksResponse.json(),
				usersResponse.json()
			]);

			artworks = artworksData;
			users = usersData.reduce((acc, user) => {
				acc[user.id] = user;
				return acc;
			}, {});
		} catch (error) {
			console.error('Error fetching data:', error);
			errorMessage = 'Error fetching data. Please try again later.';
		}
	});


</script>

<svelte:head>
	<title>Gallery</title>
	<meta name="description" content="Gallery" />
</svelte:head>

<div class="featured-artworks">
	{#each artworks as artwork}
		{#if users[artwork.artCreatorId]}
			<ArtworkCard
				imageUrl={artwork.imageUrl}
				title={artwork.artName}
				artist={users[artwork.artCreatorId].name}
				description={artwork.artDescription}
				price={artwork.artPrice}
				userId={artwork.artCreatorId}
			/>
		{/if}
	{/each}
</div>
<div class="empty-space"> </div>

<style>
	.featured-artworks {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}

	.empty-space {
		height: 1em;
	}
	
</style>
