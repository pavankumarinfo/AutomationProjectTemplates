using OpenQA.Selenium;

namespace UIAutomation.Pages
{
    public partial class TestSiteElements : BaseTest
    {

        public IWebElement Sum1WebElement => GetWebElementByIdElement("Sum1");
        public IWebElement Sum2WebElement => GetWebElementByIdElement("Sum2");
        public IWebElement GetTotalWebElement => GetWebElementByLinkTextElement("Get Total");

        private IWebElement GetWebElementByIdElement(string elementPropertyValue)
        {
            return GetInstance<Helper>().GetWebElementById(elementPropertyValue);
        }
        private IWebElement GetWebElementByLinkTextElement(string elementPropertyValue)
        {
            return GetInstance<Helper>().GetWebElementByLinkText(elementPropertyValue);
        }

        public TestSiteElements SetSum1Elements()
        {
            GetInstance<Helper>().SendKeys(Sum1WebElement,"1");
            return this;
        }
        public TestSiteElements SetSum2Elements()
        {
            GetInstance<Helper>().SendKeys(Sum2WebElement, "2");
            return this;
        }

        public TestSiteElements ClickAddButton()
        {
            GetInstance<Helper>().clickButton(GetTotalWebElement);
            return this;
        }
    }
}
