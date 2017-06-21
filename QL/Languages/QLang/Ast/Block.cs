using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast
{
    public class Block : Statement
    {
        public IList<Statement> Statements { get; set; } = new List<Statement>();
    }
}
