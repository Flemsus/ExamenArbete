<script>
	export let isOpen = false;
</script>

{#if isOpen}
	<div class="popup">
		<slot />
	</div>
{/if}

<!-- 
    
  (This is mostly for me so I can be less confused when adding it elsewhere ) 
    
How to use

    <script>
    let isOpen = false;
    </script>

   <Popup bind:isOpen={isOpen}>
    <div class="popup-content">
        This is a popup.
        <button on:click={() => isOpen = false}>Close</button>
    </div>
</Popup>

<button on:click={() => isOpen = true}>Open Popup 1</button>



If you want multiple you need to add this for each:



<script>
let isOpen1 = false;
</script>

<Popup bind:isOpen={isOpen1}>
    <div class="popup-content">
        This is the second popup.
        <button on:click={() => isOpen1 = false}>Close</button>
    </div>
</Popup>

<button on:click={() => isOpen1 = true}>Open Popup 2</button>
-->

<style>
	.popup {
		position: fixed;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		background-color: rgba(255, 255, 255, 0.5);
		z-index: 999;
		display: flex;
		justify-content: center;
		align-items: center;
	}
</style>
