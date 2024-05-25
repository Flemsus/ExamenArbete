<script>
    import '@fortawesome/fontawesome-free/css/all.css';
</script>

<div class="footer">
    <div class="footer-content">
        <div class="left-section">
            <div class="logo"><a href="/"><img src="/images/Logo.png" alt="Logo" /></a></div>
        </div>
        <div class="center-section">
            <div class="copyright">Art Reveals Talent &copy;</div>
        </div>
        <div class="right-section">
            <div class="social-icons">
                <a href="https://www.facebook.com" aria-label="Facebook">
                    <i class="fab fa-facebook"></i>
                </a>
                <a href="https://twitter.com" aria-label="Twitter">
                    <i class="fab fa-twitter"></i>
                </a>
                <a href="https://www.instagram.com" aria-label="Instagram">
                    <i class="fab fa-instagram"></i>
                </a>
            </div>
        </div>
    </div>
</div>

<style>
    .footer {
        background-color: #333;
        color: white;
        padding: 1rem 2rem;
        display: flex;
        justify-content: center;
        align-items: center;
        width: 100%;
        max-width: 64rem;
        align-self: center;
        position: relative;
        bottom: 0;
    }

    .footer-content {
        display: flex;
        justify-content: space-between;
        align-items: center;
        width: 100%;
        max-width: 64rem;
    }

    .left-section {
        flex: 1;
        display: flex;
        justify-content: flex-start;
        align-items: center;
    }

    .center-section {
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .right-section {
        flex: 1;
        display: flex;
        justify-content: flex-end;
        align-items: center;
    }

    .left-section img {
        height: 2rem;
    }

    .social-icons {
        display: flex;
        gap: 1rem;
    }

    .social-icons a {
        color: white;
        text-decoration: none;
        font-size: 1.5rem;
        transition: color 0.3s;
    }

    .social-icons a:hover {
        color: #ff7b00;
    }

    .copyright {
        font-size: 1rem;
    }

    @media (max-width: 600px) {
    .footer {
        padding: 0.5rem 1rem;
    }

    .footer-content {
        flex-direction: column;
        align-items: center;
    }

    .left-section, .center-section, .right-section {
        flex: unset;
        width: 100%;
        justify-content: center;
        margin-bottom: 0.5rem;
    }

    .left-section {
        margin-bottom: 0.2rem;
    }

    .left-section img {
        height: 3rem;

    }

    .social-icons a {
        font-size: 1.2rem;
    }

    .copyright {
        font-size: 0.8rem;
    }

   
    .center-section {
        order: 2; 
        margin-bottom: 0.1rem;
    }

    .right-section {
        order: 1; 
    }
}

</style>
