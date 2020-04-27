using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using Xunit.Sdk;

namespace UIAutomation
{
    public static class browerTypes
    {
        public static string getChrome => "chrome";
        public static string getFirefox => "firefox";
        public static string getIe => "ie";
        public static string getEdge => "edge";
    }

    //demo test site: https://www.seleniumeasy.com/test/basic-first-form-demo.html

    public class BaseTest
    {

        protected static IWebDriver _driver { get; set; } = new ChromeDriver(options());
       
        public BaseTest()
        {

        }

        public static ChromeOptions options()
        {
            ChromeOptions info = new ChromeOptions();
            info.AddArgument("--start-maximized");
            return info;
        }

        //public void IniliazeBaseTest(string browserType)
        //{
        //    switch (browserType)
        //    {
        //        case "chrome":
        //            _driver = new ChromeDriver();
        //            break;
        //        case "firefox":
        //            _driver = new FirefoxDriver();
        //            break;
        //        case "ie":
        //            _driver = new InternetExplorerDriver();
        //            break;
        //        case "edge":
        //            _driver = new EdgeDriver();
        //            break;
        //        default:
        //            _driver = new ChromeDriver();
        //            break;
        //    }

        //    //initilize elements
        //    //PageFactory.InitElements(_driver, this);
        //}

        public BaseTest IniliazeBaseTest(string browserType)
        {
            if (_driver == null)
            {
                switch (browserType)
                {
                    case "chrome":
                        _driver = new ChromeDriver(options());
                        break;
                    case "firefox":
                        _driver = new FirefoxDriver();
                        break;
                    case "ie":
                        _driver = new InternetExplorerDriver();
                        break;
                    case "edge":
                        _driver = new EdgeDriver();
                        break;
                    default:
                        _driver = new ChromeDriver();
                        break;
                }
            }

            //initilize elements
            PageFactory.InitElements(_driver, this);
            return this;
        }

        public void BasicBrowserSettings()
        {
            _driver.Manage().Window.Maximize();
        }

        public T GetInstance<T>()
        {
            try
            {
                return (T) Activator.CreateInstance(typeof(T));
            }
            finally{}
        }
    }
}
