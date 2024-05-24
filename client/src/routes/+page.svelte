<div class="hero-section">
    <div class="home-text">
        <h1>Art Reveals Talent</h1>
        <p>Discover, buy, and sell stunning artworks</p>
    </div>

    <div class="info-boxes">
        <div class="info-box">
            <h2>Explore Our Gallery</h2>
            <p>Immerse yourself in a world of artistic brilliance. Our gallery showcases a diverse collection of artworks from talented artists around the globe.</p>
            <a href="/Gallery" class="btn">Visit Gallery</a>
        </div>
        <div class="info-box">
            <h2>About Us</h2>
            <p>Learn more about our mission to celebrate and promote the talents of artists worldwide. Discover our story and the passion that drives us.</p>
            <a href="/AboutUs" class="btn">About Us</a>
        </div>
    </div>
</div>

<style>
    .hero-section {
        background-color: white;
        background-size: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        color: orange;
        text-align: center;
        padding: 2rem;
    }

    .home-text {
        padding: 1em;
    }

    .hero-section h1 {
        font-size: 3rem;
        margin-bottom: 1rem;
    }

    .hero-section p {
        font-size: 1.5rem;
        margin-bottom: 2rem;
    }

    .info-boxes {
        display: flex;
        justify-content: center;
        gap: 2rem;
        margin-top: 2rem;
    }

    .info-box {
        background-color: #f8f8f8;
        padding: 2rem;
        border-radius: 8px;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        max-width: 400px;
        text-align: left;
    }

    .info-box h2 {
        color: #333;
        font-size: 1.5rem;
        margin-bottom: 1rem;
    }

    .info-box p {
        color: #555;
        font-size: 1rem;
        margin-bottom: 1.5rem;
    }

    .btn {
        display: inline-block;
        padding: 0.8rem 1.5rem;
        background-color: orange;
        color: white;
        text-decoration: none;
        border-radius: 4px;
        transition: background-color 0.3s ease;
    }

    .btn:hover {
        background-color: #333333;
    }
</style>