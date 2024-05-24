<script>
    import { onMount } from 'svelte';
    import ProfileArtWorkCard from './../../components/ProfileArtWorkCard.svelte';
    import EditArtworkModal from './../../components/EditArtworkModal.svelte';
    import AddArtwork from './../../components/AddArtwork.svelte';
    import { PUBLIC_API_URL } from '$env/static/public';

    let user = null;
    let showEditModal = false;
    let showAddArtwork = false;
    let selectedArtwork = null;

    onMount(async () => {
        const loggedInUser = localStorage.getItem('loggedInUser');
        if (loggedInUser) {
            try {
                const parsedUser = JSON.parse(loggedInUser);
                const userId = parsedUser.id;
                const response = await fetch(`${PUBLIC_API_URL}/user/${userId}`);
                if (response.ok) {
                    user = await response.json();
                    console.log('User data:', user);
                } else {
                    console.error('Failed to fetch user data:', response.status);
                }
            } catch (error) {
                console.error('Error fetching user data:', error);
            }
        } else {
            console.warn('No user data found in localStorage.');
        }
    });

    function handleEditArtwork(event) {
        selectedArtwork = event.detail;
        showEditModal = true;
    }

    function handleSaveArtwork(event) {
        const updatedArtwork = event.detail;
        editArtwork(updatedArtwork);
    }

    async function editArtwork(artwork) {
        try {
            const response = await fetch(`${PUBLIC_API_URL}/user/updateArt`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(artwork)
            });

            if (response.ok) {
                const updatedArtworks = user.userArt.map((art) => {
                    if (art.id === artwork.id) {
                        return artwork;
                    }
                    return art;
                });
                
                user = { ...user, userArt: updatedArtworks };
            } else {
                console.error('Failed to update artwork:', response.status);
            }
        } catch (error) {
            console.error('Error updating artwork:', error);
        }
    }

    async function deleteArtwork(artwork) {
        try {
            const response = await fetch(`${PUBLIC_API_URL}/user/deleteArt/${artwork.id}`, {
                method: 'DELETE'
            });

            if (response.ok) {
                user = { ...user, userArt: user.userArt.filter((art) => art.id !== artwork.id) };
            } else {
                console.error('Failed to delete artwork:', response.status);
            }
        } catch (error) {
            console.error('Error deleting artwork:', error);
        }
    }
</script>

<svelte:head>
	<title>Profile</title>
	<meta name="description" content="Profile" />
</svelte:head>

<div class="profile-container">
    <h1>Profile</h1>

    {#if user}
        <div class="user-info">
            <p class="user-name">Name: {user.name}</p>
            <p class="user-email">Email: {user.email}</p>
            <button class="addart-btn" on:click={() => (showAddArtwork = true)}>Add Artwork</button>
            {#if showAddArtwork}
                <AddArtwork {user} onClose={() => (showAddArtwork = false)} />
            {/if}
        </div>

        <div class="artworks-header">
            <h2>Artworks</h2>
			
        </div>

        <div class="artworks-container">
            {#each user.userArt as artwork}
                <ProfileArtWorkCard
                    imageUrl={artwork.imageUrl}
                    title={artwork.artName}
                    description={artwork.artDescription}
                    price={`Current price: ${artwork.artPrice}€`}
                    {artwork}
                    on:edit={handleEditArtwork}
                    on:delete={() => deleteArtwork(artwork)}
                />
            {/each}
        </div>
    {/if}

    {#if showEditModal}
        <EditArtworkModal
            artwork={selectedArtwork}
            onClose={() => (showEditModal = false)}
            on:save={handleSaveArtwork}
        />
    {/if}
</div>
<div class="empty-space"> </div>

<style>
    .profile-container {
        width: 90%;
        max-width: 1200px;
        margin: 0 auto;
        font-family: 'Roboto', sans-serif;
        color: #333;
    }

    h1 {
        text-align: center;
        font-size: 3rem;
        margin-bottom: 1rem;
        color: #333;
    }

    .user-info {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 1rem;
        background-color: #f8f8f8;
        border-radius: 10px;
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
        margin-bottom: 2rem;
        text-align: center;
    }

    .user-name {
        font-size: 2rem;
        font-weight: 700;
        color: #444;
    }

    .user-email {
        font-size: 1.2rem;
        color: #666;
    }

    .addart-btn {
        margin-top: 1rem;
        padding: 0.75rem 1.5rem;
        background-color: orange;
        color: #fff;
        border: none;
        border-radius: 8px;
        cursor: pointer;
        font-size: 1rem;
        transition: background-color 0.3s ease, transform 0.3s ease;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }

    .addart-btn:hover {
        background-color: #333333;
        transform: scale(1.05);
    }

    .artworks-header {
        display: flex;
        justify-content: center;
        align-items: center;
        margin-bottom: 0.5rem;
    }

    h2 {
        font-size: 2.5rem;
        color: #444;
        text-transform: uppercase;
        border-bottom: 2px solid black;
        padding-bottom: 0.5rem;
    }

    .artworks-container {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
        gap: 1.5rem;
    }

	.empty-space {
		height: 1em;
	}

    @media (max-width: 768px) {
        .user-info {
            flex-direction: column;
            align-items: center;
            text-align: center;
            padding: 1.5rem;
        }

        .user-name,
        .user-email {
            margin-bottom: 0.5rem;
        }

        .artworks-header {
            flex-direction: column;
            align-items: center;
            margin-bottom: 1rem;
        }

        .addart-btn {
            margin-top: 1rem;
        }

        .artworks-container {
            flex-direction: column;
            align-items: center;
        }
    }
</style>
