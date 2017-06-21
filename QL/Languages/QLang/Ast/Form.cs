using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast
{
    public class Form : AstNode
    {
        public string Name { get; set; }
        public Statement Statement { get; set; }
    }
}