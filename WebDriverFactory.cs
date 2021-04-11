using System;
using System.IO;
using OpenQA.Selenium;

namespace GoogleCalendarWebDriver
{
    public enum BrowserType
    {
        Undefined = 0,
        GoogleChrome,
        Edge,
        FireFox,
    }
    public class WebDriverFactory
    {
        private string DriverPath = Path.Combine(Environment.CurrentDirectory, "WebDriver");

        public IWebDriver GetDriver(BrowserType browser)
        {
            try
            {
                switch (browser)
                {
                    case BrowserType.Edge:
                        return new OpenQA.Selenium.Edge.EdgeDriver(DriverPath);
                    case BrowserType.GoogleChrome:
                        return new OpenQA.Selenium.Chrome.ChromeDriver(DriverPath);
                    case BrowserType.FireFox:
                        return new OpenQA.Selenium.Firefox.FirefoxDriver(DriverPath);
                    default:
                        throw new AggregateException("Browser is not supported.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
