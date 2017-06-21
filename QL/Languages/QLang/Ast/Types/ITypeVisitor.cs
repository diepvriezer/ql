namespace QL.Languages.QLang.Ast.Types
{
    public interface ITypeVisitor<T>
    {
        T Visit(BoolType type);
        T Visit(StringType type);
        T Visit(NumType type);
    }
}
