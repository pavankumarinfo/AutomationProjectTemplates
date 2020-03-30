using CommandChainFramework.BusinessModel;
using CommandChainFramework.RestHttp;
using Shouldly;
using Xunit;

namespace UnitTest
{
    public class ReggieTests : Base<ReggieTests>
    {
        private RestCalls _restCalls = new RestCalls(ReggieBase.getTestEnvSetting());

        [Fact]
        public void getReggieRequest()
        {
            string env = null;
            //new Base<ReggieBase>().GetNewInstance().getBetaEnvSetting(out env);

            var reggieResponse = _restCalls.httpGet(ReggieBase.ReggieQuismoGet + "/490432933");
            reggieResponse.IsSuccessful.ShouldBe(true);
            reggieResponse.Request.Body.ShouldBeNull();
            reggieResponse.Content.ShouldNotBeNullOrEmpty();
        }
    }
}
