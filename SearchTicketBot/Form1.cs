using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchTicketBot
{
    public partial class Form1 : Form
    {
        IWebDriver Browser;

        public Form1()
        {
            InitializeComponent();
        }

        private bool Calculate()
        {
            

            string departure = textBox1.Text;
            string arrival = textBox2.Text;
            string date = textBox3.Text;

            Browser.Navigate().GoToUrl("https://booking.uz.gov.ua/ru/");

            IWebElement SearchDepartureCity = Browser.FindElement(By.Name("from-title"));
            SearchDepartureCity.SendKeys(departure);
            System.Threading.Thread.Sleep(1000);
            SearchDepartureCity.SendKeys(OpenQA.Selenium.Keys.ArrowDown + OpenQA.Selenium.Keys.Enter);


            IWebElement SearchArrivalCity = Browser.FindElement(By.Name("to-title"));
            SearchArrivalCity.SendKeys(arrival);
            System.Threading.Thread.Sleep(1000);
            SearchArrivalCity.SendKeys(OpenQA.Selenium.Keys.ArrowDown + OpenQA.Selenium.Keys.Enter + OpenQA.Selenium.Keys.Enter);

            // set date

            IWebElement SetDate = Browser.FindElement(By.CssSelector("input[name='date']"));

            var driver = ((IWrapsDriver)SetDate).WrappedDriver;
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);", SetDate, "value", date);

            System.Threading.Thread.Sleep(1000);

            List<IWebElement> subButtons = Browser.FindElements(By.TagName("button")).ToList();

            System.Threading.Thread.Sleep(1000);

            subButtons.Last().Submit();

            System.Threading.Thread.Sleep(1000);


            bool plExist = false;
            bool kuExist = false;

            try
            {
                IWebElement platskart = Browser.FindElement(By.CssSelector("input[data-wagon-id='П']")) ?? null;
                plExist = true;
            }
            catch (Exception) { }


            try
            {
                IWebElement kupe = Browser.FindElement(By.CssSelector("input[data-wagon-id='К']")) ?? null;
                kuExist = true;
            }
            catch (Exception) { }


            if (plExist) return plExist;
            
            if (kuExist) return kuExist;

            return false;

            
        }
        

        private void button1_Click(object sender, EventArgs e)
        {


            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverService);

            

            while (Calculate() == false)
            {
                System.Threading.Thread.Sleep(10000);
            }


            Browser.Quit();

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver(chromeDriverService);
            Browser.Manage().Window.Maximize();
            Browser.Navigate().GoToUrl("https://booking.uz.gov.ua/ru/");
        }


    }
}
