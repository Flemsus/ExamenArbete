<script>
    import { createEventDispatcher } from 'svelte';
    import { writable } from 'svelte/store';
    import { onMount } from 'svelte';

    export let artwork;
    export let artist;
    export let price;
    
    const loggedInUserName = writable(null);
    const loggedInUserId = writable(null);

    let cardNumber = '';
    let agreeToPayment = false;
    let showSuccessMessage = false;
    let isCardValid = false;
    let canBuyOwnArtwork = true;
    let showInvalidCardMessage = false; 

    const dispatch = createEventDispatcher();

    onMount(() => {
        const loggedInUser = localStorage.getItem('loggedInUser');
        if (loggedInUser) {
            try {
                const user = JSON.parse(loggedInUser);
                loggedInUserName.set(user.name);
                loggedInUserId.set(user.id);
                canBuyOwnArtwork = user.id !== artwork.artCreatorId;
            } catch (error) {
                console.error('Error parsing user data:', error);
            }
        }
    });

    function handleBuyNow() {
        if (agreeToPayment) {
            if (!isCardValid) {
                showInvalidCardMessage = true; 
                return;
            }
            if (!canBuyOwnArtwork) {
                alert("You can't buy your own item.");
                return;
            }
            showSuccessMessage = true;
            setTimeout(() => {
                showSuccessMessage = false;
                dispatch('close');
            }, 2000);
        }
    }

    function handleCancel() {
        dispatch('close');
    }

    function validateCardNumber(value) {
        isCardValid = /^\d{12}$/.test(value);
    }
</script>

<div class="modal-overlay">
    <div class="modal-content">
        <h2>{$loggedInUserName} wants to buy "{artwork.title}" sold by {artist} for {price}€</h2>
        <div class="artwork-preview">
            <img src={artwork.imageUrl} alt={artwork.title} />
        </div>
        <div class="payment-form">
            <input type="text" placeholder="Card Number" bind:value={cardNumber} on:input={e => validateCardNumber(e.target.value)} maxlength="12" />
            <div class="agree-checkbox">
                <input type="checkbox" bind:checked={agreeToPayment} id="agree-checkbox" />
                <label for="agree-checkbox">I am sure.</label>
            </div>
            <div class="button-group">
                <button class="buy-button" disabled={!agreeToPayment} on:click={handleBuyNow}>Purchase</button>
                <button class="cancel-button" on:click={handleCancel}>I'll think about it</button>
            </div>
        </div>
    </div>
</div>

{#if showSuccessMessage}
    <div class="success-message">
        <p>Thanks for your purchase!</p>
    </div>
{/if}

{#if showInvalidCardMessage} 
    <div class="custom-popup">
        <div class="popup-content">
            <p>Please enter a valid 12-digit card number.</p>
            <button class="close-button" on:click={() => (showInvalidCardMessage = false)}>Close</button>
        </div>
    </div>
{/if}

<style>
    .modal-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: flex;
        justify-content: center;
        align-items: center;
        z-index: 1000;
    }

    .modal-content {
        background-color: #ffffff;
        padding: 2rem;
        border-radius: 0.5rem;
        box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        max-width: 600px;
        width: 90%;
    }

    .modal-content h2 {
        text-align: center;
        font-size: 1.3rem;
        margin-bottom: 1.5rem;
    }

    .artwork-preview {
        display: flex;
        justify-content: center;
        margin-bottom: 2rem;
    }

    .artwork-preview img {
        max-width: 100%;
        max-height: 300px;
        border-radius: 0.5rem;
        box-shadow: 0 0.25rem 0.5rem rgba(0, 0, 0, 0.15);
    }

    .payment-form {
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .payment-form input,
    .payment-form button {
        margin-bottom: 1rem;
        padding: 0.5rem 1rem;
        border: 1px solid #ced4da;
        border-radius: 0.25rem;
        width: 100%;
        box-sizing: border-box;
        font-size: 1rem;
    }

    .agree-checkbox {
    display: flex;
    align-self: center;
    margin-bottom: 1rem;
}

.agree-checkbox input {
    flex: 1; 
    align-self: center;
}

.agree-checkbox label {
    flex: 3; 
    font-size: 14px;
    width: 6em;
    margin-top: 0.1em;
}


    .button-group {
        display: flex;
        justify-content: space-between;
        width: 100%;
        margin-top: 0;
    }

    .buy-button,
    .cancel-button {
        flex: 1;
        font-weight: 500;
        transition: background-color 0.3s, color 0.3s;
    }

    .buy-button {
        background-color: #ff7b00;
        color: #ffffff;
        cursor: pointer;
    }

    .cancel-button {
        background-color: #dc3545;
        color: #ffffff;
        cursor: pointer;
    }

    .cancel-button:hover {
        background-color: #bd2130;
        
    }

    .buy-button:disabled {
        background-color: #cccccc;
        color: #666666;
        cursor: not-allowed;
    }

    .buy-button:hover:enabled {
        background-color: #333333;
    }

    

    .success-message {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #ffffff;
        padding: 2rem;
        border-radius: 0.5rem;
        box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        z-index: 1000;
        text-align: center;
        font-size: 1.2rem;
        font-weight: 500;
    }

    .custom-popup {
        position: fixed;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        background-color: #ffffff;
        padding: 2rem;
        border-radius: 0.5rem;
        box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
        z-index: 1000;
        text-align: center;
        font-size: 1.2rem;
        font-weight: 500;
    }

    .popup-content {
        margin-bottom: 1rem;
    }

    .close-button {
        padding: 0.5rem 1rem;
        background-color: #ff7b00;
        color: #ffffff;
        border: none;
        border-radius: 0.25rem;
        cursor: pointer;
        font-size: 1rem;
        transition: background-color 0.3s ease, transform 0.3s ease;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    }

    .close-button:hover {
        background-color: #333333;
        transform: scale(1.05);
    }
</style>