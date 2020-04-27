namespace UIAutomation.Pages
{
    public class HomePage:BaseTest
    {
        private readonly Helper _helper = new Helper();
        public HomePage SetHomePage(string url)
        {
            _helper.NavigateToUrl(url);
            return this;
        }
    }
}
