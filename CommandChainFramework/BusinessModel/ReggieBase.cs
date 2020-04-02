using System;

namespace CommandChainFramework.BusinessModel
{
    public class ReggieBase
    {
        public static string ReggieHttpDomain { get; } = "http://";
        public static string ReggieRefactorHttpsDomain { get; } = "https://";
        public static string ReggieTestDomain { get; } = "reggieTest";
        public static string ReggieRefactorTestDomain { get; } = "reggieRefactorTest";
        public static string ReggieBetaDomain { get; } = "reggieBeta";
        public static string ReggieRefactorBetaDomain { get; } = "reggierefactorBeta";
        public static string ReggieQuismoGet { get; } = "/api/v1/Quismo";

        public ReggieBase GetTestEnvSetting(out string domainvalue)
        {
            domainvalue = ReggieHttpDomain + ReggieTestDomain;
            return this;
        }
        public ReggieBase GetTestRefactorEnvSetting(out string domainvalue)
        {
            domainvalue = ReggieRefactorHttpsDomain + ReggieRefactorTestDomain;
            return this;
        }
        public ReggieBase GetBetaEnvSetting(out string domainvalue)
        {
            domainvalue = ReggieHttpDomain + ReggieBetaDomain;
            return this;
        }
        public ReggieBase GetBetaRefactorEnvSetting(out string domainvalue)
        {
            domainvalue = ReggieRefactorHttpsDomain + ReggieRefactorBetaDomain;
            return this;
        }

        public T GetInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

    }

}
