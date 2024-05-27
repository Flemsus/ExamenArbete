<script>
    import { onMount } from 'svelte';
    import { PUBLIC_API_URL } from '$env/static/public';
    import { page } from '$app/stores';
    import ArtworkCard from './../../../components/ArtworkCard.svelte';

    let user = null;
    let userId = null;
    let artworks = [];
    let loading = true;
    let error = null;
    let loggedInUser = null;

    onMount(async () => {
        userId = $page.params.userId;
        loggedInUser = JSON.parse(localStorage.getItem('loggedInUser'));

        if (loggedInUser && loggedInUser.id == userId) {
            window.location.href = '/MyProfile';
            return;
        }

        try {
            const response = await fetch(`${PUBLIC_API_URL}/user/${userId}`);
            if (response.ok) {
                user = await response.json();
                artworks = user.userArt;
                loading = false;
            } else {
                throw new Error('Failed to fetch user');
            }
        } catch (err) {
            error = err.message;
            loading = false;
        }
    });
</script>

<svelte:head>
    <title>{user ? `${user.name}'s Profile` : 'Profile'}</title>
    <meta name="description" content="User profile page displaying user details and their artworks." />
</svelte:head>

{#if loading}
    <div class="loading-message">
        <p>Loading...</p>
    </div>
{:else if error}
    <div class="error-message">
        <p>{error}</p>
    </div>
{:else}
    <div class="profile-container">
        <div class="user-info">
            <div class="user-details">
                <h1>{user.name}</h1>
                <p>{user.email}</p>
            </div>
        </div>

        <div class="artworks-header">
            <h2>Artworks</h2>
        </div>

        <div class="artworks-container">
            {#each artworks as artwork}
                <ArtworkCard
                    imageUrl={artwork.imageUrl}
                    title={artwork.artName}
                    artist={user.name}
                    description={artwork.artDescription}
                    price={artwork.artPrice}
                    userId={userId}
                />
            {/each}
        </div>
    </div>
{/if}

<style>
    .profile-container {
        width: 90%;
        max-width: 1200px;
        margin: 0 auto;
        font-family: 'Roboto', sans-serif;
        color: #333;
        padding: 2rem;
    }

    .user-info {
        display: flex;
        align-items: center;
        margin-bottom: 2rem;
        background-color: #f8f8f8;
        border-radius: 10px;
        padding: 1rem;
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }

    .user-details h1 {
        font-size: 2rem;
        margin: 0;
        color: #444;
    }

    .user-details p {
        font-size: 1rem;
        color: #666;
    }

    .artworks-header {
        text-align: center;
        margin-bottom: 1.5rem;
    }

    .artworks-header h2 {
        font-size: 2rem;
        color: #444;
        text-transform: uppercase;
        border-bottom: 2px solid black;
        display: inline-block;
        padding-bottom: 0.5rem;
    }

    .artworks-container {
        display: flex;
        flex-wrap: wrap;
        gap: 1.5rem;
        justify-content: center;
    }

    .loading-message, .error-message {
        text-align: center;
        font-size: 1.5rem;
        margin: 2rem 0;
    }

    .loading-message {
        color: #ff7b00;
    }

    .error-message {
        color: red;
    }

    @media (max-width: 768px) {
        .profile-container {
            padding: 1rem;
        }

        .user-info {
            flex-direction: column;
            align-items: center;
            text-align: center;
        }

        .artworks-header h2 {
            font-size: 1.5rem;
        }

        .artworks-container {
            flex-direction: column;
        }
    }
</style>
