<script>
    import { onMount } from 'svelte';
    import moment from 'moment';
    import { PUBLIC_API_URL } from '$env/static/public';

    let selectedDate;
    let singleStartTime = '';
    let singleEndTime = '';
    let selectedWeek = '';

    async function addSingleTimeSlot() {
        try {
            if (!singleStartTime || !singleEndTime) {
                console.error('Start time and end time must be specified.');
                return;
            }

            const selectedDateTime = moment(selectedDate);

            const startDateTime = selectedDateTime.clone().set({ 'hour': parseInt(singleStartTime.split(':')[0]), 'minute': parseInt(singleStartTime.split(':')[1]) });
            const endDateTime = selectedDateTime.clone().set({ 'hour': parseInt(singleEndTime.split(':')[0]), 'minute': parseInt(singleEndTime.split(':')[1]) });

            const newTimeSlot = {
                startTime: startDateTime.toISOString(),
                endTime: endDateTime.toISOString()
            };

            const res = await fetch(`${PUBLIC_API_URL}/time-slot`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newTimeSlot),
            });

            if (!res.ok) {
                throw new Error('Post failed');
            }

            console.log('Single time slot added successfully.');
        } catch (error) {
            console.error('Error while adding single time slot:', error);
        }
    }

    onMount(() => {
        selectedDate = moment().format('YYYY-MM-DD');
    });
</script>

<div class="singletimeslot">
    <h2>Add Time Slot</h2>
    <div>
        <label for="selectedDate">Select Date:</label>
        <input type="date" bind:value="{selectedDate}">
        <input type="time" bind:value="{singleStartTime}" step="3600" />
        <input type="time" bind:value="{singleEndTime}" step="3600" />
        <button on:click={addSingleTimeSlot}>Save Single</button>
    </div>
</div>

<style>

h2 {
    text-decoration: underline;
    font-weight: bold;
    font-size: 20px;
} 

    .singletimeslot {
        display: flex;
        flex-direction: column;
        align-items: center;
        margin: 1em;
        text-align: center;
        border: solid 5px;
        border-color: green;
        background: white;
    }

    input[type="date"],
    input[type="time"] {
        margin-bottom: 10px;
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
