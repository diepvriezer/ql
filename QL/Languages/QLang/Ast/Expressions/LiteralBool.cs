namespace QL.Languages.QLang.Ast.Expressions
{
    public class LiteralBool : Literal
    {
        public bool Value { get; set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
