using CommandChainFramework.RestHttpEngine;

namespace CommandChainFramework.BusinessModel
{
    public class ReggieGet : BusinessBase
    {
        public ReggieGet getReggieRequest(string getUrl, out string responsestatus)
        {
            string env;
            GetInstance<ReggieBase>().GetBetaEnvSetting(out env).GetInstance<RestCalls>().getHttpInstance(env)
                .HttpGetAndAssertCalls(getUrl, out responsestatus);
            return this;
        }

        public ReggieGet getReggieRequest(string getUrl, string environmentdomain, out string responsestatus)
        {
            GetInstance<RestCalls>().getHttpInstance(environmentdomain)
                 .HttpGetAndAssertCalls(getUrl, out responsestatus);
            return this;
        }

        public ReggieGet GetEnvSettings(string environment, out string environmentdomain)
        {
            string envdomain = null;
            switch (environment.ToLower())
            {
                case "test":
                    GetInstance<ReggieBase>().GetTestEnvSetting(out envdomain);
                    break;
                case "beta":
                    GetInstance<ReggieBase>().GetBetaEnvSetting(out envdomain);
                    break;
                case "testrefactor":
                    GetInstance<ReggieBase>().GetTestRefactorEnvSetting(out envdomain);
                    break;
                case "betarefactor":
                    GetInstance<ReggieBase>().GetBetaRefactorEnvSetting(out envdomain);
                    break;
                default:
                    GetInstance<ReggieBase>().GetTestEnvSetting(out envdomain);
                    break;
            }

            environmentdomain = envdomain;

            return this;
        }
    }
}
