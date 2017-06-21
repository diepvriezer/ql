namespace QL.Languages.QLang.Ast.Expressions
{
    public abstract class Binary : Expression
    {
        public Expression Left { get; set; }
        public Expression Right { get; set; }
    }
}
