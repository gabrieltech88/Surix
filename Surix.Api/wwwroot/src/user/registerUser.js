;
;

const form = document.getElementById('form')
const btn = document.getElementById('btn-form')

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    btn.textContent = "Registrando Usuário..."

    const name = event.target.nome.value;
    const email = event.target.email.value;
    const userName = event.target.user.value;
    const password = event.target.password.value;

    const response = await fetch(`${window.env.DEV}/user/create`, {
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
        Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Usuário cadastrado com sucesso!',
            customClass: {
                content: 'my-swal-text' // para o texto principal
            },
            timer: 3000,
            timerProgressBar: true,
            confirmButtonText: 'OK',
            background: '#1b232d',
            color: '#fff',
            confirmButtonColor: '#00e900'
        }).then(() => {
            // Após o alerta ser fechado, redireciona
            window.location.href = `${window.env.DEV}`;
            btn.textContent = "Criar Conta";
        });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Erro!',
            text: 'Ocorreu um erro ao cadastrar o usuário',
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
        btn.textContent = "Criar Conta"
    }

})
