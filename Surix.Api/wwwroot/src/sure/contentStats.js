function animarNumero(element, valorFinal, duracao = 1000, prefixo = "", casasDecimais = 2, sufixo = "") {
    let inicio = 0;
    const incremento = valorFinal / (duracao / 16);

    const intervalo = setInterval(() => {
        inicio += incremento;
        if (inicio >= valorFinal) {
            inicio = valorFinal;
            clearInterval(intervalo);
        }
        element.textContent = prefixo + inicio.toFixed(casasDecimais);
    }, 16);
}

document.addEventListener("DOMContentLoaded", async () => {
    const response = await fetch("https://surix.runasp.net/sure/stats");
    const data = await response.json();

    animarNumero(document.getElementById("lucro"), data.lucroMensal, 1000, "R$ ", 2); // 2 casas decimais
    animarNumero(document.getElementById("roi"), data.roiMensal, 1000, "", 2, "%");       // 2 casas decimais
    animarNumero(document.getElementById("sure"), data.quantidadeSures, 800, "", 0); // sem casas decimais
    animarNumero(document.getElementById("stake"), data.stakeTotal, 1000, "R$ ", 2);
    animarNumero(document.getElementById("lucro2"), data.lucroMensal, 1000, "R$ ", 2);  
    animarNumero(document.getElementById("roi2"), data.roiMensal, 1000, "", 2, "%");      // 2 casas decimais
});