using OpenQA.Selenium;
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

        private void button1_Click(object sender, EventArgs e)
        {
            string departure = textBox1.Text;
            string arrival = textBox2.Text;
            string date = textBox3.Text;

            Browser = new OpenQA.Selenium.Chrome.ChromeDriver();
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

            IWebElement submitButton = Browser.FindElement(By.TagName("button"));
            List<IWebElement> subButtons = Browser.FindElements(By.TagName("button")).ToList();

            System.Threading.Thread.Sleep(1000);

            subButtons.Last().Submit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Browser.Quit();
        }
    }
}
