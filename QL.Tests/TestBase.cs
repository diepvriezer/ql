using QL.Languages.QLang;
using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Tests
{
    public class TestBase
    {
        protected Form Build(string source)
        {
            var builder = new AstBuilder();
            return builder.BuildFromString(source) as Form;
        }

        protected Expression BuildExpr(string expr)
        {
            var source = @"form TestForm if " + expr + @" q1: ""test"" bool";
            var builder = new AstBuilder();
            dynamic result = builder.BuildFromString(source) as Form;
            return (result.Statement.Condition) as Expression;
        }
    }
}
