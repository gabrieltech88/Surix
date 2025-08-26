
const btn = document.getElementById('logout')

btn.addEventListener("click", async () => {
    const response = await fetch("https://surix.runasp.net/user/manipulation/logout")

    if(response.ok) {
        window.location.href = 'https://surix.runasp.net/'
    }
})