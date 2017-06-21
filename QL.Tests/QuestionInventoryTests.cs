using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QL.Traversals;
using System.Linq;

namespace QL.Tests
{
    [TestClass]
    public class QuestionInventoryTests : TestBase
    {
        [TestMethod]
        public void TestQuestionInventory()
        {
            var src = @"form tForm {
                q1: ""q1"" bool
                q2: ""q2"" bool
                if (q1 || q2 || qx)
                    q3: ""q3_then"" bool
                else
                    q3: ""q3_else"" bool
}";
            var form = Build(src);
            var result = new QuestionInventory().Visit(form);
            CollectionAssert.AreEqual(new[] { "q1", "q2", "q3", "q3" }, result.Questions.Select(q => q.Id).ToArray());
            CollectionAssert.AreEqual(new[] { "q1", "q2", "qx" }, result.References.Select(r => r.Id).ToArray());
        }
    }
}
