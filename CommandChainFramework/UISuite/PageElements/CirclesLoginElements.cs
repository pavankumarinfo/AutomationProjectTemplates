using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CirclesSeleniumTestScripts.PageElements
{
    public class CirclesLoginElements
    {
        private readonly IWebDriver driver;

        public CirclesLoginElements(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath,
        Using = "//span[contains(.,'SIGN IN')]")]
        public IWebElement CirclesLoginButton { get; set; }

        [FindsBy(How = How.XPath,
             Using = "//input[@name='email']")] 
        public IWebElement CirclesLoginTextBox { get; set; }

        [FindsBy(How = How.XPath,
            Using = "//input[@name='password']")]
        public IWebElement CirclesPasswordTextBox { get; set; }

        [FindsBy(How = How.XPath,
       Using = "//input[@value='pabbi.circles@gmail.com']")]
        public IWebElement UserProfilePageEmailAddress { get; set; }

        [FindsBy(How = How.XPath,
Using = "//div[@style='height: 50px; padding: 0px 10px; display: flex; align-items: center; font-weight: 600;']")]
        public IWebElement CirclesLogoutButton { get; set; }

    }
}
