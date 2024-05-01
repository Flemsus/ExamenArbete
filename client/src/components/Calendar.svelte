<script>
	import { onMount, createEventDispatcher } from 'svelte';
	import moment from 'moment';
	import '@fortawesome/fontawesome-free/css/all.min.css';

	const dispatch = createEventDispatcher();

	export let selectedDays = [];

	let selectedDate;
	let calendar = [];

	onMount(() => {
		selectedDate = moment().format('YYYY-MM-DD');
		generateCalendar();
	});

	function generateCalendar() {
		const currentMonth = moment(selectedDate).startOf('month');
		const daysInMonth = moment(selectedDate).daysInMonth();
		const firstDayOfMonth = moment(selectedDate).startOf('month').day();

		const prevMonthDays = firstDayOfMonth === 0 ? 6 : firstDayOfMonth - 1;
		const lastMonthDate = moment(currentMonth).subtract(1, 'month').endOf('month');
		let prevMonthDates = Array.from({ length: prevMonthDays }, (_, i) =>
			moment(lastMonthDate).subtract(i, 'days')
		).map((date) => ({
			date,
			marked: false,
			outOfMonth: true
		}));

		prevMonthDates.reverse();

		const currentMonthDates = Array.from({ length: daysInMonth }, (_, i) =>
			moment(currentMonth).date(i + 1)
		).map((date) => ({
			date,
			marked: false,
			outOfMonth: false
		}));

		const totalDays = prevMonthDays + daysInMonth;
		const remainingDays = 42 - totalDays;
		const nextMonth = moment(currentMonth).add(1, 'month').startOf('month');
		const nextMonthDates = Array.from({ length: remainingDays }, (_, i) =>
			moment(nextMonth).date(i + 1)
		).map((date) => ({
			date,
			marked: false,
			outOfMonth: true
		}));

		calendar = [...prevMonthDates, ...currentMonthDates, ...nextMonthDates];
	}

	function previousMonth() {
		selectedDate = moment(selectedDate).subtract(1, 'month').format('YYYY-MM-DD');
		generateCalendar();
	}

	function nextMonth() {
		selectedDate = moment(selectedDate).add(1, 'month').format('YYYY-MM-DD');
		generateCalendar();
	}

	function handleDayClick(date) {
		const index = selectedDays.findIndex((day) => day.isSame(date, 'day'));
		if (index !== -1) {
			selectedDays.splice(index, 1);
		} else {
			selectedDays.push(date); // Add the day to selectedDays array
		}
		handleSelectedDaysChange(selectedDays);
	}

	function handleSelectedDaysChange(newSelectedDays) {
		dispatch('selectedDaysChange', { selectedDays: newSelectedDays });
	}

	function showSelectedDays() {
		const selectedDaysString = selectedDays.map((day) => day.format('YYYY-MM-DD')).join(', ');
		alert(`Selected Days: ${selectedDaysString}`);
	}
</script>

<button on:click={showSelectedDays}>Show Selected Days</button>

<div class="calendar" role="grid">
	<div class="controls">
		<button on:click={previousMonth} class="arrow-button"
			><i class="fa-solid fa-chevron-left"></i></button
		>
		<h3>{moment(selectedDate).format('MMMM YYYY')}</h3>
		<button on:click={nextMonth} class="arrow-button"
			><i class="fa-solid fa-chevron-right"></i></button
		>
	</div>
	<div class="weekdays">
		<ul>
			{#each ['Mån', 'Tis', 'Ons', 'Tors', 'Fre', 'Lör', 'Sön'] as weekday}
				<li>{weekday}</li>
			{/each}
		</ul>
	</div>
	<div class="days">
		<div class="calendar-days">
			{#each calendar as { date, marked, outOfMonth }}
				<div role="row">
					<div role="gridcell">
						<button
							type="button"
							class="day"
							aria-pressed={marked}
							on:click={() => {
								marked = !marked;
								handleDayClick(date); // Call handleDayClick to manage selectedDays
							}}
							class:selected={marked}
							disabled={outOfMonth}
						>
							<span class="day-number">{date.format('D')}</span>
							<span class="day-of-week">{date.format('dddd')}</span>
						</button>
					</div>
				</div>
			{/each}
		</div>
	</div>
</div>

<style>
	h3 {
		font-family: 'Montserrat', sans-serif;
		font-weight: 500;
		font-size: 24px;
		margin-bottom: 10px;
		position: relative;
	}

	h3::after {
		content: '';
		position: absolute;
		bottom: -5px;
		left: 0;
		width: 100%;
		height: 1px;
		background-color: #a3a3a6;
	}
	.arrow-button {
		background-color: #f6f6f6;
		color: #b3b3b3;
		border: none;
		width: 30px;
		height: 30px;
		border-radius: 50%;
		font-size: 14px;
		box-shadow: 2px 2px 4px rgba(0, 0, 0, 0.2);

		cursor: pointer;
	}

	.arrow-button:hover {
		background-color: rgba(227, 161, 61, 0.5);
	}

	.calendar {
		margin-top: 20px;
		padding: 1em;
		background-color: #ffffff;
	}

	.controls {
		display: flex;
		justify-content: space-between;
		align-items: center;
	}

	.day {
		display: flex;
		flex-direction: column;
		align-items: center;
		padding: 5px;
		margin: 2px;
		cursor: pointer;
		border: none;
		background-color: transparent;
	}

	.day.selected {
		background-color: #e3a13d;
		border-radius: 8px;
	}

	.day-number {
		font-size: 20px;
		font-family: 'Roboto', sans-serif;
		font-weight: 500;
	}

	.day-of-week {
		font-size: 10px;
	}

	ul {
		list-style-type: none;
		padding: 0;
		margin: 0;
		display: flex;
		justify-content: space-between;
	}

	.weekdays {
		font-family: 'Roboto', sans-serif;
		color: #b3b3b3;
		font-size: 20px;
		margin-top: 20px;

		padding-left: 1em;
		padding-right: 1em;
	}

	.calendar-days {
		display: grid;
		grid-template-columns: repeat(7, 1fr);
		gap: 5px;
	}

	.calendar-days div {
		display: flex;
		justify-content: center;
		align-items: center;
	}

	.calendar-days div[role='row'] {
		display: flex;
		justify-content: center;
	}

	.calendar-days div[role='gridcell'] {
		flex: 1;
		text-align: center;
	}

	button[disabled] {
		cursor: not-allowed;
	}

	.day-of-week {
		display: none;
	}
</style>
