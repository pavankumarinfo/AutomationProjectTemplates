namespace UIAutomation.BusinessModel
{
    public class HomePageFlows:BaseTest
    {
        //declartion of menu items
        //delcartion of basic page urls navigation
        public static string url = "https://www.seleniumeasy.com/test/basic-first-form-demo.html";

        public HomePageFlows NavigateToPage(string pageUrl)
        {
            GetInstance<Helper>().NavigateToUrl(pageUrl);
            return this;
        }
        public HomePageFlows NavigateToHomePage()
        {
           GetInstance<Helper>().NavigateToUrl(url);
            return this;
        }
    }
}
