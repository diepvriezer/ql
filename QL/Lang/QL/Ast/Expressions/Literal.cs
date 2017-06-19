using QL.Lang.QL.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Lang.QL.Ast.Expressions
{
    public abstract class Literal
    {
        public BaseType Type { get; }
    }
}