using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class ReturnFunctionTest : LanguageTestBase
    {

        [TestMethod]
        public void ReturnTest()
        {
            SObject result = ResetParseAndGo("a = 33; return(a); a =1;");
            Assert.AreEqual(new SObject(33), result);
        }
    }
}
