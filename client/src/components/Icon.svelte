<script>
    export let name;
    export let src;
    export let alt = "icon";
    export let link;
</script>

<div class="icon-container">
    <a href={link} class="icon-link"></a>
    <img {src} {alt} />
    <p>{name}</p>
</div>

<style lang="scss">
    .icon-container {
        position: relative; 
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        padding: 0.5em;
        margin: 0.5em;
        border: 1px solid #bfa47c;
        width: calc(50% - 1em); 
        aspect-ratio: 1/1;
        border-radius: 10px;
        background-color: #f2d09e;
        box-shadow: 5px 5px 5px #888888;
		min-width: 160px;
		font-size: 1.1em;
    }

    .icon-link {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        text-decoration: none; 
    }

    img {
        width: 70%;
        height: auto; 
    }

    p {
        font-weight: bold;
        font-size: 1.3em; 
        margin: 5px 0 0 0;
        max-width: 90%; 
        text-align: center; 

    }

    @media screen and (max-width: 768px) {
        .icon-container {
            width: calc(50% - 1em);
        }
        p {
            font-size: 0.9em; 
        }
    }

    @media screen and (min-width: 1200px) {
        .icon-container {
            width: calc(50% - 1em);
        }
        p {
            font-size: 1.5em; 
        }
    }
</style>
