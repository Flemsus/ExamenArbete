<script>
	import { createEventDispatcher } from 'svelte';
	export let imageUrl;
	export let title;
	export let description;
	export let artwork;
	export let price;

	const dispatch = createEventDispatcher();

	function handleEdit() {
		dispatch('edit', artwork);
	}

	function handleDelete() {
		const confirmed = confirm('Are you sure you want to delete this artwork?');
		if (confirmed) {
			dispatch('delete', artwork);
		}
	}
</script>

<div class="artwork-card">
	<h2>{title}</h2>
	<div class="image-container">
		<img src={imageUrl} alt={title} />
	</div>
	<p class="description">{description}</p>
	<p class="price">{price}€</p>
	<div class="actions">
		<button on:click={handleEdit}>Edit</button>
		<button on:click={handleDelete}>Delete</button>
	</div>
</div>

<style>
	.artwork-card {
		border: 1px solid orange;
		padding: 16px;
		margin: 16px;
		text-align: center;
		width: 300px;
		display: flex;
		flex-direction: column;
		align-items: center;
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
		border: 1px solid orange;
		padding: 8px;
		margin-top: 8px;
		background-color: #f0f0f0;
		width: 100%;
		text-align: left;
	}

	.price {
		border: 1px solid orange;
		padding: 8px;
		margin-top: 8px;
		background-color: #f0f0f0;
		width: 100%;
		text-align: left;
	}

	.actions {
		display: flex;
		justify-content: center;
		gap: 8px;
		margin-top: auto;
		margin-bottom: 16px;
		width: 100%;
	}

	.actions button {
		padding: 8px 16px;
		background-color: #e3a13d;
		color: #fff;
		border: none;
		border-radius: 4px;
		cursor: pointer;
	}

	.actions button:hover {
		background-color: #0056b3;
	}
</style>
