<script>
    import { onMount } from 'svelte';
    import PurchaseArtwork from './PurchaseArtwork.svelte';

    export let imageUrl;
    export let title;
    export let artist;
    export let description;
    export let price;
    export let userId;

    let isLoggedIn = false;
    let showPurchasePopup = false;
    let loggedInUser = null;
    let isArtistUser = false;

    onMount(() => {
        loggedInUser = JSON.parse(localStorage.getItem('loggedInUser'));
        isLoggedIn = loggedInUser !== null;
        if (isLoggedIn && loggedInUser.id === userId) {
            isArtistUser = true;
        }
    });

    function handleBuyNow() {
        if (isLoggedIn && !isArtistUser) {
            showPurchasePopup = true;
        }
    }
</script>

<div class="artwork-card">
    <h2>{title}</h2>
    <a class="artist" href={`/user/${userId}`}>{artist}</a>
    <div class="image-container">
        <img src={imageUrl} alt={title} />
    </div>
    <div class="description-box">
        <p>{description}</p>
    </div>
    <div class="buy-box">
        <p class="price">{price}€</p>
        <button
            disabled={!isLoggedIn || isArtistUser}
            class:disabled={!isLoggedIn || isArtistUser}
            title={isLoggedIn ? (isArtistUser ? 'You cannot purchase your own artwork.' : '') : 'You must be logged in to purchase this item.'}
            on:click={handleBuyNow}
        >
            Order
        </button>
    </div>
</div>

{#if showPurchasePopup}
    <PurchaseArtwork
        artwork={{ title, imageUrl }}
        artist={artist}
        price={price}
        on:close={() => (showPurchasePopup = false)}
    />
{/if}

<style>
    .artwork-card {
        border: 1px solid #333333;
        padding: 12px;
        margin: 12px;
        text-align: center;
        width: 260px;
        display: flex;
        flex-direction: column;
        align-items: center;
        border-radius: 10px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        background-color: #fff;
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }

    .artwork-card:hover {
        transform: scale(1.05);
        box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
    }

    .artwork-card h2 {
        font-size: 1.5rem;
        margin-bottom: 0rem;
        color: #333;
    }

    .artwork-card .artist {
        font-size: 1.2rem;
        margin-top: 0.5rem;
        color: #ff7b00;
        cursor: pointer;
        transition: color 0.3s ease;
    }

    .artwork-card .artist:hover {
        color: #333333;
        text-decoration: none;
    }

    .image-container {
        width: 100%;
        height: 160px;
        display: flex;
        justify-content: center;
        align-items: center;
        margin-bottom: 10px;
    }

    .artwork-card img {
        max-width: 100%;
        max-height: 100%;
        object-fit: cover;
        border: 2px solid #ff7b00;
        border-radius: 10px;
    }

    .artwork-card .description-box {
        border: 1px solid #ff7b00;
        padding: 8px;
        margin-bottom: 12px;
        text-align: left;
        background-color: #f9f9f9;
        width: 100%;
        border-radius: 10px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .artwork-card .description-box p {
        margin-top: 0;
        font-size: 0.9rem;
        color: #555;
    }

    .artwork-card .buy-box {
        border: 1px solid #ff7b00;
        padding: 8px;
        background-color: #f9f9f9;
        width: 100%;
        border-radius: 10px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }

    .artwork-card .buy-box .price {
        font-size: 1.2rem;
        font-weight: bold;
        color: #333;
        margin-bottom: 0.5rem;
    }

    .artwork-card .buy-box button {
        background-color: #ff7b00;
        color: #fff;
        border: none;
        padding: 8px 16px;
        cursor: pointer;
        border-radius: 8px;
        font-size: 0.9rem;
        transition: background-color 0.3s ease, transform 0.3s ease;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }

    .artwork-card .buy-box button:hover {
        background-color: #333333;
        transform: scale(1.05);
    }

    .artwork-card .buy-box button.disabled {
        background-color: #ccc;
        color: #666;
        cursor: not-allowed;
    }
</style>
