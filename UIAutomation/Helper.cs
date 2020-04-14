using OpenQA.Selenium;
using System;

namespace UIAutomation
{
    public class Helper : BaseTest
    {
        Helper()
        {
            
        }
        public Helper NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);
            return this;
        }

        public Helper GetWebElementById(string elementID,out IWebElement elementById)
        {
            Wait(1);
            elementById=_driver.FindElement(By.Id(elementID));
            return this;
        }
        public Helper GetWebElementByName(string elementName, out IWebElement elementByName)
        {
            Wait(1);
            elementByName=_driver.FindElement(By.Name(elementName));
            return this;
        }

        public IWebElement GetWebElementById(string elementID)
        {
            Wait(1);
            return _driver.FindElement(By.Id(elementID));
        }

        public IWebElement GetWebElementByLinkText(string elementID)
        {
            Wait(1);
            return _driver.FindElement(By.LinkText(elementID));
        }

        public IWebElement GetWebElementByName(string elementName)
        {
            Wait(1);
            return _driver.FindElement(By.Name(elementName));
        }

        public void SendKeys(IWebElement webElement,string inputString)
        {
            Wait(1);
            webElement.SendKeys(inputString);
        }

        public void clickButton(IWebElement webElement)
        {
            Wait(1);
            webElement.Click();
        }

        public Helper Wait(int seconds)
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
            return this;
        }
    }
}
