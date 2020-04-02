using CommandChainFramework.BusinessModel;



namespace CommandChainFramework.Nunit
{
    class environmentNames
    {
        public const string TestEnvironment = "test";
        public const string BetaEnvironment = "beta";
        public const string TestRefactorEnvironment = "testrefactor";
        public const string BetaRefactorEnvironment = "betarefactor";
    }
    [TestFixture]
    public class TestClass : BaseTest
    {
        [Test]
        public void getReggieRequest()
        {
            GetInstance<ReggieGet>()
                .getReggieRequest(ReggieBase.ReggieQuismoGet + "/177522623", out var assertresponsecode);
            assertresponsecode.ShouldBe("OK");
        }

        [Test]
        [TestCase(environmentNames.TestEnvironment)]
        [TestCase(environmentNames.BetaEnvironment)]
        [TestCase(environmentNames.TestRefactorEnvironment)]
        [TestCase(environmentNames.BetaRefactorEnvironment)]
        public void getReggieRequest_envBasedTest(string environmentname)
        {
            GetInstance<ReggieGet>().GetEnvSettings(environmentname, out var envName)
                .getReggieRequest(ReggieBase.ReggieQuismoGet + "/177522623", envName, out var assertresponsecode);

            assertresponsecode.ShouldBe("OK");
        }
    }
}
