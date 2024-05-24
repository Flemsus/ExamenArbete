<script>
</script>
<svelte:head>
	<title>About</title>
	<meta name="description" content="About" />
</svelte:head>
<h1>About Us</h1>

<section>
	<h2>Our Mission</h2>
	<p>
		Art Reveals Talent is dedicated to providing a dynamic platform where artists can unleash their
		creativity, showcase their masterpieces, and connect with art enthusiasts worldwide. We believe
		in the transformative power of art to inspire, uplift, and foster cultural exchange.
	</p>
</section>

<section>
	<h2>Our History</h2>
	<p>
		Founded in 2024 by a visionary duo, Donald Duck and Goofy Goof, Art Reveals Talent emerged from
		a shared passion for art and a desire to revolutionize the way art is bought and sold online.
		What began as a modest endeavor has since flourished into a vibrant community of artists,
		collectors, and aficionados, united by their love for creativity.
	</p>
</section>

<section>
	<h2>Core Values</h2>
	<ul>
		<li>
			<strong>Creativity:</strong><br> We celebrate the diversity of artistic expression and foster an environment
			where creativity knows no bounds.
		</li>
		<li>
			<strong>Innovation:</strong><br> We embrace innovation and continuously strive to push the boundaries
			of technology to enhance the art experience for our community.
		</li>
		<li>
			<strong>Community:</strong><br> We believe in the power of community and seek to cultivate meaningful
			connections between artists and art lovers worldwide.
		</li>
		<li>
			<strong>Quality:</strong><br> We uphold the highest standards of quality in everything we do, from
			the artworks showcased on our platform to the user experience we provide.
		</li>
	</ul>
</section>

<section>
	<h2>Testimonials</h2>
	<blockquote>
		"Art Reveals Talent has been instrumental in helping me reach a wider audience for my artwork.
		The platform is user-friendly, and the support team is always available to assist."
		<footer>- Picasso, Random dude found on the street</footer>
	</blockquote>
</section>

<section>
	<h4>Founders</h4>
	<div class="team-member">
		<img src="images/Dolan.png" alt="Donald Duck" />
		<h3>Donald Duck</h3>
		<p>Co-Founder & CEO</p>
	</div>
	<div class="team-member">
		<img src="images/Gooby.png" alt="Goofy Goof" />
		<h3>Goofy Goof</h3>
		<p>Co-Founder & CTO</p>
	</div>
</section>

<style>
	h1 {
		text-align: left;
		margin-bottom: 20px;
	}

	section {
		margin-bottom: 0px;
	}

	h2 {
		font-size: 24px;
		margin-bottom: 10px;
		text-align: left;
	}

	p {
		line-height: 1.6;
		margin-bottom: 20px;
	}

	h4 {
		text-align: center;
		font-size: 30px;
		margin-bottom: 10px;
	}

	ul {
		list-style-type: none;
		padding-left: 0;
		text-align: left;
	}

	blockquote {
		font-style: italic;
		border-left: 5px solid orange;
		padding-left: 15px;
		margin-bottom: 10px;
	}

	blockquote footer {
		font-weight: bold;
		margin-top: 10px;
	}

	.team-member {
		text-align: center;
		margin-bottom: 10px;
	}

	.team-member img {
		width: 150px;
		height: 150px;
		border-radius: 50%;
	}

	.team-member h3 {
		margin-bottom: 5px;
	}

	.team-member p {
		font-style: italic;
	}

	li {
		margin-bottom: 1em;
	}
</style>
