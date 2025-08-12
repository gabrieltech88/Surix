import loadPage from "./contentSure.js";

const btn = document.getElementById('btn-surix');
let pageNumber = 1
let pageSize = 4

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}



btn.addEventListener("click", async () => {

    const token = getCookie('jwt');
    console.log(token)

    let inputEvent = document.getElementById('evento')
    let inputCasaA = document.getElementById('casaA')
    let inputCasaB = document.getElementById('casaB')
    let inputOddA = document.getElementById('oddA')
    let inputOddB = document.getElementById('oddB')
    let inputStakeA = document.getElementById('stakeA')
    let inputStakeB = document.getElementById('stakeB')

    let event = inputEvent.value
    let casaA = inputCasaA.value
    let casaB = inputCasaB.value
    let oddA = inputOddA.value
    let oddB = inputOddB.value
    let stakeA = parseFloat(inputStakeA.value)
    let stakeB = parseFloat(inputStakeB.value)


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

    const response = await fetch("https://localhost:8800/sure", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(data),
    })

    if (response.ok) {
        loadPage(1);

        inputEvent.value = ""
        inputCasaA.value = ""
        inputCasaB.value = "" 
        inputOddA.value = "" 
        inputOddB.value = "" 
        inputStakeA.value = "" 
        inputStakeB.value = ""

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