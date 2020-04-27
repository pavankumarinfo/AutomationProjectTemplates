using OpenQA.Selenium;

namespace UIAutomation.Pages
{
    public partial class TestSiteElements : BaseTest
    {
        private Helper _helper = new Helper();
        public IWebElement Sum1WebElement => getWebElementByCssElement("input#sum1");//("Sum1");
        public IWebElement Sum2WebElement => getWebElementByCssElement("input#sum2");
        public IWebElement GetTotalWebElement => getWebElementByCssElement("form#gettotal > button");
        private IWebElement getWebElementByCssElement(string elementPropertyValue)
        {
            return _helper.GetWebElementByCssElement(elementPropertyValue);
        }
        private IWebElement GetWebElementByIdElement(string elementPropertyValue)
        {
            return _helper.GetWebElementById(elementPropertyValue);
        }
        private IWebElement GetWebElementByLinkTextElement(string elementPropertyValue)
        {
            return _helper.GetWebElementByLinkText(elementPropertyValue);
        }

        public TestSiteElements SetSum1Elements()
        {
            _helper.SendKeys(Sum1WebElement, "1");
            return this;
        }
        public TestSiteElements SetSum2Elements()
        {
            _helper.SendKeys(Sum2WebElement, "2");
            return this;
        }

        public TestSiteElements ClickAddButton()
        {
            _helper.ClickButton(GetTotalWebElement);
            return this;
        }
    }
}
