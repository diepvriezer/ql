namespace QL.Languages.QLang.Ast.Expressions
{
    public abstract class Unary : Expression
    {
        public Expression Expression { get; set; }
    }
}
