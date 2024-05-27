<script>
	import { createEventDispatcher } from 'svelte';

	export let artwork;
	export let onClose;

	const dispatch = createEventDispatcher();

	let editedArtwork = { ...artwork };

	function handleSubmit() {
		if (!/^\d+$/.test(editedArtwork.artPrice)) {
			alert('Price must be a number.');
			return;
		}
		dispatch('save', editedArtwork);
		onClose();
	}
</script>

<div class="modal-overlay">
	<div class="modal-content">
		<h2>Edit Artwork</h2>
		<form on:submit|preventDefault={handleSubmit}>
			<div class="form-group">
				<label for="title">Title:</label>
				<input type="text" id="title" bind:value={editedArtwork.artName} maxlength="50" />
			</div>
			<div class="form-group">
				<label for="description">Description:</label>
				<textarea id="description" bind:value={editedArtwork.artDescription} maxlength="100"
				></textarea>
			</div>
			<div class="form-group">
				<label for="price">Price: (€)</label>
				<input type="text" id="price" bind:value={editedArtwork.artPrice} maxlength="20" />
			</div>
			<div class="buttons">
				<button type="submit" class="save-btn">Save</button>
				<button type="button" class="cancel-btn" on:click={onClose}>Cancel</button>
			</div>
		</form>
	</div>
</div>

<style>

h2 {
	font-size: 20px;
	font-weight: bold;
	margin-bottom: 10px;
	text-align: center;
}

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
		transition: background-color 0.3s;
	}

	.save-btn:hover {
		background-color: #333333;
	}

	.cancel-btn {
		background-color: #dc3545;
        color: #ffffff;
		border: none;
		padding: 10px 20px;
		border-radius: 3px;
		cursor: pointer;
		transition: background-color 0.3s;
	}

    .cancel-btn:hover {
        background-color: #bd2130;
        
    }
</style>
