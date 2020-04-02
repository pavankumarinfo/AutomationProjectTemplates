using System;
using System.Collections.Generic;
using System.Text;

namespace CommandChainFramework.UnitTest
{
    public class Helper
    {
        public static string TestEnvironment { get; } = "test";
        public static string BetaEnvironment { get; } = "beta";
        public static string TestRefactorEnvironment { get; } = "testrefactor";
        public static string BetaRefactorEnvironment { get; } = "betarefactor";
    }
}
