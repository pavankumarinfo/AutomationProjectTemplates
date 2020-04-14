using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace CirclesSeleniumTestScripts.PageElements
{
    public class FacebookLoginPageElements
    {
        private readonly IWebDriver driver;
        
        public FacebookLoginPageElements(IWebDriver browser)
        {
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@type='email']")]
        public IWebElement facebookLoginEmailTextBox { get; set; }

        [FindsBy(How = How.XPath,
             Using = "//input[@id='pass']")] //pabbi.circles@gmail.com
        public IWebElement facebookLoginPasswordTextBox { get; set; }

        [FindsBy(How = How.XPath,
            Using = "//input[@type='submit']")] //abc123456
        public IWebElement facebookLoginSubmitButton { get; set; }

         [FindsBy(How = How.XPath,
     Using = "//div[@style='height: 50px; padding: 0px 10px; display: flex; align-items: center; font-weight: 600;']")]
        public IWebElement FacebookLogoutButton { get; set; }
    }
}
