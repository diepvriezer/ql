namespace QL.Languages.QLang.Ast.Expressions
{
    public class LiteralNum : Literal
    {
        public decimal Value { get; set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
