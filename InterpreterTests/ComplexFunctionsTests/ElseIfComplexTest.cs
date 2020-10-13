using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class ElseIfComplexTest : LanguageTestBase
    {
        [TestMethod]
        public void IfTrueElseIfTrueElseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(true) { a = 2; } elseif (true) { a = 3; } else { a = 4; };  a;");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void IfFalseElseIfTrueElseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(false) { a = 2; } elseif (true) { a = 3; } else { a = 4; };  a;");
            Assert.AreEqual(new SObject(3), result);
        }
        

        [TestMethod]
        public void IfFalseElseIfFalseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(false) { a = 2; } elseif (false) { a = 3; };  a;");
            Assert.AreEqual(new SObject(1), result);
        }
        
        [TestMethod]
        public void IfFalseElseIfFalseElseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(false) { a = 2; } elseif (false) { a = 3; } else { a = 4; };  a;");
            Assert.AreEqual(new SObject(4), result);
        }
    }
}
