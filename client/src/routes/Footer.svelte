<script>
	import { page } from '$app/stores';
	import '@fortawesome/fontawesome-free/css/all.css';
</script>

<div class="footer">
	<div class="footer-content">
		<div class="social-icons">
			<a href="https://www.facebook.com"><i class="fab fa-facebook"></i></a>
			<a href="https://twitter.com"><i class="fab fa-twitter"></i></a>
			<a href="https://www.instagram.com"><i class="fab fa-instagram"></i></a>
		</div>
		<ul class="footer-nav">
			<li aria-current={$page.url.pathname.startsWith('/ContactUs') ? 'page' : undefined}>
				<a href="/ContactUs">Contact</a>
			</li>
			<li aria-current={$page.url.pathname.startsWith('/ContactUs') ? 'page' : undefined}>
				<a href="/AboutUs">About Us</a>
			</li>
			<li aria-current={$page.url.pathname.startsWith('/Users') ? 'page' : undefined}>
				<a href="/Users">Show Users</a>
			</li>
		</ul>
	</div>
</div>

<style>
	/* Add your footer styles here */
	.footer {
		background-color: #333;
		color: white;
		padding: 1rem;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.footer-content {
		display: flex;
		gap: 1rem;
	}

	.social-icons a {
		color: white;
		text-decoration: none;
		font-size: 1.5rem;
		transition: color 0.3s;
	}

	.social-icons a:hover {
		color: #ff7b00;
	}

	.footer-nav {
		list-style-type: none;
		padding: 0;
		margin: 0;
		display: flex;
		gap: 1rem;
	}

	.footer-nav li {
		font-size: 1rem;
	}

	.footer-nav li a {
		color: white;
		text-decoration: none;
		transition: color 0.3s;
	}

	.footer-nav li a:hover {
		color: #ff7b00;
	}
</style>
