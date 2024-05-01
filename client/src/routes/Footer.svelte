<script>
	import { page } from '$app/stores';

	let dropdownOpen = false;

	function toggleDropdown() {
		dropdownOpen = !dropdownOpen;
	}
</script>

<div class="blank-space"></div>
<footer class="footer">
	<a href="/"><img src="images/home.png" alt="home-icon" /></a>
	<a href="/MyProfile"><img src="images/profile.png" alt="profile-icon" /></a>
	<a href="/Register"><img src="images/schedule.png" alt="schedule-icon" /></a>
	<a href="/Schedule"><img src="images/check.png" alt="avaiability-icon" /></a>
	<div class="dropdown">
		<button class="dropbtn" on:click={toggleDropdown}>
			<img src="images/options.png" alt="options-icon" />
		</button>
		<div class="dropdown-content" style="display: {dropdownOpen ? 'block' : 'none'};">
			<ul>
				<li aria-current={$page.url.pathname === '/' ? 'page' : undefined}>
					<a href="/">Vickariepoolen</a>
				</li>
				<li aria-current={$page.url.pathname.startsWith('/Login') ? 'page' : undefined}>
					<a href="/Login">Login</a>
				</li>
				<li aria-current={$page.url.pathname.startsWith('/register-user') ? 'page' : undefined}>
					<a href="/register-user">Registrera</a>
				</li>
				<li aria-current={$page.url.pathname.startsWith('/Register') ? 'page' : undefined}>
					<a href="/Register">Time Slots</a>
				</li>
				<li aria-current={$page.url.pathname.startsWith('/Users') ? 'page' : undefined}>
					<a href="/Users">Show Users</a>
				</li>
			</ul>
		</div>
	</div>
</footer>

<style>
	.footer {
		position: fixed;
		bottom: 0;
		left: 0;
		width: 100%;
		background-color: #f0f0f0;
		display: flex;
		justify-content: space-around;
		align-items: center;
		padding: 10px 0;
	}

	img {
		width: 30px;
		height: auto;
		cursor: pointer;
	}

	img:hover {
		opacity: 0.8;
	}

	.dropdown {
		position: relative;
		display: inline-block;
	}

	.dropdown-content {
		display: none;
		position: absolute;
		background-color: #f9f9f9;
		min-width: 160px;
		box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
		z-index: 1;
		bottom: 100%;
	}

	.dropdown-content ul {
		list-style-type: none;
		padding: 0;
		margin: 0;
	}

	.dropdown-content ul li {
		padding: 8px 12px;
	}

	.dropdown-content ul li a {
		text-decoration: none;
		color: black;
	}

	.blank-space {
		height: 5em;
		visibility: hidden;
	}
</style>
