using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace SearchTicketBot
{
    public partial class Form1 : Form
    {
        IWebDriver Browser;
        
        public Form1()
        {
            InitializeComponent();
        }



        private void Calculate()
        {
            string departure = textBox1.Text;
            string arrival = textBox2.Text;
            string date = textBox3.Text;

            Browser.Navigate().GoToUrl("https://booking.uz.gov.ua/ru/");

            IWebElement SearchDepartureCity = Browser.FindElement(By.Name("from-title"));
            SearchDepartureCity.SendKeys(departure);
            System.Threading.Thread.Sleep(5000);
            SearchDepartureCity.SendKeys(OpenQA.Selenium.Keys.ArrowDown + OpenQA.Selenium.Keys.Enter);


            IWebElement SearchArrivalCity = Browser.FindElement(By.Name("to-title"));
            SearchArrivalCity.SendKeys(arrival);
            System.Threading.Thread.Sleep(5000);
            SearchArrivalCity.SendKeys(OpenQA.Selenium.Keys.ArrowDown + OpenQA.Selenium.Keys.Enter + OpenQA.Selenium.Keys.Enter);

            // set date

            IWebElement SetDate = Browser.FindElement(By.CssSelector("input[name='date']"));

            var driver = ((IWrapsDriver)SetDate).WrappedDriver;
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", SetDate, "value", date);

            System.Threading.Thread.Sleep(5000);

            List<IWebElement> subButtons = Browser.FindElements(By.TagName("button")).ToList();

            System.Threading.Thread.Sleep(5000);

            subButtons.Last().Submit();

            System.Threading.Thread.Sleep(5000);


            bool plExist = false;
            bool kuExist = false;

            try
            {
                IWebElement platskart = Browser.FindElement(By.CssSelector("input[data-wagon-id='П']")) ?? null;
                plExist = true;
            }
            catch (Exception) { }


            if (kuExist || plExist)
            {
                Browser.Quit();

                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;

                Browser = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverService);
                Browser.Manage().Window.Maximize();
                Browser.Navigate().GoToUrl("https://booking.uz.gov.ua/ru/");

                WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();
                wplayer.URL = @"D:/Chrome Downloads/ding.mp3";
                wplayer.controls.play();

            }




        }


        public async Task DoSomethingAndWaitAsync()
        {
            while (true)
            { 
                await Task.Run(() => Calculate());
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }



        private async void button1_ClickAsync(object sender, EventArgs e)
        {

            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverService);

            await DoSomethingAndWaitAsync();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
