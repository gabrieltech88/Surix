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

    const token = await fetch("https://localhost:8800/user/manipulation/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })

    if (token.ok) {
        document.cookie = `jwt=${token}; path=/; Secure; SameSite=Strict`;
        window.location.href = "/privado"
    }
})
