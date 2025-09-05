const text1 = document.getElementById('aposta-text-1');
const text2 = document.getElementById('aposta-text-2');
const value1 = document.getElementById('value1');
const value2 = document.getElementById('value2');
const totalApostado = document.getElementById('total-apostado');
const retornoGarantido = document.getElementById('retorno-garantido');
const lucroText = document.getElementById('lucro');
const confirmation = document.getElementById('container-confirmation-percent')
const btnNao = document.getElementById('btnNao')
const textPercent = document.getElementById('texPercent')
const formCalc = document.getElementById('form-register-calc')
const x = document.getElementById('x')

const input1 = document.getElementById('input1');
const input2 = document.getElementById('input2');
const inputValor = document.getElementById('input-valor-total');

// Função que só chama calcular() se todos os inputs estiverem válidos
function checarECalcular() {
    const val1 = input1.value.trim();
    const val2 = input2.value.trim();
    const valorTotal = inputValor.value.trim();

    const odd1 = parseFloat(val1);
    const odd2 = parseFloat(val2);
    const total = parseFloat(valorTotal);

    if (!isNaN(odd1) && !isNaN(odd2) && !isNaN(total) && odd1 > 0 && odd2 > 0 && total > 0) {
        calcular(odd1, odd2, total);
    } else {
        // Limpa ou zera os resultados
        text1.textContent = 'Casa A - Odd 0';
        text2.textContent = 'Casa B - Odd 0';
        value1.textContent = 'R$ 0,00';
        value2.textContent = 'R$ 0,00';
        retornoGarantido.textContent = 'R$ 0,00';
        lucroText.textContent = 'R$ 0,00';
        totalApostado.textContent = 'R$ 0,00';
    }
}

// Função principal de cálculo
function calcular(odd1, odd2, total) {
    confirmation.style.display = 'flex'

    text1.textContent = `Casa A - Odd ${odd1}`;
    text2.textContent = `Casa B - Odd ${odd2}`;

    const inverso1 = 1 / odd1;
    const inverso2 = 1 / odd2;

    const sumInversos = inverso1 + inverso2;

    const percentual1 = inverso1 / sumInversos;
    const percentual2 = inverso2 / sumInversos;

    const aposta1 = percentual1 * total;
    const aposta2 = percentual2 * total;

    const retorno1 = aposta1 * odd1;
    const retorno2 = aposta2 * odd2;

    const retornoFinal = Math.min(retorno1, retorno2);
    const lucro = retornoFinal - total;

    value1.textContent = `R$ ${aposta1.toFixed(2)}`;
    value2.textContent = `R$ ${aposta2.toFixed(2)}`;
    retornoGarantido.textContent = `R$ ${retornoFinal.toFixed(2)}`;
    lucroText.textContent = `R$ ${lucro.toFixed(2)}`;
    totalApostado.textContent = `R$ ${total.toFixed(2)}`;

    const stakeA = (1 / odd1) / (1 / odd1 + 1 / odd2) * total;
    const stakeB = total - aposta1;

    const retornoA = aposta1 * odd1;
    const retornoB = aposta2 * odd2;

    const lucroA = Math.min(retornoA, retornoB) - total;
    const roi = (lucroA / total) * 100;

    textPercent.textContent = `${roi.toFixed(2)}%`
}

// Adiciona os listeners nos 3 inputs
[input1, input2, inputValor].forEach(input => {
    input.addEventListener('input', checarECalcular);
});


btnNao.addEventListener("click", () => {
    confirmation.style.display = 'none'
})

btnSim.addEventListener("click", () => {
    formCalc.style.display = 'flex'
})

x.addEventListener("click", () => {
    formCalc.style.display = 'none'
})