using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomation.Pages;

namespace UIAutomation.BusinessModel
{
    public class BusinessFlows:BaseTest
    {
        public BusinessFlows AddFlows()
        {
            GetInstance<TestSiteElements>().SetSum1Elements().SetSum2Elements().ClickAddButton();
            return this;
        }
        public BusinessFlows SubtractionFlows()
        {
            GetInstance<TestSiteElements>().SetSum1Elements().SetSum2Elements().ClickAddButton();
            return this;
        }
    }
}
