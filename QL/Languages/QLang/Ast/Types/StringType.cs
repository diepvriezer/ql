namespace QL.Languages.QLang.Ast.Types
{
    public class StringType : BaseType
    {
        public override T Accept<T>(ITypeVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
