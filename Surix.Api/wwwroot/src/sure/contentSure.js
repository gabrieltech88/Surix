import getCookie from "./registerSure.js"


const table = document.getElementById('body-table')
const btnLeft = document.getElementById('btn-left')
const btnRight = document.getElementById('btn-right')
const text = document.getElementById('info')
const textPages = document.getElementById('text-pages')

let pageNumber = 1
let pageSize = 5
let paginas = []
let pageController = 1



document.addEventListener("DOMContentLoaded", async () => {
    const token = getCookie('jwt')
    console.log(token)
    table.innerHTML = "";

    const response = await fetch(`${window.env.PROD}/sure/content`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        }
    })

    const data = await response.json();
    const sures = data.sures
    const name = data.name
    const count = data.totalCount
    const pages = Number(data.totalCount) / 5

    if(!Number.isInteger(pages)) {
        const page = Math.floor(pages) + 1
        textPages.textContent = `1 - ${page}`
        paginas.push(page)
    } else {
        textPages.textContent = `1 - ${pages}`
        paginas.push(pages)
    }

    text.textContent = `${name} • ${count} registros`


    sures.forEach((sure) => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${formatDate(sure.date)}</td>
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
        pageController -= 1
        textPages.textContent = `${pageController} - ${paginas}`
    }

})

btnRight.addEventListener("click", () => {

    if(pageController < paginas) {
        loadPage(pageNumber + 1);
        pageController += 1
        textPages.textContent = `${pageController} - ${paginas}`
    }
})

const loadPage = async (page) => {
    if (page < 1) return; // não permite página < 1
    pageNumber = page;
    const token = getCookie('jwt')

    table.innerHTML = "";

    // faz fetch com pageNumber e pageSize
    const response = await fetch(`${window.env.PROD}/sure/content?pageNumber=${pageNumber}&pageSize=${pageSize}`, {
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
            <td>${formatDate(sure.date)}</td>
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

function formatDate(dateString) {
    const [datePart] = dateString.split(' '); // Pega apenas a parte da data (descarta hora)
    const [year, month, day] = datePart.split('-');
    return `${day}/${month}/${year}`;
}

export default loadPage;