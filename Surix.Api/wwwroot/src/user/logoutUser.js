;
;


const btn = document.getElementById('logout')

btn.addEventListener("click", async () => {
    const response = await fetch(`${window.env.DEV}/user/manipulation/logout`)

    if(response.ok) {
        window.location.href = `${window.env.DEV}`
    }
})