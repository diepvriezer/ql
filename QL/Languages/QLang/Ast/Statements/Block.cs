using System.Collections.Generic;

namespace QL.Languages.QLang.Ast.Statements
{
    public class Block : Statement
    {
        public IList<Statement> Statements { get; set; } = new List<Statement>();

        public override T Accept<T>(IStatementVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
