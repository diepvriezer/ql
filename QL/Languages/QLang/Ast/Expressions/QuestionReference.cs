namespace QL.Languages.QLang.Ast.Expressions
{
    public class QuestionReference : Expression
    {
        public string Id { get; set; }

        public override T Accept<T>(IExpressionVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
