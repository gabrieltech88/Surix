const form = document.getElementById('form')
const btn = document.getElementById('btn-form')

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    btn.textContent = "Registrando Usuário..."

    const name = event.target.nome.value;
    const email = event.target.email.value;
    const userName = event.target.user.value;
    const password = event.target.password.value;

    const response = await fetch("https://localhost:8800/user/create", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            name,
            userName,
            email,
            password
        })
    })

    if (response.ok) {
        window.location.href = "https://localhost:8800/index";
        btn.textContent = "Criar Conta"
    } else {
        alert("Falha ao cadastrar usuário")
    }

})
