using QL.Languages.QLang.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast
{
    public class IfStatement : Statement
    {
        public Expression Condition { get; set; }

        public Statement Then { get; set; }

        public Statement Else { get; set; }
    }
}
