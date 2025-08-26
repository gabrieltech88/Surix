function calcularLucro(odd1, odd2) {
    const soma = (1 / odd1) + (1 / odd2);
    const porcentagem = soma * 100;
    return (100 - porcentagem).toFixed(2); // lucro garantido
}

let inicializado = false;

async function carregarSurebets() {
    try {
        const response = await fetch('/sure/get');
        const dados = await response.json();
        const section = document.getElementById('section-sure');

        if (!inicializado) {
            // Cria os cards apenas na primeira vez
            for (let i = 0; i < dados.length; i += 3) {
                const column = document.createElement('div');
                column.classList.add('column');

                const grupo = dados.slice(i, i + 3);
                grupo.forEach(item => {
                    const lucro = calcularLucro(item.odd1, item.odd2);

                    const card = document.createElement('div');
                    card.classList.add('card');

                    card.innerHTML = `
                        <div class="line">
                            <div class="line1">
                                <div class="line-line1">
                                    <p class="categoria">${item.categoria}</p>
                                    <p class="campeonato">
                                        ${item.campeonato}
                                    </p>
                                </div>
                                <p class="porcentagem">
                                    <img src="./assets/arrow.svg" style="width: 16px; height: 16px;">
                                    ${lucro}%
                                </p>
                            </div>
                            <h2 class="evento">${item.evento}</h2>
                            <div class="container-data-hora">
                                <p class="data">
                                    <img src="./assets/calendar.svg" style="height: 14px; width: 14px;">
                                    ${item.data}
                                </p>
                                <p class="hora">
                                    <img src="./assets/watch.svg" style="height: 14px; width: 14px;">
                                    ${item.hora}
                                </p>
                            </div>
                            <div class="container-jogos">
                                <div class="jogos">
                                    <div class="jogo">
                                        <p class="casa1">${item.casa1}</p>
                                        <p class="mercado1">${item.mercado1}</p>
                                        <p class="odd1">${item.odd1}</p>
                                    </div>
                                    <div class="jogo">
                                        <p class="casa1">${item.casa2}</p>
                                        <p class="mercado1">${item.mercado2}</p>
                                        <p class="odd1">${item.odd2}</p>
                                    </div>
                                </div>
                                <div class="container-vs">
                                    <p class="vs">VS</p>
                                </div>
                            </div>
                        </div>
                        <div class="line-line">
                            <p class="lucro-garantido"><img src="./assets/trophy.svg" alt="" style="width: 14px; height: 14px;">Lucro Garantido</p>
                            <p class="retorno">${lucro}% de retorno</p>
                        </div>
                    `;

                    column.appendChild(card);
                });

                section.appendChild(column);
            }

            inicializado = true;
        } else {
            // Atualiza os dados dos elementos já existentes
            const categorias = document.querySelectorAll('.categoria');
            const campeonatos = document.querySelectorAll('.campeonato');
            const porcentagens = document.querySelectorAll('.porcentagem');
            const eventos = document.querySelectorAll('.evento');
            const datas = document.querySelectorAll('.data');
            const horas = document.querySelectorAll('.hora');
            const casas = document.querySelectorAll('.casa1');
            const mercados = document.querySelectorAll('.mercado1');
            const odds = document.querySelectorAll('.odd1');
            const retornos = document.querySelectorAll('.retorno');

            dados.forEach((item, index) => {
                const lucro = calcularLucro(item.odd1, item.odd2);

                const i1 = index * 2;     // primeira aposta
                const i2 = index * 2 + 1; // segunda aposta

                categorias[index].textContent = item.categoria;
                campeonatos[index].innerHTML = ` ${item.campeonato}`;
                porcentagens[index].innerHTML = `<img src="./assets/arrow.svg" style="width: 16px; height: 16px;"> ${lucro}%`;
                eventos[index].textContent = item.evento;
                datas[index].innerHTML = `<img src="./assets/calendar.svg" style="height: 14px; width: 14px;"> ${item.data}`;
                horas[index].innerHTML = `<img src="./assets/watch.svg" style="height: 14px; width: 14px;"> ${item.hora}`;

                // Atualiza as duas apostas
                casas[i1].textContent = item.casa1;
                mercados[i1].textContent = item.mercado1;
                odds[i1].textContent = item.odd1;

                casas[i2].textContent = item.casa2;
                mercados[i2].textContent = item.mercado2;
                odds[i2].textContent = item.odd2;

                retornos[index].textContent = `${lucro}% de retorno`;
            });
        }
    } catch (erro) {
        console.error("Erro ao carregar dados:", erro);
    }
}

// Atualiza a cada 3 segundos
setInterval(carregarSurebets, 3000);
carregarSurebets();
