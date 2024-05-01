<script>
    import moment from 'moment';
    import { PUBLIC_API_URL } from '$env/static/public';

    let selectedDays = {
        Monday: false,
        Tuesday: false,
        Wednesday: false,
        Thursday: false,
        Friday: false,
        Saturday: false,
        Sunday: false
    };

    let startTime = '';
    let endTime = '';

    function addTimeSlot(day, startTime, endTime) {
        try {
            if (!startTime || !endTime) {
                console.error('Start time and end time must be specified.');
                return;
            }

            const currentDate = moment().startOf('day');
            let selectedDateTime = moment().isoWeekday(day).startOf('day');

            if (day === 'Sunday') {
                selectedDateTime = selectedDateTime.add(1, 'week').startOf('isoWeek');
            } else {
                selectedDateTime = selectedDateTime.add(1, 'day');
            }

            const startDateTime = moment(selectedDateTime).utcOffset(0).set({ 'hour': parseInt(startTime.split(':')[0]), 'minute': parseInt(startTime.split(':')[1]), 'second': 0, 'millisecond': 0 });
            const endDateTime = moment(selectedDateTime).utcOffset(0).set({ 'hour': parseInt(endTime.split(':')[0]), 'minute': parseInt(endTime.split(':')[1]), 'second': 0, 'millisecond': 0 });

            return {
                startTime: startDateTime.toISOString(),
                endTime: endDateTime.toISOString(),
                day: day
            };
        } catch (error) {
            console.error('Error while adding time slot:', error);
        }
    }

    async function sendTimeSlots() {
        const timeSlots = [];
        const weekdays = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];

        weekdays.forEach((day) => {
            if (selectedDays[day]) {
                const timeSlot = addTimeSlot(day, startTime, endTime);
                if (timeSlot) {
                    timeSlots.push(timeSlot);
                }
            }
        });

        if (timeSlots.length > 0) {
            timeSlots.forEach(async (timeSlot) => {
                try {
                    const res = await fetch(`${PUBLIC_API_URL}/time-slot`, {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                        body: JSON.stringify(timeSlot),
                    });

                    if (!res.ok) {
                        throw new Error('Post failed');
                    }

                    console.log(`Time slot added successfully for ${timeSlot.day}.`);
                } catch (error) {
                    console.error('Error while adding time slot:', error);
                }
            });
        }
    }

    function logCheckboxStatus(day, checked) {
        const selectedDateTime = moment().isoWeekday(day).startOf('day');
        console.log(`${checked ? 'Checked' : 'Unchecked'} - ${day} - ${selectedDateTime.format('YYYY-MM-DD')}`);
    }

    const currentWeekNumber = moment().isoWeek();
</script>

<div class="multipletimeslot">
    <h2>Week {currentWeekNumber}</h2>
    <div class="checkboxes">
        {#each Object.keys(selectedDays) as day}
            <label>
                <input type="checkbox" bind:checked="{selectedDays[day]}" on:change={() => logCheckboxStatus(day, selectedDays[day])}>
                {day}
            </label>
        {/each}
    </div>
    <div class="time-inputs">
        <input type="time" bind:value="{startTime}" step="3600" />
        <input type="time" bind:value="{endTime}" step="3600" />
        <button on:click={sendTimeSlots}>Save Weekly</button>
    </div>
</div>

<style>
    h2 {
        text-decoration: underline;
        font-weight: bold;
        font-size: 20px;
    }

    .multipletimeslot {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 1em;
        text-align: center;
        border: solid 5px blue;
        background-color: white;
    }

    .checkboxes {
        display: flex;
        justify-content: center;
        margin-bottom: 10px;
    }

    .checkboxes label {
        margin-right: 10px;
    }

    .time-inputs {
        display: flex;
        align-items: center;
        margin-bottom: 5px;
    }

    .time-inputs input[type="time"] {
        margin-right: 10px;
    }

    button {
        padding: 8px 16px;
        background-color: #4CAF50;
        color: white;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

    button:hover {
        background-color: #45a049;
    }
</style>
