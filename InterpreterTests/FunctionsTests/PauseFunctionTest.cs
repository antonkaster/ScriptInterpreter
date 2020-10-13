using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class PauseFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void PauseTest()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            ResetParseAndGo("pause(10)");
            stopwatch.Stop();

            Assert.IsTrue(stopwatch.ElapsedMilliseconds > 10 && stopwatch.ElapsedMilliseconds < 100);
        }
    }
}
