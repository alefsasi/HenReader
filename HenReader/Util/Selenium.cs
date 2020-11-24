using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HenReader.Util
{
    public class Selenium
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        private string baseURL = @"https://nhentai.net/";
        private int tempoTransicao;


        public Selenium(int tempo)
        {
            try
            {
                driver = new ChromeDriver();
                js = (IJavaScriptExecutor)driver;
                tempoTransicao = tempo;
                driver.Manage().Window.Maximize();

            }
            catch (Exception ex)
            {
                throw new Exception(@"Erro ao Carregar driver do Navegador! 
É necessário fazer download do driver do navegador no endereço: http://chromedriver.storage.googleapis.com/index.html 
Em seguida descompacte no mesmo diretório desta aplicação.");
            } // TESTE 2

        }

        public void RandomRead()
        {
            driver.Navigate().GoToUrl(baseURL + "random/");
            Thread.Sleep(500);

            Read();

        }
        public void FavoritoRead(string username, string password)
        {
            var isLoged = Logar(username, password);

            if (isLoged != baseURL)
            {
                throw new Exception("login ou senha incorreta");

            }
            driver.Navigate().GoToUrl(isLoged + "favorites/");

            Thread.Sleep(200);

            FavoritoRandom();

        }

        private string Logar(string username, string password)
        {

            driver.Navigate().GoToUrl(baseURL + "login/");

            var usernameBox = driver.FindElement(By.Id("id_username_or_email"));
            var passwordBox = driver.FindElement(By.Id("id_password"));
            var submitButton = driver.FindElement(By.CssSelector("button.button-wide"));

            usernameBox.SendKeys(username);
            passwordBox.SendKeys(password);
            submitButton.Click();

            Thread.Sleep(200);


            return driver.Url;
        }


        private void FavoritoRandom()
        {
            try
            {
                var randomButton = driver.FindElement(By.Id("favorites-random-button"));
                randomButton.Click();
                Thread.Sleep(500);
                Read();
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro Inesperado!");
            }

        }
        private void Read()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.Id("cover")).Click();
            Thread.Sleep(3000);

            var numPages = Int32.Parse(driver.FindElement(By.ClassName("num-pages")).Text);

            var containerHeight = driver.FindElement(By.Id("image-container")).Size.Height;
            Thread.Sleep(1000);

            for (int i = 1; i < numPages; i++)
            {

                var scroll = containerHeight / 50;

                for (int j = 0; j < scroll; j++)
                {
                    Thread.Sleep(tempoTransicao);
                    js.ExecuteScript("window.scrollBy(0,30)");
                }

                driver.FindElement(By.ClassName("next")).Click();

            }

            driver.FindElement(By.ClassName("back-to-gallery")).Click();

        }


        public void Exit()
        {
            driver.Quit();
        }
    }
}
