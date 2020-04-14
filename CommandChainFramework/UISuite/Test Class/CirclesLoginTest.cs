using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using CirclesSeleniumTestScripts.PageFlows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CirclesSeleniumTestScripts.PageElements;

namespace CirclesSeleniumTestScripts.Test_Class
{
    [TestClass]
    public class GmailLoginTest
    {
        [TestMethod]
        public void LoginToCirclesAccountAndverifyEmailAddress()
        {
            try
            {
                IWebDriver driver = new FirefoxDriver();
                CircleLoginFlows.NavigateCirclesLoginPage(driver);
                CircleLoginFlows.VerifyCirclesVerifyPage(driver);
                driver.Close();

            }
            catch (Exception ex)
            {
                Assert.Fail(" CreateNewStore :" + ex.Message);
            }
        }
    }
}
