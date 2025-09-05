;
;


const x = document.getElementById('x')
const link = document.getElementById('placeholder-link')
const btn = document.getElementById('btn-submit-forgot')
const form = document.getElementById('form-forgot');

x.addEventListener("click", () => {
    event.preventDefault()
    form.style.display = "none";
})

link.addEventListener("click", () => {
    event.preventDefault()
    form.style.display = "flex";
})

form.addEventListener("submit", async (event) => {
    event.preventDefault();
    btn.textContent = "Atualizando..."

    const username = event.target.usernameForgot.value;
    const novaSenha = event.target.newPassword.value;
    const novaSenhaConfirmada = event.target.confirmedPassword.value;

    if (novaSenha === novaSenhaConfirmada) {
        const result = await fetch(`${window.env.DEV}/user/password`, {
            method: "PATCH",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                username,
                password: novaSenha
            })
        })

        if (result.ok) {
            Swal.fire({
                icon: 'success',
                title: 'Sucesso!',
                didOpen: () => {
                    const container = document.querySelector('.swal2-container');
                    if (container) container.style.zIndex = '999999';
                },
                text: 'Senha Atualizada',
                customClass: {
                    content: 'my-swal-text' // para o texto principal
                },
                timer: 5000,
                timerProgressBar: true,
                confirmButtonText: 'OK',
                background: '#1b232d',
                color: '#fff',
                confirmButtonColor: '#00e900'
            });

            btn.textContent = "Redefinir senha"
            form.style.display = "none";
        }
    }
})