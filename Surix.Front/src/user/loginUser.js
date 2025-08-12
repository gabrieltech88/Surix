const btn = document.getElementById("btn-form-login")
const form = document.getElementById("form-login")

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const userName = document.getElementById("userName").value
    const password = document.getElementById("password").value

    const data = {
        userName,
        password
    }

    btn.textContent = "Logando..."

    const response = await fetch("https://localhost:8800/user/manipulation/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })

    if (response.ok) {
        const response2 = await fetch("https://localhost:8800/surix")

        if (response2.ok) {
            btn.textContent = "Entrar"
            window.location.href = "/surix"; // redireciona manualmente
        }
    }

    btn.textContent = "Entrar"
})
