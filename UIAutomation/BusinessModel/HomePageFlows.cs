namespace UIAutomation.BusinessModel
{
    public class HomePageFlows:BaseTest
    {
        private Helper _helper=new Helper();
        //declartion of menu items
        //delcartion of basic page urls navigation
        public static string url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html";

        //public HomePageFlows NavigateToPage(string pageUrl)
        //{
        //    GetInstance<Helper>().NavigateToUrl(pageUrl,_driver);
        //    return this;
        //}
        //public HomePageFlows NavigateToHomePage()
        //{
        //   GetInstance<Helper>().NavigateToUrl(url,_driver);
        //    return this;
        //}

        public HomePageFlows NavigateToPage(string pageUrl)
        {
            _helper.NavigateToUrl(pageUrl, _driver);
            return this;
        }
        public HomePageFlows NavigateToHomePage()
        {
            _helper.NavigateToUrl(url, _driver);
            return this;
        }
    }
}
