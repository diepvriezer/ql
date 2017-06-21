namespace QL.Languages.QLang.Ast
{
    public interface IFormVisitor<T>
    {
        T Visit(Form node);
    }
}
