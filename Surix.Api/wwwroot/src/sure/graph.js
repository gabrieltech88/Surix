import getCookie from "./registerSure.js";
;
;


const ctx = document.getElementById('meuGrafico').getContext('2d');

const grafico = new Chart(ctx, {
    type: 'line',
    data: {
        labels: [],
        datasets: [{
            label: 'ROI (%)',
            data: [],
            borderColor: '#20df66',
            tension: 0.6,
            fill: true,
            backgroundColor: 'rgba(32, 223, 102, 0.2)', 
            pointRadius: 4,
            pointBackgroundColor: '#20df66',
            borderWidth: 2
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        interaction: {
            mode: 'nearest',
            axis: 'x',
            intersect: false
        },
        scales: {
            x: {
                grid: {
                    display: false
                },
                ticks: {
                    color: '#ededed',
                    font: { size: 12, family: 'Arial' }
                }
            },
            y: {
                beginAtZero: true,
                grid: {
                    color: '#22252f',
                    drawBorder: false
                },
                ticks: {
                    color: '#ededed',
                    font: { size: 12, family: 'Arial' }
                }
            }
        },
        plugins: {
            legend: {
                labels: {
                    usePointStyle: true,
                    pointStyle: 'circle',
                    color: '#ededed',
                    font: { size: 12, weight: 'bold' }
                }
            }
        }
    }
});

document.addEventListener("DOMContentLoaded", async () => {

    const token = getCookie('jwt')
    const response = await fetch(`${window.env.DEV}/sure/roi`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    });

    const data = await response.json()

   

    await data.forEach((item) => {

        const roi = item.somaRoi
        const dayMonth = `${item.dia}/${item.mes}`

        if (!grafico.data.datasets[0].data.includes(roi)) {
            grafico.data.datasets[0].data.push(roi)
            grafico.update()
        }

        if (!grafico.data.labels.includes(dayMonth)) {
            grafico.data.labels.push(dayMonth)
            grafico.update()
        }
    })

    

    const dayMonth = `${day}/${month}`


})