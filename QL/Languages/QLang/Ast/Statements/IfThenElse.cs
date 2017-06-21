using QL.Languages.QLang.Ast.Expressions;
namespace QL.Languages.QLang.Ast.Statements
{
    public class IfThenElse : Statement
    {
        public Expression Condition { get; set; }

        public Statement Then { get; set; }

        public Statement Else { get; set; }

        public override T Accept<T>(IStatementVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
