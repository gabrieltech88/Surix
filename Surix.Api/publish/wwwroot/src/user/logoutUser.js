;
;


const btn = document.getElementById('logout')

btn.addEventListener("click", async () => {
    const response = await fetch(`${window.env.PROD}/user/manipulation/logout`)

    if(response.ok) {
        window.location.href = `${window.env.PROD}`
    }
})