using QL.Languages.QLang.Ast.Types;

namespace QL.Languages.QLang.Ast.Statements
{
    public class Question : Statement
    {
        public BaseType Type { get; set; }

        public string Text { get; set; }

        public string Id { get; set; }

        public override T Accept<T>(IStatementVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
