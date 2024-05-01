<script lang="ts">
	export let val = '';
	export let type = 'text';
	export let placeholder = '';
	function handleInput(event) {
		val = event.target.value;
	}
</script>

<div>
	<slot />
	<input {type} id="input-{val}" {placeholder} on:input={handleInput} value={val} />
</div>

<style lang="scss">
	@use 'static/css/theme.scss';

	input {
		width: 100%;
		padding: 10px 10px 10px 30px;
		margin-bottom: 10px;
		border: 1px solid #ccc;
		background-color: #f1f0f0;
		box-shadow:
			0 0 0 #fff inset,
			0 0 0 #fff inset,
			0 -2px 0 theme.$button-bg-dark inset;
	}
	input:focus {
		box-shadow:
			0 0 0 #fff inset,
			0 0 0 #fff inset,
			0 -5px 0 theme.$button-bg-dark-hover inset;
		outline: none;
	}

	div {
		position: relative;
	}
</style>
