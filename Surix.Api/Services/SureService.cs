using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Surix.Api.Data.DTO;
using Surix.Api.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace Surix.Api.Services;

public class SureService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;


    public SureService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<List<Surebet>> GetInfoSures()
    {
        var surebets = new List<Surebet>();

        var options = new ChromeOptions();
        options.AddArgument("--headless=new"); // headless mais novo
        options.AddArgument("--disable-blink-features=AutomationControlled"); // remove detecção
        options.AddArgument("--mute-audio");
        options.AddArgument("--no-sandbox");
        options.AddArgument("--disable-gpu");
        options.AddArgument("--disable-infobars");
        options.AddArgument("--disable-dev-shm-usage");
        options.AddArgument("--start-maximized");
        options.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/139.0.7258.127 Safari/537.36");

        using var driver = new ChromeDriver(options);
        driver.Manage().Cookies.DeleteAllCookies();
        driver.Navigate().GoToUrl("https://zerolossbet.com");

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

        // --- LOGIN ---
        wait.Until(d => d.FindElement(By.Id("floatingInput"))).SendKeys("Thiagox7kol@gmail.com");
        driver.FindElement(By.Id("floatingInput1")).SendKeys("123");
        driver.FindElement(By.CssSelector("button[type='submit']")).Click();

        // --- Espera página carregar ---
        wait.Until(d => d.FindElement(By.Id("pills-home-tab")));

        // --- Aba Pré-live ---
        var preLiveTab = driver.FindElement(By.Id("pills-home-tab"));
        ScrollToElement(driver, preLiveTab);
        SimulateHumanClick(driver, preLiveTab);

        // --- Espera os cards carregarem ---
        wait.Until(d => d.FindElements(By.ClassName("card-body")).Count > 0);
        var cards = driver.FindElements(By.ClassName("card-body"));
        foreach (var card in cards)
        {
            try
            {
                var eventoFull = card.FindElement(By.CssSelector("h6.text-gray-400")).Text;
                var eventoLinhas = eventoFull.Split('\n');

                string evento = eventoLinhas[0].Trim();
                string campeonato = eventoLinhas[1].Trim();
                string categoria = eventoLinhas[2].Trim();

                // --- Captura Data e Hora ---
                string dataHoraStr = card.FindElement(By.CssSelector("small.text-muted")).Text.Trim();
                // Exemplo: "14/08/2025 17:30"

                string dataJogo = "";
                string horaJogo = "";

                var partes = dataHoraStr.Split(' ');
                if (partes.Length >= 2)
                {
                    dataJogo = partes[0];
                    horaJogo = partes[1];
                }

                var mercados = card.FindElements(By.CssSelector("p[data-desc]"));
                var botoes = card.FindElements(By.CssSelector("button.btn.btn-secondary"));

                string casa1 = botoes[0].Text.Split('\n')[0].Trim();
                string odd1 = botoes[0].Text.Split('\n')[1].Trim();

                string casa2 = botoes[1].Text.Split('\n')[0].Trim();
                string odd2 = botoes[1].Text.Split('\n')[1].Trim();

                surebets.Add(new Surebet
                {
                    Evento = evento,
                    Campeonato = campeonato,
                    Categoria = categoria,
                    Data = dataJogo,   // <-- novo campo
                    Hora = horaJogo,   // <-- novo campo
                    Mercado1 = mercados[0].Text,
                    Casa1 = casa1,
                    Odd1 = odd1,
                    Mercado2 = mercados[1].Text,
                    Casa2 = casa2,
                    Odd2 = odd2
                });
            }
            catch
            {
                // ignora cards que deram erro
            }
        }

        driver.Quit();
        return surebets;
    }

    // --- Função para scroll suave ---
    private void ScrollToElement(IWebDriver driver, IWebElement element)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({behavior: 'smooth', block: 'center'});", element);
        Thread.Sleep(500); // pausa curta
    }

    // --- Função para simular clique humano ---
    private void SimulateHumanClick(IWebDriver driver, IWebElement element)
    {
        var actions = new Actions(driver);
        actions.MoveToElement(element).Pause(TimeSpan.FromMilliseconds(new Random().Next(200, 500))).Click().Perform();
        Thread.Sleep(300); // pausa pós clique
    }

}