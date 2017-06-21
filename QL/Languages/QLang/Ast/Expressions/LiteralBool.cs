using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast.Expressions
{
    public class LiteralBool : Literal
    {
        public bool Value { get; set; }
    }
}
