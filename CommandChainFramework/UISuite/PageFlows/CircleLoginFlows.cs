using OpenQA.Selenium;
using CirclesSeleniumTestScripts.PageElements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CirclesSeleniumTestScripts.PageFlows
{
    public static class CircleLoginFlows:UIBaseTest
    {

        public static IWebDriver NavigateCirclesLoginPage(IWebDriver driver)
        {
            
            Helper.NavigatetoCirclesPages(driver, Circlesurls.CirclesHomePage);
            Helper.NavigatetoCirclesPages(driver, Circlesurls.CirclesLoginPage);
            CirclesLoginElements circleloginpage = new CirclesLoginElements(driver);

            circleloginpage.CirclesLoginTextBox.SendKeys(Username.GetLoginID());
            circleloginpage.CirclesPasswordTextBox.SendKeys(Loginpassword.GetPassword());
            circleloginpage.CirclesLoginButton.Click();
            return driver;
        }

        public static IWebDriver VerifyCirclesVerifyPage(IWebDriver driver)
        {

            Helper.Sleep(driver, 10000);
            Helper.NavigatetoCirclesPages(driver, Circlesurls.CirclesUserProfilePage);
            Helper.Sleep(driver, 10000);

            CirclesLoginElements circleloginpage = new CirclesLoginElements(driver);
            Assert.AreEqual("pavan.circles@gmail.com", circleloginpage.UserProfilePageEmailAddress.Text);
            Helper.NavigatetoCirclesPages(driver, Circlesurls.CirclesHomePage);
            return driver;
        }
    }
}
