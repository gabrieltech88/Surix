const btn = document.getElementById('confirmar');
const formCalc = document.getElementById('form-register-calc')
const card = document.getElementById('container-confirmation-percent')

btn.addEventListener("click", async () => {

    btn.textContent = "Confirmando..."

    const stakeTotal = document.getElementById('input-valor-total').value
    const oddA = document.getElementById('input1').value
    const oddB = document.getElementById('input2').value
    const eventName = document.getElementById('evento-calc').value
    const casaA = document.getElementById('casa1-calc').value
    const casaB = document.getElementById('casa2-calc').value

    const data = {
        event: eventName,
        casaA,
        casaB,
        oddA: parseFloat(parseFloat(oddA).toFixed(2)),
        oddB: parseFloat(parseFloat(oddB).toFixed(2)),
        stake: stakeTotal
    }

    const response = await fetch(`${window.env.PROD}/sure`, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })

    if (response.ok) {

        document.getElementById('input-valor-total').value = ""
        document.getElementById('input1').value = ""
        document.getElementById('input2').value = ""
        document.getElementById('evento-calc').value = ""
        document.getElementById('casa1-calc').value = ""
        document.getElementById('casa2-calc').value = ""

        Swal.fire({
            icon: 'success',
            title: 'Sucesso!',
            text: 'Sure registrada com sucesso',
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

        btn.textContent = "Confirmar"
        formCalc.style.display = "none"
        card.style.display = "none"
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Erro',
            text: 'Erro ao registrar sure',
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

        btn.textContent = "Confirmar"
    }
})
