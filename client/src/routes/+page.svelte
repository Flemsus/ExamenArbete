<script>
	import Icon from "../components/Icon.svelte";

</script>

<svelte:head>
	<title>Home</title>
	<meta name="description" content="Home" />
</svelte:head>

<h1>Home</h1>

<div>
	<Icon name="Min Profil" src="images/profile.png" alt="profile-icon" link="/MyProfile"/>
	<Icon name="Schema" src="images/schedule.png" alt="schedule-icon" link="/Schedule"/>
	<Icon name="Tillgänglighet" src="images/check.png" alt="avaiability-icon" link="/Register"/>
	<Icon name="Lönespecifikation" src="images/salary.png" alt="salary-icon" link="/SalarySpec"/>
</div>

<style lang="scss">
h1 {
    font-family: 'Silian Rail', sans-serif;
    font-size: 3em;
    font-weight: bold;
    text-align: center;
}

div {
    display: flex;
    flex-wrap: wrap;
    justify-content: center; 
    align-items: center;
    max-width: 60%;
    margin: 0 auto; 
    margin-top: 1em; 
    margin-bottom: 1em; 	
}
    
</style>