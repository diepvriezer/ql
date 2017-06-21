using Antlr4.Runtime;
using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Grammar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang
{
    public class AstBuilder
    {
        public IEnumerable<string> Messages { get; }

        public Form BuildFromPath(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return Build(new AntlrInputStream(reader));
            }
        }
        public Form BuildFromString(string raw)
        {
            return Build(new AntlrInputStream(raw));
        }

        private Form Build(AntlrInputStream input)
        {
            var lexer = new QLLexer(input);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new QLParser(tokenStream);

            var visitor = new AstBuilderVisitor();
            var root = parser.form().Accept(visitor);

            return root as Form;
        }
    }
}
