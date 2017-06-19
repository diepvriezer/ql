using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Lang.QL.Ast.Expressions
{
    public class LiteralString : Literal
    {
        public string Value { get; set; }
    }
}
