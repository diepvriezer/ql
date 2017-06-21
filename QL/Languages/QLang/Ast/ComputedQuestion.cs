using QL.Languages.QLang.Ast.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast
{
    public class ComputedQuestion : Question
    {
        public Expression Expression { get; set; }
    }
}
