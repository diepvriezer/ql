using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Languages.QLang.Grammar;
using Antlr4.Runtime;
using QL.Languages.QLang;
using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Ast.Types;
using QL.Languages.QLang.Ast.Expressions;
using QL.Languages.QLang.Ast.Statements;

namespace QL.Tests.Languages.QLang
{
    [TestClass]
    public class LanguageTests : TestBase
    {
        [TestMethod]
        public void ParsesQuestions()
        {
            var src = @"form TestForm testQuestion: ""test me!"" bool";
            var result = Build(src);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Statement, typeof(Question));
            var question = result.Statement as Question;
            Assert.AreEqual("testQuestion", question.Id);
            Assert.AreEqual("test me!", question.Text);
            Assert.IsInstanceOfType(question.Type, typeof(BoolType));
        }
        [TestMethod]
        public void ParsesComputedQuestions()
        {
            var src = @"form TestForm testQuestion: ""test me!"" bool(true)";
            var result = Build(src);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Statement, typeof(ComputedQuestion));
            var question = result.Statement as ComputedQuestion;
            Assert.AreEqual("testQuestion", question.Id);
            Assert.AreEqual("test me!", question.Text);
            Assert.IsInstanceOfType(question.Type, typeof(BoolType));
            Assert.IsInstanceOfType(question.Expression, typeof(LiteralBool));
            Assert.IsTrue((question.Expression as LiteralBool).Value);
        }

        [TestMethod]
        public void ParsesIfStatements()
        {
            var src = @"form TestForm
    if true {
        q1: """" string
        if true
            q2: """" string
        else
            if true
                q3: """" string
    }";
            var result = Build(src);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestExpressions()
        {
            dynamic expr = BuildExpr("a+b");
            Assert.IsInstanceOfType(expr as Expression, typeof(Add));
            Assert.AreEqual(expr.Left.Id, "a");
            Assert.AreEqual(expr.Right.Id, "b");

            expr = BuildExpr("(a||b&&c)");
            Assert.IsInstanceOfType(expr as Expression, typeof(LogicalOr));

            expr = BuildExpr("a>b>c");
            Assert.AreEqual(expr.Left.Left.Id, "a");
            Assert.AreEqual(expr.Right.Id, "c");

            expr = BuildExpr(@"a == ""b""");
            Assert.AreEqual(expr.Right.Value, "b");

            expr = BuildExpr(@"!a+b");
            Assert.IsInstanceOfType(expr as Expression, typeof(Add));
            Assert.IsInstanceOfType(expr.Left as Expression, typeof(Not));
        }
    }
}
