
const btn = document.getElementById('logout')

btn.addEventListener("click", async () => {
    const response = await fetch("https://localhost:8800/user/manipulation/logout")

    if(response.ok) {
        window.location.href = 'https://localhost:8800/'
    }
})