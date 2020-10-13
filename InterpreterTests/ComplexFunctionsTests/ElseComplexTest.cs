using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class ElseComplexTest : LanguageTestBase
    {

        [TestMethod]
        public void IfTrueElseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(true) { a = 2; } else { a = 3; }; a;");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void IfFalseElseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(false) { a = 2; } else { a = 3; }; a;");
            Assert.AreEqual(new SObject(3), result);
        }

        [TestMethod]
        public void DoubleElseTest()
        {
            SObject result = ResetParseAndGo("a=1; if(a != 1) { a=2; } else { a=3; }; if(a>3) { a=4; } else { a=5; } a;");
            Assert.AreEqual(new SObject(5), result);
        }
    }
}
