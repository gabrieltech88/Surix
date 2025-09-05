const text = document.getElementById('info')

document.addEventListener("DOMContentLoaded", async () => {

    const response = await fetch(`${window.env.PROD}/sure/content`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    })

    const data = await response.json();
    const name = data.name
    const count = data.totalCount
 

    text.textContent = `${name} • ${count} registros`
})
