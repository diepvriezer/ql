namespace QL.Languages.QLang.Ast.Expressions
{
    public class Not : Unary
    {
        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
