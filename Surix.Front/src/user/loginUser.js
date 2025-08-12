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

    const response = await fetch("https://localhost:8800/user/manipulation/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })

    if (response.ok) {
        const token = await response.text();
        document.cookie = `jwt=${token}; path=/; Secure; SameSite=Strict`;

        const response2 = await fetch("https://localhost:8800/surix", {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            }
        })

        if (response2.ok) {
            window.location.href = "/surix"; // redireciona manualmente
        }
    }
})
