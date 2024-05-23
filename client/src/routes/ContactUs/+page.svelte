<script>
	let name = '';
	let email = '';
	let subject = '';
	let message = '';

	function handleSubmit() {
		// Add your form submission logic here
		console.log('Name:', name);
		console.log('Email:', email);
		console.log('Subject:', subject);
		console.log('Message:', message);

		// Reset form fields after submission
		name = '';
		email = '';
		subject = '';
		message = '';
	}
</script>

<h1>Contact Us</h1>

<form on:submit|preventDefault={handleSubmit}>
	<label for="name">Name:</label>
	<input type="text" id="name" bind:value={name} required />

	<label for="email">Email:</label>
	<input type="email" id="email" bind:value={email} required />

	<label for="subject">Subject:</label>
	<input type="text" id="subject" bind:value={subject} required />

	<label for="message">Message:</label>
	<textarea id="message" bind:value={message} required></textarea>

	<button type="submit">Send Message</button>
</form>

<footer>
	<p>Contact Information:</p>
	<p>Email: contact@artrevealstalent.com</p>
	<p>Phone: 123-456-7890</p>
	<p>Address: 123 Art Street, Artville</p>
</footer>

<style>
	h1 {
		text-align: center;
		margin-bottom: 20px;
	}

	form {
		max-width: 500px;
		margin: 0 auto;
	}

	label {
		display: block;
		margin-bottom: 10px;
		font-weight: bold;
	}

	input,
	textarea {
		width: 100%;
		padding: 10px;
		margin-bottom: 20px;
		border: 1px solid #ccc;
		border-radius: 5px;
	}

	button {
		padding: 10px 20px;
		background-color: #007bff;
		color: #fff;
		border: none;
		border-radius: 5px;
		cursor: pointer;
	}

	button:hover {
		background-color: #0056b3;
	}

	footer {
		text-align: center;
		margin-top: 50px;
		font-size: 14px;
	}
</style>
