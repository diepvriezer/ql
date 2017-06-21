namespace QL.Languages.QLang.Ast.Types
{
    public abstract class BaseType : AstNode
    {
        public abstract T Accept<T>(ITypeVisitor<T> visitor);
    }
}
