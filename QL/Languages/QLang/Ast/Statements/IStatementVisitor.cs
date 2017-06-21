namespace QL.Languages.QLang.Ast.Statements
{
    public interface IStatementVisitor<T>
    {
        T Visit(Block node);
        T Visit(ComputedQuestion node);
        T Visit(Question node);
        T Visit(IfThenElse node);
    }
}
