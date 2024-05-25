<script>
    import { onMount, onDestroy } from 'svelte';
    import { writable } from 'svelte/store';
    import Popup from './../components/Popup.svelte';
    import Login from './../components/Login.svelte';

    const isOpen = writable(false);
    const loggedInUser = writable(null);
    const isDropdownOpen = writable(false);

    function handlePageChange(event) {
        isOpen.set(false);
        isDropdownOpen.set(false);
    }

    function handleKeydown(event) {
        if (event.key === 'Escape') {
            isOpen.set(false);
            isDropdownOpen.set(false);
        }
    }

    function handleOutsideClick(event) {
        const target = event.target;
        const isInputOrButton = target.tagName === 'INPUT' || target.tagName === 'BUTTON';
        if (!isInputOrButton) {
            isOpen.set(false);
            isDropdownOpen.set(false);
        }
    }

    function isAdmin(user) {
        return user && user.roleId === 1;
    }

    onMount(() => {
        window.addEventListener('popstate', handlePageChange);
        window.addEventListener('keydown', handleKeydown);
        window.addEventListener('click', handleOutsideClick);
        const storedUser = localStorage.getItem('loggedInUser');
        if (storedUser) {
            try {
                loggedInUser.set(JSON.parse(storedUser));
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

    function toggleDropdown() {
        isDropdownOpen.update(open => !open);
    }

    async function logoutUser() {
        localStorage.removeItem('loggedInUser');
        loggedInUser.set(null);
        window.location.href = '/';
    }

    function redirectToProfile() {
        window.location.href = '/MyProfile';
    }
</script>

<div class="header">
    <div class="logo"><a href="/"><img src="/images/Logo.png" alt="Logo" /></a></div>
    <div class="nav">
        <a href="/">Home</a>
        <a href="/Gallery">Gallery</a>
        <a href="/AboutUs">About Us</a>
        <a href="/ContactUs">Contact</a>
    </div>
    {#if $loggedInUser !== null}
        <div class="user-dropdown">
            <button class="name-button" type="button" on:click={toggleDropdown} aria-haspopup="true" aria-expanded={$isDropdownOpen}>
                {$loggedInUser.name}
            </button>
            {#if $isDropdownOpen}
                <div class="dropdown-content">
                    <a href="/MyProfile" on:click={redirectToProfile}>Profile</a>
                    {#if isAdmin($loggedInUser)}
                        <a href="/Users">Admin</a>
                    {/if}
                    <a href="/" on:click={logoutUser}>Logout</a>
                </div>
            {/if}
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
        width: 100%;
        max-width: 64rem;
        margin: 0 auto;
    }

    .logo {
        font-size: 1.5rem;
        font-weight: bold;
    }

    .logo img {
        height: 3rem;
    }

    .nav {
        display: flex;
        gap: 1rem;
        flex-wrap: wrap;
    }

    .nav a,
    .name-button {
        color: white;
        text-decoration: none;
        padding: 0.5rem 1rem;
        border-radius: 5px;
        transition: background-color 0.3s;
        font-size: 1rem;
    }

    .nav a:hover,
    .name-button:hover {
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

    .user-dropdown button {
        background: none;
        border: none;
        color: white;
        font-size: 1rem;
        cursor: pointer;
        padding: 0.5rem 1rem;
        border-radius: 5px;
        transition: background-color 0.3s;
        height: 2.5rem;
    }

    .dropdown-content {
        position: absolute;
        background-color: #f9f9f9;
        max-width: 100em;
        box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
        z-index: 1;
        padding: 0.5rem 1rem;
        border-radius: 5px;
        margin-left: 0.5em;
        text-align: center;
        
    }

    .dropdown-content a {
        color: black;
        padding: 0.5rem 0;
        text-decoration: none;
        display: block;
    }

    .dropdown-content a:hover {
        background-color: #f1f1f1;
    }

    @media (max-width: 768px) {
        .header {
            flex-wrap: wrap;
            justify-content: center;
            padding: 0.5rem;
        }

        .logo,
        .nav,
        .user-dropdown,
        .login-btn {
            margin: 0.5rem;
        }

        .nav a,
        .name-button {
            font-size: 0.8rem;
            padding: 0.3rem 0.6rem;
        }

        .logo img {
            height: 2rem;
        }
    }
</style>
