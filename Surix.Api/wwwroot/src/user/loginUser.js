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

    const response = await fetch(`${window.env.DEV}/user/manipulation/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })

    if (response.ok) {
        const response2 = await fetch(`${window.env.DEV}/surix`)

        if (response2.ok) {
            btn.textContent = "Entrar"
            window.location.href = "/surix"; // redireciona manualmente
        }
    }

    Swal.fire({
        icon: 'error',
        title: 'Erro!',
        text: 'Senha ou Usuário incorreto',
        customClass: {
            content: 'my-swal-text' // para o texto principal
        },
        timer: 5000,
        timerProgressBar: true,
        confirmButtonText: 'OK',
        background: '#1b232d',
        color: '#fff',
        confirmButtonColor: '#f02727ff'
    });

    btn.textContent = "Entrar"
})
