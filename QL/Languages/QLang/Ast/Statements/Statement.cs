namespace QL.Languages.QLang.Ast.Statements
{
    public abstract class Statement : AstNode
    {
        public abstract T Accept<T>(IStatementVisitor<T> visitor);
    }
}
