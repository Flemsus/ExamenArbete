<script>
    import { onMount, onDestroy } from 'svelte';
    import { writable } from 'svelte/store';
    import Popup from './../components/Popup.svelte';
    import Login from './../components/Login.svelte';

    const isOpen = writable(false);
    const loggedInUserName = writable(null);

    function handlePageChange(event) {
        isOpen.set(false);
    }

    function handleKeydown(event) {
        if (event.key === 'Escape') {
            isOpen.set(false);
        }
    }

    function handleOutsideClick(event) {
        const target = event.target;
        const isInputOrButton = target.tagName === 'INPUT' || target.tagName === 'BUTTON';
        if (!isInputOrButton) {
            isOpen.set(false);
        }
    }

    onMount(() => {
        window.addEventListener('popstate', handlePageChange);
        window.addEventListener('keydown', handleKeydown);
        window.addEventListener('click', handleOutsideClick);
        const loggedInUser = localStorage.getItem('loggedInUser');
        if (loggedInUser) {
            try {
                const user = JSON.parse(loggedInUser);
                loggedInUserName.set(user.name);
            } catch (error) {
                console.error('Error parsing user data:', error);
            }
        }

        return () => {
            window.removeEventListener('popstate', handlePageChange);
            window.removeEventListener('keydown', handleKeydown);
            window.removeEventListener('click', handleOutsideClick);
        };
    });

    function togglePopup() {
        isOpen.set(!$isOpen);
    }

    async function logoutUser() {
        localStorage.removeItem('loggedInUserName');
        localStorage.removeItem('loggedInUser');
        loggedInUserName.set(null);
        window.location.href = '/';
    }

    function redirectToProfile() {
        window.location.href = '/MyProfile';
    }
</script>

<div class="header">
    <div class="logo"><a href="/"><img src="images/Logo.png" alt="Donald Duck" /></a></div>
    <div class="nav">
        <a href="/">Home</a>
        <a href="/Gallery">Gallery</a>
    </div>
    {#if $loggedInUserName}
        <div class="user-dropdown">
            <span>{$loggedInUserName}</span>
            <div class="dropdown-content">
                <a href="/MyProfile" on:click={redirectToProfile}>My Profile</a>
                <a href="/" on:click={logoutUser}>Logout</a>
            </div>
        </div>
    {:else if $isOpen}
        <Popup bind:isOpen={$isOpen}>
            <div class="popup-content">
                <Login />
            </div>
        </Popup>
    {:else}
        <button class="login-btn" on:click={togglePopup}>Login</button>
    {/if}
</div>

<style>
    .header {
        background-color: #333;
        color: white;
        padding: 1rem;
        display: flex;
        justify-content: space-between;
        align-items: center;
        position: sticky;
        top: 0;
        z-index: 1000;
        width: 53.3%;
		align-self: center;
    }

    .logo {
        font-size: 1.5rem;
        font-weight: bold;
    }

    .logo img {
        height: 5rem; 
    }

    .nav {
        display: flex;
        gap: 1rem;
    }

    .nav a {
        color: white;
        text-decoration: none;
        padding: 0.5rem 1rem;
        border-radius: 5px;
        transition: background-color 0.3s;
    }

    .nav a:hover {
        background-color: #555;
    }

    .login-btn {
        background-color: #ff7b00;
        border: none;
        color: white;
        padding: 0.5rem 1rem;
        border-radius: 5px;
        cursor: pointer;
        transition: background-color 0.3s;
        height: 2.5rem;
    }

    .login-btn:hover {
        background-color: #ff9500;
    }

    .user-dropdown {
        position: relative;
        display: inline-block;
    }

    .user-dropdown span {
        cursor: pointer;
    }

    .dropdown-content {
        display: none;
        position: absolute;
        background-color: #f9f9f9;
        min-width: 160px;
        box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
        z-index: 1;
    }

    .user-dropdown:hover .dropdown-content {
        display: block;
    }

    .dropdown-content a {
        color: black;
        padding: 12px 16px;
        text-decoration: none;
        display: block;
    }

    .dropdown-content a:hover {
        background-color: #f1f1f1;
    }
</style>
