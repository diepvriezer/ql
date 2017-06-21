using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast.Expressions
{
    public abstract class Binary : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}
