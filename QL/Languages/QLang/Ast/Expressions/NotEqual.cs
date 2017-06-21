namespace QL.Languages.QLang.Ast.Expressions
{
    public class NotEqual : Binary
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
