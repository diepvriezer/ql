using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Ast.Statements;
using QL.Languages.QLang.Ast.Types;
using QL.Languages.QLang.Ast.Expressions;

namespace QL.Traversals
{
    public class PrintSource : DefaultVisitor<string>
    {
        public override string Visit(Form node)
        {
            return $"form {node.Name} {node.Statement.Accept(this)}";
        }
        public override string Visit(Block node)
        {
            var s = "{\n";
            foreach(var stat in node.Statements)
            {
                var res = stat.Accept(this);
                s += "\t" + res;
            }
            s += "}";
            return s;
        }

        public override string Visit(Question node)
        {
            var fmt = $"{node.Id}: \"{node.Text}\" {node.Type.Accept(this)}\n";
            return fmt;
        }

        public override string Visit(ComputedQuestion node)
        {
            var fmt = $"{node.Id}: \"{node.Text}\" {node.Type.Accept(this)}({node.Expression.Accept(this)})\n";
            return fmt;
        }

        public override string Visit(IfThenElse node)
        {
            var fmt = $"if {node.Condition.Accept(this)} {node.Then.Accept(this)}";
            if (node.Else != null)
                fmt += $"\nelse\n{node.Else.Accept(this)}";
            return fmt;
        }

        public override string Visit(BoolType type)
        {
            return "boolean";
        }

        public override string Visit(NumType type)
        {
            return "num";
        }

        public override string Visit(StringType type)
        {
            return "string";
        }

        public override string Visit(QuestionReference node)
        {
            return node.Id;
        }

        public override string Visit(LiteralBool node)
        {
            return node.Value.ToString();
        }

        public override string Visit(LiteralNum node)
        {
            return node.Value.ToString();
        }

        public override string Visit(LiteralString node)
        {
            return $"\"{node.Value}\"";
        }

        public override string Visit(Add node)
        {
            return VisitBinary("+", node);
        }

        public override string Visit(Divide node)
        {
            return VisitBinary("/", node);
        }

        public override string Visit(Equal node)
        {
            return VisitBinary("==", node);
        }

        public override string Visit(GreaterThan node)
        {
            return VisitBinary(">", node);
        }

        public override string Visit(GreaterThanOrEqual node)
        {
            return VisitBinary(">=", node);
        }

        public override string Visit(LessThan node)
        {
            return VisitBinary("<", node);
        }

        public override string Visit(LessThanOrEqual node)
        {
            return VisitBinary("<=", node);
        }

        public override string Visit(LogicalAnd node)
        {
            return VisitBinary("&&", node);
        }

        public override string Visit(LogicalOr node)
        {
            return VisitBinary("||", node);
        }

        public override string Visit(Multiply node)
        {
            return VisitBinary("*", node);
        }

        public override string Visit(NotEqual node)
        {
            return VisitBinary("!=", node);
        }

        public override string Visit(Subtract node)
        {
            return VisitBinary("-", node);
        }

        public override string Visit(Minus node)
        {
            return VisitUnary("-", node);
        }

        public override string Visit(Not node)
        {
            return VisitUnary("!", node);
        }

        private string VisitBinary(string op, Binary node)
        {
            var fmt = $"({node.Left.Accept(this)} {op} {node.Right.Accept(this)})";
            return fmt;
        }

        private string VisitUnary(string op, Unary node)
        {
            var fmt = $"{op}{node.Expression.Accept(this)})";
            return fmt;
        }        
    }
}
