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

    let selectedWeek = '';

    function addTimeSlot(day, startTime, endTime, weekOffset) {
        try {
            if (!startTime || !endTime) {
                console.error('Start time and end time must be specified.');
                return;
            }

            const currentWeekNumber = moment().isoWeek();
            const selectedDateTime = moment().startOf('isoWeek').add(weekOffset, 'weeks').isoWeekday(day);

            const startDateTime = selectedDateTime.clone().set({ 'hour': parseInt(startTime.split(':')[0]), 'minute': parseInt(startTime.split(':')[1]), 'second': 0, 'millisecond': 0 });
            const endDateTime = selectedDateTime.clone().set({ 'hour': parseInt(endTime.split(':')[0]), 'minute': parseInt(endTime.split(':')[1]), 'second': 0, 'millisecond': 0 });

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

        if (selectedWeek) {
            const weekCount = parseInt(selectedWeek.split(' ')[0]);

            for (let weekOffset = 0; weekOffset < weekCount; weekOffset++) {
                for (const day of Object.keys(selectedDays)) {
                    if (selectedDays[day]) {
                        const timeSlot = addTimeSlot(day, startTime, endTime, weekOffset);
                        if (timeSlot) {
                            timeSlots.push(timeSlot);
                        }
                    }
                }
            }
        }

        if (timeSlots.length > 0) {
            try {
                const promises = timeSlots.map(async (timeSlot) => {
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

                    console.log(`Time slot added successfully for ${timeSlot.day} on ${moment(timeSlot.startTime).format('YYYY-MM-DD')}.`);
                });

                await Promise.all(promises);
            } catch (error) {
                console.error('Error while adding time slots:', error);
            }
        }
    }

    function logCheckboxStatus(day, checked) {
        if (checked) {
            console.log(`Checked - ${day}`);
        } else {
            console.log(`Unchecked - ${day}`);
        }
    }
</script>

<div class="multipletimeslot">
    <h2>Weekdays</h2>
    <div class="checkboxes">
        {#each Object.keys(selectedDays) as day}
            <label>
                <input type="checkbox" bind:checked="{selectedDays[day]}" on:change={() => logCheckboxStatus(day, selectedDays[day])}>
                {day}
            </label>
        {/each}
    </div>

    <div class="week-selection">
        <h3>Select Weeks:</h3>
        {#each ['1 week', '2 weeks', '3 weeks', '4 weeks', '5 weeks'] as week}
            <label>
                <input type="radio" bind:group="{selectedWeek}" value="{week}">
                {week}
            </label>
        {/each}
    </div>

    <div class="time-inputs">
        <input type="time" bind:value="{startTime}" step="3600">
        <input type="time" bind:value="{endTime}" step="3600">
        <button on:click={sendTimeSlots}>Save Weekly</button>
    </div>
</div>

<style>
    h2, h3 {
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
        border: solid 5px red;
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

    .week-selection {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin-bottom: 10px;
    }

    .week-selection label {
        margin-bottom: 5px;
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

