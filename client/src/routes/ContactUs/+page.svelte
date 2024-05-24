<script>
    let name = '';
    let email = '';
    let subject = '';
    let message = '';
    let showSuccessMessage = false;

    function handleSubmit() {
        console.log('Name:', name);
        console.log('Email:', email);
        console.log('Subject:', subject);
        console.log('Message:', message);

        name = '';
        email = '';
        subject = '';
        message = '';

        showSuccessMessage = true;
    }
</script>

<svelte:head>
	<title>Contact</title>
	<meta name="description" content="Contact" />
</svelte:head>

<h1>Contact Us</h1>

<div class="contact-container">
    <div class="info-text">
        <h2>Contact info</h2>
        <p><i class="fas fa-envelope"></i> contact@artrevealstalent.com</p>
        <p><i class="fas fa-phone"></i> 123-456-7890</p>
        <p><i class="fas fa-map-marker-alt"></i> 123 Art Street, Artville</p>
    </div>

    <form on:submit|preventDefault={handleSubmit}>
        {#if showSuccessMessage}
            <div class="success-message">
                Message sent.
            </div>
        {/if}

        <div class="form-group">
            <label for="name">Name</label>
            <input type="text" id="name" bind:value={name} required />
        </div>

        <div class="form-group">
            <label for="email">Email</label>
            <input type="email" id="email" bind:value={email} required />
        </div>

        <div class="form-group">
            <label for="subject">Subject</label>
            <input type="text" id="subject" bind:value={subject} required />
        </div>

        <div class="form-group">
            <label for="message">Message</label>
            <textarea id="message" bind:value={message} required></textarea>
        </div>

        <button type="submit" class="btn">Send Message</button>
    </form>
</div>

<style>
    h1 {
        text-align: center;
        margin-bottom: 40px;
        color: #333;
    }

    .contact-container {
        display: flex;
        justify-content: center;
        align-items: flex-start;
        max-width: 1200px;
        margin: 0 auto;
        padding: 0 10px;
    }

    .info-text {
        flex: 1.5; 
        margin-right: 40px;
        font-size: 16px;
        color: #555;
        background-color: white;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    .info-text h2 {
        margin-top: 0;
        color: #333;
    }

    .info-text p {
        margin-bottom: 10px;
    }

    .info-text i {
        color: #e3a13d;
        margin-right: 10px;
    }

    form {
        flex: 1; 
        width: 500px;
		max-width: 500px;
        background-color: #fff;
        padding: 30px;
        border-radius: 10px;
        box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
    }

    .form-group {
        margin-bottom: 20px;
    }

    label {
        display: block;
        margin-bottom: 5px;
        font-weight: bold;
        color: #333;
    }

    input,
    textarea {
        width: 100%;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 16px;
        color: #555;
    }

    textarea {
        height: 150px;
    }

    .btn {
        padding: 10px 20px;
        background-color: #e3a13d;
        color: #fff;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        transition: background-color 0.3s;
    }

    .btn:hover {
        background-color: #333333;
    }

	.success-message {
        background-color: #d4edda;
        color: #155724;
        padding: 10px;
        border-radius: 5px;
        margin-bottom: 20px;
    }

    @media (max-width: 768px) {
        .contact-container {
            flex-direction: column;
            align-items: center;
        }

        .info-text {
            margin-right: 0;
            margin-bottom: 30px;
        }

        form {
            margin-left: 20px;
            max-width: 500px;
            width: calc(100% - 40px);
        }
    }
</style>
