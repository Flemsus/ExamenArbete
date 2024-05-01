<script lang="ts">
    export let small: any = undefined;
    export let disabled = false;

</script>

<button class:small on:click {disabled}>
    <slot/>
</button>

<style lang="scss">
    @use "static/css/theme.scss";

    button {
        width: 100%;
        background-color: theme.$button-bg-dark;
        color: #fff;
        text-transform: uppercase;
        padding: 10px;
        letter-spacing: 2px;
        transition: background-color 0.3s ease;
        border: none;
        border-radius: 1px;
        text-decoration: none;
        cursor: pointer;
    }
    
    button:hover {
        background: theme.$button-bg-dark-hover;
        border-color: theme.$color-border-dark;
        transition: all 0.2s;
    }
    
    button:disabled {
        background: theme.$color-neutral;
    }

    .small {
        padding: 10px 20px;
        width: max-content;
    }
</style>
