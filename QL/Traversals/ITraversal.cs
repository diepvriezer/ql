using QL.Languages.QLang.Ast;

namespace QL.Traversals
{
    public interface IValidationTraversal
    {
        bool Continue();
    }
}
