<script>
    import { onMount } from 'svelte';
    import { PUBLIC_API_URL } from '$env/static/public';

    let users = [];
    let emailToCheck = '';
    let emailExists = null;
    let popupVisible = false;

    onMount(async () => {
        try {
            const response = await fetch(`${PUBLIC_API_URL}/user/all`);
            if (response.ok) {
                users = await response.json();
            } else {
                console.error('Failed to fetch users:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    });

    function checkEmailExists() {
        emailExists = users.some(user => user.email === emailToCheck);
        popupVisible = true;
        setTimeout(() => {
            popupVisible = false;
            emailToCheck = '';
        }, 3000);
    }

    function handleKeyPress(event) {
        if (event.key === 'Enter') {
            checkEmailExists();
        }
    }
</script>

<div class="container">
    <div>
        <label for="emailInput">Email</label>
        <input 
            type="email" 
            id="emailInput" 
            bind:value={emailToCheck} 
            on:keypress={handleKeyPress} 
        />
        <button 
            on:click={checkEmailExists} 
            on:keypress={event => event.key === 'Enter' && checkEmailExists()} 
        >
            Recover password
        </button>
        {#if popupVisible}
            <div class="popup">
                {#if emailExists !== null}
                    {#if emailExists}
                        <p>Further instructions have been sent to your email.</p>
                    {:else}
                        <p>This email is not registered.</p>
                    {/if}
                {/if}
            </div>
        {/if}
    </div>
</div>


<style>
    .container {
        max-width: 400px;
        margin: auto;
        padding: 1rem;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }

    label {
        display: block;
        margin-bottom: 5px;
        font-weight: bold;
    }

    input[type="email"] {
        width: calc(100% - 31px); 
        padding: 8px;
        margin-bottom: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
    }

    button {
        padding: 8px 15px;
        background-color: #ff7b00;
        color: #fff;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        transition: background-color 0.3s;
    }

    button:hover {
        background-color: #333333;
    }

    .popup {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #fff;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 5px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        z-index: 9999;
		width: 80%; 
    max-width: 400px; 
    }

    .popup p {
        margin: 0;
    }
</style>
