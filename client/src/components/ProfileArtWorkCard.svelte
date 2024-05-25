<script>
	import { createEventDispatcher } from 'svelte';
	export let imageUrl;
	export let title;
	export let description;
	export let artwork;
	export let price;

	const dispatch = createEventDispatcher();
	let isDeletePopupVisible = false;

	function handleEdit() {
		dispatch('edit', artwork);
	}

	function handleDeleteConfirmation() {
        isDeletePopupVisible = true;
    }

    function handleDelete() {
        dispatch('delete', artwork);
        isDeletePopupVisible = false;
    }

    function closeDeletePopup() {
        isDeletePopupVisible = false;
    }
</script>

<div class="artwork-card">
    <h2>{title}</h2>
    <div class="image-container">
        <img src={imageUrl} alt={title} />
    </div>
    <p class="description">{description}</p>
    <p class="price">{price}</p>
    <div class="actions">
        <button on:click={handleEdit}>Edit</button>
        <button class="delete-button" on:click={handleDeleteConfirmation}>Delete</button>
    </div>
    {#if isDeletePopupVisible}
    <div class="popup">
        <p>Are you sure you want to delete this artwork?</p>
        <button class="delete-button" on:click={handleDelete}>Yes</button>
        <button on:click={closeDeletePopup}>Cancel</button>
    </div>
    {/if}
</div>

<style>
	.artwork-card {
		border: 1px solid #ff7b00;
		padding: 16px;
		margin: 16px;
		text-align: center;
		width: 240px;
		display: flex;
		flex-direction: column;
		align-items: center;
		border-radius: 8px;
	}

	.artwork-card h2 {
		font-size: 24px;
		margin-bottom: 10px;
		margin-top: 4px;
	}

	.image-container {
		width: 100%;
		height: 200px;
		display: flex;
		justify-content: center;
		align-items: center;
		margin-bottom: 8px;
	}

	.artwork-card img {
		max-width: 100%;
		max-height: 100%;
		object-fit: cover;
	}

	.description {
		border: 1px solid #ff7b00;
		padding: 8px;
		margin-top: 8px;
		background-color: #f0f0f0;
		width: 100%;
		text-align: left;
		border-radius: 8px;
	}

	.price {
		border: 1px solid #ff7b00;
		padding: 8px;
		margin-top: 8px;
		background-color: #f0f0f0;
		width: 100%;
		text-align: left;
		border-radius: 8px;
	}

	.actions {
		display: flex;
		justify-content: center;
		gap: 8px;
		margin-top: auto;
		margin-bottom: 16px;
		width: 100%;
		
	}

	.artwork-card .delete-button {
		padding: 8px 16px;
		background-color: #dc3545;
        color: #ffffff;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		transition: background-color 0.3s ease, transform 0.3s ease;
}

.artwork-card .delete-button:hover {
	background-color: #bd2130; 
}

	.actions button {
		padding: 8px 16px;
		background-color: #ff7b00;
		color: #fff;
		border: none;
		border-radius: 4px;
		cursor: pointer;
		transition: background-color 0.3s ease, transform 0.3s ease;
	}

	.actions button:hover {
		background-color: #333333;
	}

	.popup {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #fff;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        z-index: 9999;
    }

    .popup p {
        margin: 0;
        margin-bottom: 10px;
    }

    .popup button {
        padding: 8px 16px;
        background-color: #ff7b00;
        color: #fff;
        border: none;
        border-radius: 4px;
        cursor: pointer;
        margin-right: 10px;
		transition: background-color 0.3s ease, transform 0.3s ease;
    }

    .popup button:hover {
        background-color: #333333;
    }
</style>
