namespace QL.Languages.QLang.Ast.Expressions
{
    public abstract class Expression : AstNode
    {
        public abstract T Accept<T>(IExpressionVisitor<T> visitor);
    }
}
