namespace QL.Languages.QLang.Ast.Expressions
{
    public interface IExpressionVisitor<T>
    {
        T Visit(Add node);
        T Visit(Subtract node);
        T Visit(Divide node);
        T Visit(Multiply node);
        T Visit(GreaterThan node);
        T Visit(GreaterThanOrEqual node);
        T Visit(LessThan node);
        T Visit(LessThanOrEqual node);
        T Visit(LogicalAnd node);
        T Visit(LogicalOr node);
        T Visit(Minus node);
        T Visit(Not node);
        T Visit(Equal node);
        T Visit(NotEqual node);
        T Visit(QuestionReference node);
        T Visit(LiteralBool node);
        T Visit(LiteralString node);
        T Visit(LiteralNum node);
    }
}
