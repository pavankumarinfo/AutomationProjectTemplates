﻿using UIAutomation.BusinessModel;
using Xunit;
namespace UIAutomation.Tests
{
    public class Tests:Base
    {
        [Fact]
        public void AdditionTest()
        {
            GetInstance<BaseTest>().IniliazeBaseTest(browerTypes.getChrome)
                 .GetInstance<Helper>().Wait(2)
                 .GetInstance<HomePageFlows>().NavigateToHomePage()
                 .GetInstance<Helper>().Wait(2)
                 .GetInstance<BusinessFlows>().AddFlows();
        }

        [Fact]
        public void SubstractionTest()
        {
            GetInstance<BaseTest>().IniliazeBaseTest(browerTypes.getChrome)
                .GetInstance<Helper>().Wait(2)
                .GetInstance<HomePageFlows>().NavigateToHomePage()
                .GetInstance<Helper>().Wait(2)
                .GetInstance<BusinessFlows>().SubtractionFlows();
        }
    }
}