const btn = document.getElementById('btn-surix');

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}



btn.addEventListener("click", async () => {

    const token = getCookie('jwt');
    console.log(token)

    const event = document.getElementById('evento').value
    const casaA = document.getElementById('casaA').value
    const casaB = document.getElementById('casaB').value
    const oddA = document.getElementById('oddA').value
    const oddB = document.getElementById('oddB').value
    let stakeA = parseFloat(document.getElementById('stakeA').value)
    let stakeB = parseFloat(document.getElementById('stakeB').value)

    const stakeTotal = stakeA + stakeB;
    console.log(stakeTotal)

    btn.textContent = "Registrando..."

    const data = {
        event,
        casaA,
        casaB,
        oddA: parseFloat(parseFloat(oddA).toFixed(2)),
        oddB: parseFloat(parseFloat(oddB).toFixed(2)),
        stake: stakeTotal
    }

    const response = await fetch("https://localhost:8800/sure/sure", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data),
    })

    if (response.ok) {
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

        btn.textContent = "Registrar"
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

        btn.textContent = "Registrar"
    }
})

export default getCookie;