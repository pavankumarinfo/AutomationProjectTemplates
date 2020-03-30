using System;

namespace CommandChainFramework.BusinessModel
{
    public class ReggieBase
    {
        public const string ReggieHttpDomain = "http://";
        public const string ReggieRefactorHttpsDomain = "https://";
        public const string ReggieTestDomain = "reggieTest";
        public const string ReggieRefactorTestDomain = "reggieRefactorTest";
        public const string ReggieBetaDomain = "reggieBeta";
        public const string ReggieRefactorBetaDomain = "reggierefactorBeta";
        public const string ReggieQuismoGet = "/api/v1/Quismo";

        #region "old code"
        public static string getTestEnvSetting()
        {
            return ReggieHttpDomain + ReggieTestDomain;
        }
        public static string getTestRefactorTestEnvSetting()
        {
            return ReggieRefactorHttpsDomain + ReggieRefactorTestDomain;
        }
        public static string getBetaEnvSetting()
        {
            return ReggieHttpDomain + ReggieBetaDomain;
        }
        public static string getBetaRefactorTestEnvSetting()
        {
            return ReggieRefactorHttpsDomain + ReggieRefactorBetaDomain;
        }
        #endregion

        //public ReggieBase getTestEnvSetting(out string domainvalue)
        //{
        //    domainvalue = ReggieHttpDomain + ReggieTestDomain;
        //    return this;
        //}
        //public ReggieBase getTestRefactorTestEnvSetting(out string domainvalue)
        //{
        //    domainvalue = ReggieRefactorHttpsDomain + ReggieRefactorTestDomain;
        //    return this;
        //}
        //public ReggieBase getBetaEnvSetting(out string domainvalue)
        //{
        //    domainvalue = ReggieHttpDomain + ReggieBetaDomain;
        //    return this;
        //}
        //public ReggieBase getBetaRefactorTestEnvSetting(out string domainvalue)
        //{
        //    domainvalue = ReggieRefactorHttpsDomain + ReggieRefactorBetaDomain;
        //    return this;
        //}

    }

}
