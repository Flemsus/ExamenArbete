<script>
</script>

<div class="container">
  <div class="options-menu">
    <p>Options</p>
      <button>Option 1</button>
      <button>Option 2</button>
      <button>Option 3</button>
  </div>
</div>

<style>

p {
    text-align: center;
}

.container {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center; 
  align-items: center; 
}

.options-menu {
    background-color: #f2d09e;
    border: 1px solid #ccc; 
    border-radius: 5px; 
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1); 
    padding: 10px;
}

.options-menu button {
    display: block;
    width: 100%;
    padding: 8px 12px;
    margin-bottom: 5px;
    border: none;
    background-color: white; 
    color: #333; 
    font-size: 16px;
    text-align: left; 
}

.options-menu button:hover {
    background-color: #f0f0f0; 
}
</style>
