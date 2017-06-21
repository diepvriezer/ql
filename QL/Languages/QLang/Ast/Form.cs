using QL.Languages.QLang.Ast.Statements;

namespace QL.Languages.QLang.Ast
{
    public class Form : AstNode
    {
        public string Name { get; set; }
        public Statement Statement { get; set; }

        public virtual T Accept<T>(IFormVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }
    }
}