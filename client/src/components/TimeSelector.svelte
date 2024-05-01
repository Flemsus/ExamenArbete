<script lang="ts">
	import { onMount, createEventDispatcher } from 'svelte';
	import Popup from './Popup.svelte';
	import Button from './Button.svelte';

	const dispatch = createEventDispatcher();

	let startTime: string | null = null;
	let endTime: string | null = null;

	let isOpenStartTime = false;
	let isOpenEndTime = false;

	let currentHour = 0;
	let currentMinute = 0;

	const hours: string[] = Array.from({ length: 24 }, (_, i) => i.toString().padStart(2, '0'));
	const minutes: string[] = Array.from({ length: 60 }, (_, i) => i.toString().padStart(2, '0'));

	const handleHourScroll = (event: WheelEvent | TouchEvent) => {
		if ('deltaY' in event) {
			currentHour = (currentHour + (event.deltaY > 0 ? -1 : 1) + 24) % 24;
		} else {
			const deltaY = (event as TouchEvent).touches[0].clientY - initialTouchY;
			const hoursToScroll = Math.floor(deltaY / HOUR_SCROLL_SENSITIVITY);
			currentHour = (currentHour + hoursToScroll + 24) % 24;
		}
	};

	const handleMinuteScroll = (event: WheelEvent | TouchEvent) => {
		if ('deltaY' in event) {
			currentMinute = (currentMinute + (event.deltaY > 0 ? -1 : 1) + 60) % 60;
		} else {
			const deltaY = (event as TouchEvent).touches[0].clientY - initialTouchY;
			const minutesToScroll = Math.floor(deltaY / MINUTE_SCROLL_SENSITIVITY);
			currentMinute = (currentMinute + minutesToScroll + 60) % 60;
		}
	};

	const saveStartTime = () => {
		startTime = `${hours[currentHour]}:${minutes[currentMinute]}`;
		dispatch('startTimeSelected', { startTime });
	};

	const saveEndTime = () => {
		endTime = `${hours[currentHour]}:${minutes[currentMinute]}`;
		dispatch('endTimeSelected', { endTime });
	};

	const saveTime = () => {
		alert(`Start Time: ${startTime} - End Time: ${endTime}`);
	};

	$: isSaveDisabled = startTime === null || endTime === null;
	let initialTouchY = 0;
	const HOUR_SCROLL_SENSITIVITY = 10;
	const MINUTE_SCROLL_SENSITIVITY = 10;

	onMount(() => {
		const timeSelector = document.querySelector('.time-selector');
		if (timeSelector) {
			timeSelector.addEventListener('wheel', handleHourScroll as EventListener);
			timeSelector.addEventListener('wheel', handleMinuteScroll as EventListener);
			timeSelector.addEventListener('touchstart', (event: TouchEvent) => {
				initialTouchY = event.touches[0].clientY;
			});
			timeSelector.addEventListener('touchmove', handleHourScroll as EventListener);
			timeSelector.addEventListener('touchmove', handleMinuteScroll as EventListener);
		}
	});
</script>

<div class="time-buttons">
	<Button on:click={() => (isOpenStartTime = true)}>Start Time: {startTime || ''}</Button>
	<Button on:click={() => (isOpenEndTime = true)}>End Time: {endTime || ''}</Button>

	<Button on:click={saveTime} disabled={isSaveDisabled}>Save Time</Button>
</div>

<Popup bind:isOpen={isOpenStartTime}>
	<div class="popup">
		<div class="button-container">
			<button class="cancel-button" on:click={() => (isOpenStartTime = false)}>Avbryt</button>

			<button
				class="save-button"
				on:click={() => {
					saveStartTime();
					isOpenStartTime = false;
				}}>Spara</button
			>
		</div>
		<div class="text-above-selector">Starttid</div>

		<div class="time-selector">
			<div class="hour-list" on:wheel={handleHourScroll}>
				<div class="hour-item">{hours[currentHour]}</div>
			</div>
			<div class="colon-item">:</div>
			<div class="minute-list" on:wheel={handleMinuteScroll}>
				<div class="minute-item">{minutes[currentMinute]}</div>
			</div>
		</div>
	</div>
</Popup>

<Popup bind:isOpen={isOpenEndTime}>
	<div class="popup">
		<div class="button-container">
			<button class="cancel-button" on:click={() => (isOpenEndTime = false)}>Avbryt</button>

			<button
				class="save-button"
				on:click={() => {
					saveEndTime();
					isOpenEndTime = false;
				}}>Spara</button
			>
		</div>
		<div class="text-above-selector">Slutttid</div>

		<div class="time-selector">
			<div class="hour-list" on:wheel={handleHourScroll}>
				<div class="hour-item">{hours[currentHour]}</div>
			</div>
			<div class="colon-item">:</div>
			<div class="minute-list" on:wheel={handleMinuteScroll}>
				<div class="minute-item">{minutes[currentMinute]}</div>
			</div>
		</div>
	</div>
</Popup>

<style>
	.popup {
		position: fixed;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		background-color: white;
		z-index: 999;
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
	}

	.popup button {
		color: #e3a13d;
		font-family: 'Roboto', sans-serif;
		font-size: 20px;
		font-weight: 600;
		padding: 10px 20px;
		border: none;
		border-radius: 5px;
		cursor: pointer;
	}

	.button-container {
		margin-top: 20px;
		margin-bottom: auto;
		display: flex;
		justify-content: space-between;
	}

	.save-button {
		margin-left: 100px;
	}

	.cancel-button {
		margin-right: 100px;
		color: black;
	}

	.text-above-selector {
		font-family: 'Roboto', sans-serif;
		font-size: 25px;
		margin-bottom: 10px;
	}

	.time-selector {
		display: flex;
		overflow-x: auto;
		-webkit-overflow-scrolling: touch;
		background-color: white;
		margin-bottom: 500px;
	}
	.hour-list,
	.minute-list {
		flex: 1;
		display: flex;
		flex-direction: column;
	}

	.hour-item,
	.minute-item {
		padding: 7px;
		font-size: 30px;
		border-bottom: 1px solid #ccc;
		text-align: center;
		cursor: pointer;
	}
	.colon-item {
		padding: 8px;
		color: black;
		font-size: 30px;
		background-color: white;
	}
	.hour-item:last-child,
	.minute-item:last-child {
		border-bottom: none;
	}

	.minute-list {
		background-color: #d9d9d9;
	}

	.hour-list {
		background-color: #f1d09e;
	}

	.time-buttons {
		margin-top: 1em;
		margin-left: auto;
		margin-right: auto;
	}

	.save-button,
	.cancel-button {
		background-color: transparent;
		border: none;
		color: black;
	}
</style>
