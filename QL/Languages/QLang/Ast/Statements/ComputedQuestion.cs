using QL.Languages.QLang.Ast.Expressions;

namespace QL.Languages.QLang.Ast.Statements
{
    public class ComputedQuestion : Question
    {
        public Expression Expression { get; set; }

        public override T Accept<T>(IStatementVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
