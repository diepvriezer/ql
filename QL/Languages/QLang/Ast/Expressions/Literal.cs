using QL.Languages.QLang.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast.Expressions
{
    public abstract class Literal : Expression
    {
        public BaseType Type { get; }
    }
}