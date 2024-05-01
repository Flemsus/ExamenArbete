<script>
	import Footer from './Footer.svelte';
	import './styles.css';
	import LoggedInUser from '../components/LoggedInUser.svelte';
</script>

<div class="app">
	<LoggedInUser />

	<main>
		<slot />
	</main>

	<Footer />
</div>

<style>
	:global(*) {
		box-sizing: border-box;
	}
	.app {
		display: flex;
		flex-direction: column;
		min-height: 100vh;
	}

	main {
		flex: 1;
		display: flex;
		flex-direction: column;
		padding: 0rem;
		width: 100vw;
		max-width: 64rem;
		margin: 0 auto;
		/* TODO: Can we make this dynamic? */
		padding-bottom: 60px;
	}
</style>
