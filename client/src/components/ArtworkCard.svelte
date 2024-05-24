<script>
	import { onMount } from 'svelte';
	import PurchaseArtwork from './PurchaseArtwork.svelte';

	export let imageUrl;
	export let title;
	export let artist;
	export let description;
	export let price;

	let isLoggedIn = false;
	let showPurchasePopup = false;
	let loggedInUser = null;

	onMount(() => {
		loggedInUser = localStorage.getItem('loggedInUser');
		isLoggedIn = loggedInUser !== null;
	});

	function handleBuyNow() {
		if (isLoggedIn) {
			showPurchasePopup = true;
		}
	}
</script>

<div class="artwork-card">
	<h2>{title}</h2>
	<p class="artist">{artist}</p>
	<div class="image-container">
		<img src={imageUrl} alt={title} />
	</div>
	<div class="description-box">
		<p>{description}</p>
	</div>
	<div class="buy-box">
		<p class="price">{price}€</p>
		<button
			disabled={!isLoggedIn}
			class:disabled={!isLoggedIn}
			title={isLoggedIn ? '' : 'You must be logged in to purchase this item.'}
			on:click={handleBuyNow}>Buy Now</button
		>
	</div>
</div>

{#if showPurchasePopup}
    <PurchaseArtwork
        artwork={{ title, imageUrl }}
        artist={artist}
        loggedInUser={loggedInUser}
        on:close={() => (showPurchasePopup = false)}
    />
{/if}

<style>
	.artwork-card {
		border: 1px solid orange;
		padding: 14px;
		margin: 14px;
		text-align: center;
		width: 300px;
		display: flex;
		flex-direction: column;
		align-items: center;
		border-radius: 8px;
	}

	.artwork-card h2 {
		font-size: 24px;
		margin-bottom: 4px;
	}

	.artwork-card .artist {
		font-size: 18px;
		margin-top: 0;
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
		border: 2px solid orange;
		border-radius: 8px;
	}

	.artwork-card .description-box {
		border: 1px solid orange;
		padding: 8px;
		margin-top: auto;
		margin-bottom: 16px;
		text-align: left;
		background-color: #f0f0f0;
		width: 100%;
		border-radius: 8px;
	}

	.artwork-card .description-box p {
		margin-top: 0;
		font-size: 14px;
	}

	.artwork-card .buy-box {
		border: 1px solid orange;
		padding: 8px;
		margin-top: 16px;
		background-color: #f0f0f0;
		width: 100%;
		border-radius: 8px;
	}

	.artwork-card .buy-box button {
		background-color: orange;
		color: #fff;
		border: none;
		padding: 8px 16px;
		cursor: pointer;
		border-radius: 8px;
	}

	.artwork-card .buy-box button:hover {
		background-color: #333333;

	}

	.artwork-card .buy-box button.disabled {
		background-color: #ccc;
		color: #666;
		cursor: not-allowed;
	}
</style>
