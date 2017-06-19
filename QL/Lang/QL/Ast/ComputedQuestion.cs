using QL.Lang.QL.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Lang.QL.Ast
{
    public class ComputedQuestion : Question
    {
        public Expression Expression { get; set; }
    }
}
