<script>
	import { onMount } from 'svelte';
	import ArtworkCard from './../../components/ArtworkCard.svelte';

	let loggedInUser = null;
	let successMessage = '';

	onMount(() => {
		loggedInUser = localStorage.getItem('loggedInUser');
	});

	let users = {
		1: { name: 'Flemsus', profileUrl: '/user/1' },
		2: { name: 'Picasso', profileUrl: '/user/2' },
		3: { name: 'A random dog', profileUrl: '/user/3' }
	};

	let artworks = [
		{ imageUrl: 'images/Art1.png', title: 'Purple Boy', artistId: 1 },
		{ imageUrl: 'images/Art2.png', title: 'Mona Lisa', artistId: 2 },
		{ imageUrl: 'images/Art3.png', title: 'Woof', artistId: 3 }
	];

	let newTitle = '';
	let newArtistId = '';
	let newImageUrl = '';

	function handleFileUpload(event) {
		const file = event.target.files[0];
		const reader = new FileReader();
		reader.onload = () => {
			newImageUrl = reader.result;
		};
		reader.readAsDataURL(file);
	}

	function addArtwork() {
		if (newTitle && newArtistId && newImageUrl) {
			artworks = [
				...artworks,
				{ imageUrl: newImageUrl, title: newTitle, artistId: parseInt(newArtistId) }
			];
			newTitle = '';
			newArtistId = '';
			newImageUrl = '';
			successMessage = 'Artwork added successfully!';
		} else {
			successMessage = 'Please provide all required information.';
		}
	}
</script>

<div class="hero-section">
	<h1>Artistic Realms, Together!</h1>
	<p>Discover, buy, and sell stunning artworks</p>
</div>

<div class="featured-artworks">
	{#each artworks as artwork}
		{#if users[artwork.artistId]}
			<ArtworkCard
				imageUrl={artwork.imageUrl}
				title={artwork.title}
				artist={users[artwork.artistId].name}
				profileUrl={users[artwork.artistId].profileUrl}
			/>
		{/if}
	{/each}
</div>

<div class="upload-form">
	<h2>Upload New Artwork</h2>
	<input type="text" placeholder="Artwork Title" bind:value={newTitle} />
	<input type="number" placeholder="Artist ID" bind:value={newArtistId} />
	<input type="file" accept="image/*" on:change={handleFileUpload} />
	<button on:click={addArtwork}>Add Artwork</button>
</div>

<style>
	.hero-section {
		text-align: center;
		margin-bottom: 32px;
	}
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
