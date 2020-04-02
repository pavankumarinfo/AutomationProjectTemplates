﻿using System;
using CommandChainFramework.BusinessModel;
using Shouldly;
using Xunit;

namespace CommandChainFramework.UnitTest
{
    class environmentNames
    {
        public const string TestEnvironment  = "test";
        public const string BetaEnvironment  = "beta";
        public const string TestRefactorEnvironment = "testrefactor";
        public const string BetaRefactorEnvironment = "betarefactor";
    }
    public class ReggieTests : BaseTest
    {
        [Fact]
        public void getReggieRequest()
        {
            GetInstance<ReggieGet>()
                .getReggieRequest(ReggieBase.ReggieQuismoGet + "/177522623", out var assertresponsecode);
            assertresponsecode.ShouldBe("OK");
        }

        [Theory]
        [InlineData(environmentNames.TestEnvironment)]
        [InlineData(environmentNames.BetaEnvironment)]
        [InlineData(environmentNames.TestRefactorEnvironment)]
        [InlineData(environmentNames.BetaRefactorEnvironment)]
        public void getReggieRequest_envBasedTest(string environmentname)
        {
            GetInstance<ReggieGet>().GetEnvSettings(environmentname, out var envName)
                .getReggieRequest(ReggieBase.ReggieQuismoGet + "/177522623", envName, out var assertresponsecode);

            assertresponsecode.ShouldBe("OK");
        }
    }
}
