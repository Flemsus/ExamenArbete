<script>
	import { onMount } from 'svelte';
	import moment from 'moment';
	import { PUBLIC_API_URL } from '$env/static/public';
	import '@fortawesome/fontawesome-free/css/all.min.css';

	let user = {};
	let loggedInUser = localStorage.getItem('loggedInUser');

	onMount(async () => {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/user/${loggedInUser}`);
			if (response.ok) {
				user = await response.json();
				user.timeSlots.sort((a, b) => {
					return moment(a.startTime).valueOf() - moment(b.startTime).valueOf();
				});
			} else {
				console.error('Failed to fetch user data:', response.statusText);
			}
		} catch (error) {
			console.error('Error fetching user data:', error);
		}
	});

	function formatDateTime(dateTime) {
		return moment(dateTime).format('YYYY-MM-DD  HH:mm');
	}
</script>

<div class="profile-container">
	<div class="profile-item">
		<div class="profile-picture">
			<div class="placeholder-image"></div>
		</div>
	</div>
	<div class="profile-item">
		<h1>{user.name}</h1>
	</div>
	<div class="profile-item">
		<p><i class="fas fa-envelope"></i> {user.email}</p>
	</div>
	<div class="profile-item">
		<p><i class="fas fa-phone"></i> {user.phoneNumber}</p>
	</div>
	{#if user.timeSlots && user.timeSlots.length > 0}
		<div class="profile-item">
			<h2>Time Slots</h2>
			<ul>
				{#each user.timeSlots as timeSlot}
					<li>
						Time Slot {formatDateTime(timeSlot.startTime)} - {moment(timeSlot.endTime).format(
							'HH:mm'
						)}
					</li>
				{/each}
			</ul>
		</div>
	{:else}
		<div class="profile-item">
			<p>No time slots available</p>
		</div>
	{/if}
</div>

<style>
	.profile-container {
		background-color: #f2d09e;
		padding: 10px;
		border: solid black 5px;
		margin: auto;
		margin-top: 1em;
		border-radius: 10px;
		display: flex;
		flex-direction: column;
		align-items: center;
		width: 20em;
	}

	.profile-item {
		margin-bottom: 10px;
	}

	.profile-picture {
		width: 120px;
		height: 120px;
		border-radius: 50%;
		overflow: hidden;
		background-color: white;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.placeholder-image {
		width: 100px;
		height: 100px;
		background-color: #fff;
		border-radius: 50%;
	}

	h2 {
		text-align: center;
	}

	.profile-item h1 {
		margin: 5px 0;
	}

	.profile-item p {
		margin: 2px 0;
	}

	.profile-item ul {
		padding-left: 20px;
		margin-top: 0;
	}

	.profile-item li {
		margin-bottom: 5px;
	}
</style>
