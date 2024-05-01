<script>
	import Calendar from '../../components/Calendar.svelte';
	import TimeSelector from '../../components/TimeSelector.svelte';
	import RegUser from '../../components/RegUser.svelte';
	import { PUBLIC_API_URL } from '$env/static/public';

	let startTime = '';
	let endTime = '';
	let selectedDays = [];

	const handleDayMarked = (event) => {
		selectedDays = event.detail.markedDays;
	};

	const handleStartTimeSelected = (event) => {
		startTime = event.detail.startTime;
	};

	const handleEndTimeSelected = (event) => {
		endTime = event.detail.endTime;
	};

	const addTimeSlot = async () => {
		try {
			if (!startTime || !endTime) {
				console.error('Start time and end time must be specified.');
				return;
			}
			let days = await selectedDays();
			let timeSlots = await createTimeSlots(days);
			console.log('timeSlots:', timeSlots);
			await postTimeSlots(timeSlots);
		} catch (error) {
			console.error('Error while adding time slots:', error);
		}
	};

	const createTimeSlots = async (days) =>
		days.map((day) => {
			const startDateTime = day.date.clone().set({
				hour: parseInt(startTime.split(':')[0]),
				minute: parseInt(startTime.split(':')[1])
			});
			const endDateTime = day.date.clone().set({
				hour: parseInt(endTime.split(':')[0]),
				minute: parseInt(endTime.split(':')[1])
			});

			return {
				startTime: startDateTime.toISOString(),
				endTime: endDateTime.toISOString()
			};
		});

	// TODO: Implement postTimeSlots in backend
	const postTimeSlots = async (timeSlots) => {
		const res = await fetch(`${PUBLIC_API_URL}/time-slots`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(timeSlots)
		});

		if (!res.ok) {
			throw new Error('Post failed');
		}

		console.log(
			'Time slot added successfully for',
			day.date.format('YYYY-MM-DD'),
			startTime + '-' + endTime
		);
		return res;
	};

	const saveTime = () => {
		const selectedDaysString = selectedDays.map((day) => day.date.format('YYYY-MM-DD')).join(', ');
		alert(`Selected Days: ${selectedDaysString}, Start Time: ${startTime}, End Time: ${endTime}`);
	};

	function showSelectedDays() {
		const selectedDaysString = selectedDays.map((day) => day.date.format('YYYY-MM-DD')).join(', ');
		alert(`Selected Days: ${selectedDaysString}`);
	}
</script>

<svelte:head>
	<title>Schema</title>
	<meta name="description" content="Schema" />
</svelte:head>

<Calendar on:dayMarked={handleDayMarked} />

<button on:click={showSelectedDays}>Show Selected Days</button>
<button on:click={saveTime}>Save Time</button>

<TimeSelector
	on:startTimeSelected={handleStartTimeSelected}
	on:endTimeSelected={handleEndTimeSelected}
/>

<RegUser />

<button on:click={saveTime}>Save Time</button>
