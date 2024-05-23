<script>
	import { onMount } from 'svelte';
	import ArtworkCard from './../../components/ArtworkCard.svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let loggedInUser = null;
	let successMessage = '';
	let errorMessage = '';

	let artworks = [];
	let users = {};

	onMount(async () => {
		loggedInUser = localStorage.getItem('loggedInUser');

		try {
			const [artworksResponse, usersResponse] = await Promise.all([
				fetch(`${PUBLIC_API_URL}/user/allArt`),
				fetch(`${PUBLIC_API_URL}/user/all`) // Fetch user data
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

	let newTitle = '';
	let newArtistId = '';
	let newImageUrl = '';
	let newArtDescription = '';
	let newArtPrice = '';

	async function addArtwork() {
		if (newTitle && newArtistId && newImageUrl) {
			try {
				const response = await fetch(`${PUBLIC_API_URL}/user/addArt`, {
					method: 'POST',
					headers: {
						'Content-Type': 'application/json'
					},
					body: JSON.stringify({
						artName: newTitle,
						artCreatorId: parseInt(newArtistId),
						imageUrl: newImageUrl,
						artDescription: newArtDescription,
						artPrice: newArtPrice
					})
				});

				if (!response.ok) {
					throw new Error('Failed to add artwork');
				}

				const newArtwork = await response.json();
				artworks = [...artworks, newArtwork];

				newTitle = '';
				newArtistId = '';
				newImageUrl = '';
				newArtDescription = '';
				newArtPrice = '';
				successMessage = 'Artwork added successfully!';
			} catch (error) {
				successMessage = 'Error adding artwork: ' + error.message;
			}
		} else {
			successMessage = 'Please provide all required information.';
		}
	}
</script>

<div class="featured-artworks">
	{#each artworks as artwork}
		{#if users[artwork.artCreatorId]}
			<!-- Check if the user exists -->
			<ArtworkCard
				imageUrl={artwork.imageUrl}
				title={artwork.artName}
				artist={users[artwork.artCreatorId].name}
				description={artwork.artDescription}
				price={artwork.artPrice}
			/>
		{/if}
	{/each}
</div>

<div class="upload-form">
	<h2>Upload New Artwork</h2>
	<input type="text" placeholder="Artwork Title" bind:value={newTitle} />
	<input type="number" placeholder="Artist ID" bind:value={newArtistId} />
	<input type="text" placeholder="Artwork Image URL" bind:value={newImageUrl} />
	<input type="text" placeholder="Artwork Description" bind:value={newArtDescription} />
	<div class="price-input">
		<input type="text" placeholder="Artwork Price (€)" bind:value={newArtPrice} />
	</div>
	<button on:click={addArtwork}>Add Artwork</button>
	<p>{successMessage}</p>
</div>

<style>
	.featured-artworks {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
	}
	.upload-form {
		margin: 32px;
		text-align: center;
	}
	.upload-form input,
	.upload-form button {
		display: block;
		margin: 8px auto;
	}
</style>
