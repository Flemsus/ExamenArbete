<script>
	import { createEventDispatcher } from 'svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	export let user;
	export let onClose;

	const dispatch = createEventDispatcher();

	let newTitle = '';
	let newImageUrl = '';
	let newArtDescription = '';
	let newArtPrice = '';
	let successMessage = '';

	async function handleSubmit() {
		try {
			if (!/^\d+$/.test(newArtPrice)) {
				throw new Error('Price must be a number.');
			}

			const response = await fetch(`${PUBLIC_API_URL}/user/addArt`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					artName: newTitle,
					artCreatorId: user.id,
					imageUrl: newImageUrl,
					artDescription: newArtDescription,
					artPrice: newArtPrice
				})
			});

			if (!response.ok) {
				throw new Error('Failed to add artwork');
			}

			newTitle = '';
			newImageUrl = '';
			newArtDescription = '';
			newArtPrice = '';
			successMessage = 'Artwork added successfully!';
			dispatch('close');
		} catch (error) {
			successMessage = 'Error adding artwork: ' + error.message;
		}
	}
</script>

<div class="modal-overlay">
	<div class="modal-content">
		<h2>Add New Artwork</h2>
		<form on:submit|preventDefault={handleSubmit}>
			<div class="form-group">
				<label for="title">Title:</label>
				<input type="text" id="title" bind:value={newTitle} maxlength="50" required />
			</div>
			<div class="form-group">
				<label for="imageUrl">Image URL:</label>
				<input type="text" id="imageUrl" bind:value={newImageUrl} required />
			</div>
			<div class="form-group">
				<label for="description">Description:</label>
				<textarea id="description" bind:value={newArtDescription} maxlength="100"></textarea>
			</div>
			<div class="form-group">
				<label for="price">Price: (€)</label>
				<input type="text" id="price" bind:value={newArtPrice} maxlength="20" required />
			</div>
			<div class="buttons">
				<button type="submit" class="save-btn">Add Artwork</button>
				<button type="button" class="cancel-btn" on:click={onClose}>Cancel</button>
			</div>
		</form>
		<p>{successMessage}</p>
	</div>
</div>

<style>
	.modal-overlay {
		position: fixed;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		background-color: rgba(0, 0, 0, 0.5);
		display: flex;
		justify-content: center;
		align-items: center;
		z-index: 1000;
	}

	.modal-content {
		background-color: #fff;
		padding: 20px;
		border-radius: 5px;
		box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
		max-width: 500px;
		width: 100%;
	}

	.form-group {
		margin-bottom: 20px;
	}

	label {
		display: block;
		font-weight: bold;
		margin-bottom: 5px;
	}

	input[type='text'],
	textarea {
		width: 100%;
		padding: 10px;
		border: 1px solid #ccc;
		border-radius: 3px;
	}

	.buttons {
		display: flex;
		justify-content: flex-end;
	}

	.save-btn {
		background-color: #ff7b00;
		color: #fff;
		border: none;
		padding: 10px 20px;
		border-radius: 3px;
		cursor: pointer;
		margin-right: 10px;
	}

	.cancel-btn {
		background-color: #ccc;
		color: #333;
		border: none;
		padding: 10px 20px;
		border-radius: 3px;
		cursor: pointer;
	}
</style>
