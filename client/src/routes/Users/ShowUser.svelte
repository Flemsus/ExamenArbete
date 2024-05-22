<script>
    import { onMount } from 'svelte';
    import { PUBLIC_API_URL } from '$env/static/public';

    let users = [];

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
    });
</script>

<div class="container"> 
    <ul class="user-list">
        <h2>All Users</h2>
        {#each users as user}
            <li class="user-item">
                <div>ID: {user.id}</div>
                <div>Name: {user.name}</div>
                <div>Email: {user.email}</div>
                <div>Phone Number: {user.phoneNumber}</div>
                <div>Password: {user.password}</div>
            </li>
        {/each}
    </ul>
</div>

<style>
    h2 {
        text-align: center;
        font-size: 2em;
    }

    .container {
        margin: 20px;
    }

    .user-list {
        list-style: none; 
        padding: 0;
        border: solid red 5px;
        background-color: white;
    }

    .user-item {
        padding: 10px;
        border-top: solid red 5px;
    }

    .user-item div {
        margin-bottom: 5px;
    }

</style>
