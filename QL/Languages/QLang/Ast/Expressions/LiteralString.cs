namespace QL.Languages.QLang.Ast.Expressions
{
    public class LiteralString : Literal
    {
        public string Value { get; set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
