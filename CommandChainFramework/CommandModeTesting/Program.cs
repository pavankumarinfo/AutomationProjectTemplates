using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.VisualBasic;
//using McMaster.Extensions.CommandLineUtils;

namespace CommandChainFramework.CommandModeTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        //public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        //private int OnExecute()
        //{
        //    return 0;
        //}

        // These are the command line arguments using the McMaster NuGet package
        //[Required]
        //[Option(Description = "The name of the application being tested", ShortName = "a",
        //    LongName = "ApplicationName")]
        //public string ApplicationName { get; set; }

        //[Required]
        //[DirectoryExists]
        //[Option(Description = "The path to the unit tests", ShortName = "u", LongName = "UnitTestPath")]
        //public string UnitTestPath { get; set; }

        //[Required]
        //[Option(Description = "The environment that is being tested", ShortName = "e", LongName = "Environment")]
        //[AllowedValues("debug", "local", "test", "beta", "training", "train", "uat", "prod", "production")]
        //public string Environment { get; set; }

        //[Option(Description = "This SkipProjectName allows people to skip with *, skips complete tests from performance", ShortName = "s", LongName = "SkipProjectName")]
        //public string SkipProjectName { get; set; }
    }
}
