<script>
	import { onMount } from 'svelte';
	import ProfileArtWorkCard from './../../components/ProfileArtWorkCard.svelte';
	import EditArtworkModal from './../../components/EditArtworkModal.svelte';
	import AddArtwork from './../../components/AddArtwork.svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let user = null;
	let showEditModal = false;
	let showAddArtwork = false;
	let selectedArtwork = null;

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

	function handleEditArtwork(event) {
		selectedArtwork = event.detail;
		showEditModal = true;
	}

	function handleSaveArtwork(event) {
		const updatedArtwork = event.detail;
		editArtwork(updatedArtwork);
	}

	async function editArtwork(artwork) {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/updateArt`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify(artwork)
			});

			if (response.ok) {
				const updatedArtworks = user.userArt.map((art) => {
					if (art.id === artwork.id) {
						return artwork;
					}
					return art;
				});
				
				user = { ...user, userArt: updatedArtworks };
			} else {
				console.error('Failed to update artwork:', response.status);
			}
		} catch (error) {
			console.error('Error updating artwork:', error);
		}
	}

	async function deleteArtwork(artwork) {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/deleteArt/${artwork.id}`, {
				method: 'DELETE'
			});

			if (response.ok) {

				user = { ...user, userArt: user.userArt.filter((art) => art.id !== artwork.id) };
			} else {
				console.error('Failed to delete artwork:', response.status);
			}
		} catch (error) {
			console.error('Error deleting artwork:', error);
		}
	}
</script>

<div class="profile-container">
<h1>Profile</h1>

{#if user}
	<div>
		<p>Name: {user.name}</p>
		<p>Email: {user.email}</p>
		<button class="addart-btn" on:click={() => (showAddArtwork = true)}>Add Artwork</button>
		{#if showAddArtwork}
			<AddArtwork {user} onClose={() => (showAddArtwork = false)} />
		{/if}
	</div>

	<h2>Artworks: (Logout and Login again for them to appear in this list for real)</h2>
	<div class="artworks-container">
		{#each user.userArt as artwork}
			<ProfileArtWorkCard
				imageUrl={artwork.imageUrl}
				title={artwork.artName}
				description={artwork.artDescription}
				price={`Current price: ${artwork.artPrice}€`}
				{artwork}
				on:edit={handleEditArtwork}
				on:delete={() => deleteArtwork(artwork)}
			/>
		{/each}
	</div>
{/if}

{#if showEditModal}
	<EditArtworkModal
		artwork={selectedArtwork}
		onClose={() => (showEditModal = false)}
		on:save={handleSaveArtwork}
	/>
{/if}
</div>

<style>

	.artworks-container {
		display: flex;
		flex-wrap: wrap;
		justify-content: center;
		padding: 1em;
	}

	.addart-btn {
		padding: 8px 16px;
		background-color: #e3a13d;
		color: #fff;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		
	}

	.addart-btn:hover {
		background-color: #333333;
	}

	h2 {
		font-size: 24px;
		margin-bottom: 10px;
		margin-top: 10px;
		
	}
</style>
