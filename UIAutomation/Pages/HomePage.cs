using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIAutomation.Pages
{
    public class HomePage:BaseTest
    {
        public HomePage setHomePage(string url)
        {
            GetInstance<Helper>().NavigateToUrl(url);
            return this;
        }
    }
}
