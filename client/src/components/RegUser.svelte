<script>
    import { onMount } from 'svelte';
    import moment from 'moment';
    import { PUBLIC_API_URL } from '$env/static/public';

    let users = [];
    let selectedUserId = null; 
    let selectedDate;
    let calendar = [];
    let startTime = '';
    let endTime = '';

    onMount(async () => {
        try {
            const response = await fetch(`${PUBLIC_API_URL}/user/all`);
            if (response.ok) {
                users = await response.json();
            } else {
                console.error('Failed to fetch users:', response.statusText);
            }
        } catch (error) {
            console.error('Error fetching users:', error);
        }
        selectedDate = moment().format('YYYY-MM-DD');
        generateCalendar();
    });

    function generateCalendar() {
    const currentMonth = moment(selectedDate).startOf('month');
    const daysInMonth = moment(selectedDate).daysInMonth();
    const firstDayOfMonth = moment(selectedDate).startOf('month').day(); 

    calendar = [];

    const prevMonthDays = (firstDayOfMonth === 0) ? 6 : firstDayOfMonth - 1;
    const lastMonthDate = moment(currentMonth).subtract(1, 'month').endOf('month');
    for (let i = prevMonthDays; i > 0; i--) {
        const prevMonthDate = moment(lastMonthDate).subtract(i - 1, 'days');
        calendar.push({
            date: prevMonthDate,
            marked: false,
            outOfMonth: true
        });
    }

    for (let i = 1; i <= daysInMonth; i++) {
        const date = moment(currentMonth).date(i);
        calendar.push({
            date,
            marked: false,
            outOfMonth: false
        });
    }

    const totalDays = prevMonthDays + daysInMonth;
    const remainingDays = 42 - totalDays; 
    const nextMonth = moment(currentMonth).add(1, 'month').startOf('month');
    for (let i = 1; i <= remainingDays; i++) {
        const nextMonthDate = moment(nextMonth).date(i);
        calendar.push({
            date: nextMonthDate,
            marked: false,
            outOfMonth: true
        });
    }
}

function previousMonth() {
        selectedDate = moment(selectedDate).subtract(1, 'month').format('YYYY-MM-DD');
        generateCalendar();
    }

    function nextMonth() {
        selectedDate = moment(selectedDate).add(1, 'month').format('YYYY-MM-DD');
        generateCalendar();
    }

    async function addTimeSlot() {
        try {
            if (!selectedUserId) { 
                console.error('Please select a user.');
                return;
            }
            if (!startTime || !endTime) {
                console.error('Start time and end time must be specified.');
                return;
            }

            for (const day of calendar) {
                if (day.marked && !day.outOfMonth) {
                    const startDateTime = day.date.clone().set({ 'hour': parseInt(startTime.split(':')[0]), 'minute': parseInt(startTime.split(':')[1]) });
                    const endDateTime = day.date.clone().set({ 'hour': parseInt(endTime.split(':')[0]), 'minute': parseInt(endTime.split(':')[1]) });

                    const newTimeSlot = { 
                        startTime: startDateTime.toISOString(),
                        endTime: endDateTime.toISOString()
                    };

                    const res = await fetch(`${PUBLIC_API_URL}/time-slot/add?userId=${selectedUserId}`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(newTimeSlot),
                    });

                    if (!res.ok) {
                        throw new Error('Post failed');
                    }

                    console.log('Time slot added successfully for', day.date.format('YYYY-MM-DD'), startTime + '-' + endTime);

                }
            }
        } catch (error) {
            console.error('Error while adding time slots:', error);
        }
    }
</script>


<div class="calendar" role="grid">
    <div>
        <label for="userSelect">Select User:</label>
        <select id="userSelect" bind:value={selectedUserId}> 
            <option value={null}>Select User...</option>
            {#each users as user}
                <option value={user.id}>{user.name}</option> 
            {/each}
        </select>        
    </div>
    <div class="controls">
        <button on:click={previousMonth}>Previous Month</button>
        <h3>{moment(selectedDate).format('MMMM YYYY')}</h3>
        <button on:click={nextMonth}>Next Month</button>
    </div>
    <div class="weekdays">
        <ul>
            {#each ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'] as weekday}
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
                            on:click={() => marked = !marked}
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
        <div class="time-inputs">
            <div>
                <label for="startTime">Start Time:</label>
                <input type="time" id="startTime" bind:value={startTime} step="3600" />
            </div>
            <div>
                <label for="endTime">End Time:</label>
                <input type="time" id="endTime" bind:value={endTime} step="3600" />
            </div>
            <div>
                <button type="button" on:click={addTimeSlot}>Add Time Slot</button>
            </div>
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
