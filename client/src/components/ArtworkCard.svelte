<script>
	export let imageUrl;
	export let title;
	export let artist;
	export let description;
	export let price;
</script>

<div class="artwork-card">
	<h2>{title}</h2>
	<p class="artist">{artist}</p>
	<img src={imageUrl} alt={title} />
	<div class="description-box">
		<p>{description}</p>
	</div>
	<div class="buy-box">
		<p class="price">{price}€</p>
		<button>Buy Now</button>
	</div>
</div>

<style>
	.artwork-card {
		border: 1px solid orange;
		padding: 16px;
		margin: 16px;
		text-align: center;
		width: 300px;
	}

	.artwork-card img {
		max-width: 100%;
		max-height: 200px;
		object-fit: cover;
		border: 2px solid orange;
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

	.artwork-card .description-box {
		border: 1px solid orange;
		padding: 8px;
		margin-top: 8px;
		text-align: left;
		background-color: #f0f0f0;
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
	}

	.artwork-card .buy-box button {
		background-color: orange;
		color: #fff;
		border: none;
		padding: 8px 16px;
		cursor: pointer;
	}
</style>
