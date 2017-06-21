using QL.Languages.QLang.Ast.Expressions;
using QL.Languages.QLang.Ast.Statements;
using QL.Languages.QLang.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL.Languages.QLang.Ast
{
    public abstract class DefaultVisitor<T> : IFormVisitor<T>, IStatementVisitor<T>, IExpressionVisitor<T>, ITypeVisitor<T>
    {
        public virtual T Visit(Form node)
        {
            return node.Statement.Accept(this);
        }

        public virtual T Visit(Block node)
        {
            T last = default(T);
            foreach(var stat in node.Statements)
            {
                last = stat.Accept(this);
            }
            return last;
        }

        public virtual T Visit(ComputedQuestion node)
        {
            node.Expression.Accept(this);
            return node.Type.Accept(this);
        }

        public virtual T Visit(Question node)
        {
            return node.Type.Accept(this);
        }

        public virtual T Visit(IfThenElse node)
        {
            T last = node.Condition.Accept(this);
            if (node.Then != null)
                last = node.Then.Accept(this);
            if (node.Else != null)
                last = node.Else.Accept(this);
            return last;
        }

        public virtual T Visit(Add node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(Subtract node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(Divide node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(Multiply node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(GreaterThan node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(GreaterThanOrEqual node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(LessThan node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(LessThanOrEqual node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(LogicalAnd node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(LogicalOr node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(Minus node)
        {
            return VisitUnary(node);
        }

        public virtual T Visit(Not node)
        {
            return VisitUnary(node);
        }

        public virtual T Visit(Equal node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(NotEqual node)
        {
            return VisitBinary(node);
        }

        public virtual T Visit(QuestionReference node)
        {
            return default(T);
        }

        public virtual T Visit(LiteralBool node)
        {
            return default(T);
        }

        public virtual T Visit(LiteralString node)
        {
            return default(T);
        }

        public virtual T Visit(LiteralNum node)
        {
            return default(T);
        }

        public virtual T Visit(BoolType type)
        {
            return default(T);
        }

        public virtual T Visit(StringType type)
        {
            return default(T);
        }

        public virtual T Visit(NumType type)
        {
            return default(T);
        }

        private T VisitBinary(Binary node)
        {
            node.Left.Accept(this);
            return node.Right.Accept(this);
        }

        private T VisitUnary(Unary node)
        {
            return node.Expression.Accept(this);
        }
    }
}
