import getCookie from "./registerSure.js"

const table = document.getElementById('body-table')
const btnLeft = document.getElementById('btn-left')
const btnRight = document.getElementById('btn-right')

let pageNumber = 1
let pageSize = 4

document.addEventListener("DOMContentLoaded", async () => {
    const token = getCookie('jwt')
    console.log(token)
    table.innerHTML = "";

    const response = await fetch("https://localhost:8800/sure/content", {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    })

    const data = await response.json();
    const sures = data.sures
    console.log(data)


    sures.forEach((sure) => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${sure.date}</td>
            <td>${sure.event}</td>
            <td>${sure.stake}</td>
            <td>${sure.casaA}</td>
            <td>${sure.oddA}</td>
            <td>${sure.casaB}</td>
            <td>${sure.oddB}</td>
            <td>${sure.lucro}</td>
            <td>${sure.roi}</td>
        `
        table.appendChild(tr);
    });

})

btnLeft.addEventListener("click", () => {

    if (pageNumber > 1) {
        loadPage(pageNumber - 1);
    }
})

btnRight.addEventListener("click", () => {
    loadPage(pageNumber + 1);
})

const loadPage = async (page) => {
    if (page < 1) return; // não permite página < 1
    pageNumber = page;
    const token = getCookie('jwt')

    table.innerHTML = "";

    // faz fetch com pageNumber e pageSize
    const response = await fetch(`https://localhost:8800/sure/content?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    });

    const data = await response.json();
    const sures = data.sures

    sures.forEach((sure) => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${sure.date}</td>
            <td>${sure.event}</td>
            <td>${sure.stake}</td>
            <td>${sure.casaA}</td>
            <td>${sure.oddA}</td>
            <td>${sure.casaB}</td>
            <td>${sure.oddB}</td>
            <td>${sure.lucro}</td>
            <td>${sure.roi}</td>
        `

        table.appendChild(tr);
    });


};