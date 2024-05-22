<script>
	export let imageUrl;
	export let title;
	export let artist;
</script>

<div class="artwork-card">
	<img src={imageUrl} alt={title} />
	<h2>{title}</h2>
	<p>{artist}</p>
</div>

<style>
	.artwork-card {
		border: 1px solid #ccc;
		padding: 16px;
		margin: 16px;
		text-align: center;
		width: 300px;
	}

	.artwork-card img {
		max-width: 100%;
		max-height: 200px;
		object-fit: cover;
	}
</style>
