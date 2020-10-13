using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class WhileComplexTest : LanguageTestBase
    {
        [TestMethod]
        public void CounterWhileTest()
        {
            SObject result = ResetParseAndGo("a=1; while(a<5){ a=a+1; }; a;");
            Assert.AreEqual(new SObject(5), result);
        }

        [TestMethod]
        public void FalseWhileTest()
        {
            SObject result = ResetParseAndGo("a=1; while(false){ a=a+1; }; a;");
            Assert.AreEqual(new SObject(1), result);
        }

        [TestMethod]
        public void WhileIfTest()
        {
            SObject result = ResetParseAndGo("a=1; b=true; while(b){ if(a<5){ a=a+1; }else{ b=false; } }; a;");
            Assert.AreEqual(new SObject(5), result);
        }
    }
}
