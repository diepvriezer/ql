using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Ast.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QL.Languages.QLang.Ast.Expressions;

namespace QL.Traversals
{
    public class TypeChecker : DefaultVisitor<BaseType>, IValidationTraversal
    {
        public TypeChecker(IDictionary<string, BaseType> questionTypeMap)
        {
            _questionTypeMap = questionTypeMap;
            _printer = new PrintSource();
        }

        private readonly IDictionary<string, BaseType> _questionTypeMap;
        private readonly PrintSource _printer;

        private bool _contd = true;
        private string _lastExpr = null;

        public bool Continue()
        {
            if (_contd)
                Console.WriteLine("INFO\tType checker passed");

            return _contd;
        }

        public override BaseType Visit(QuestionReference node)
        {
            return _questionTypeMap[node.Id];
        }

        public override BaseType Visit(LiteralBool node)
        {
            return new BoolType();
        }

        public override BaseType Visit(LiteralNum node)
        {
            return new NumType();
        }

        public override BaseType Visit(LiteralString node)
        {
            return new StringType();
        }

        public override BaseType Visit(Add node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(Divide node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }
        
        public override BaseType Visit(Multiply node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new NumType();
        }

        public override BaseType Visit(Subtract node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new NumType();
        }
        
        public override BaseType Visit(Equal node)
        {
            StoreExpr(node);
            EnsureSameType(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }
        
        public override BaseType Visit(NotEqual node)
        {
            StoreExpr(node);
            EnsureSameType(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(GreaterThan node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(GreaterThanOrEqual node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(LessThan node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(LessThanOrEqual node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(LogicalAnd node)
        {
            StoreExpr(node);
            EnsureOfType<BoolType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(LogicalOr node)
        {
            StoreExpr(node);
            EnsureOfType<BoolType>(node.Left.Accept(this), node.Right.Accept(this));
            return new BoolType();
        }

        public override BaseType Visit(Minus node)
        {
            StoreExpr(node);
            EnsureOfType<NumType>(node.Expression.Accept(this));
            return new NumType();
        }

        public override BaseType Visit(Not node)
        {
            StoreExpr(node);
            EnsureOfType<BoolType>(node.Expression.Accept(this));
            return new BoolType();
        }

        private void StoreExpr(Expression expr)
        {
            _lastExpr = _printer.Visit((dynamic) expr);
        }

        private string PrintType(BaseType type)
        {
            return "\"" + _printer.Visit((dynamic)type) + "\"";
        }

        private void EnsureOfType<T>(params BaseType[] types)
            where T : BaseType
        {
            var misses = types.Where(t => !(t is T));
            if (misses.Any())
            {
                var printed = types.Select(PrintType);
                Console.WriteLine($"ABRT\tResolved top type(s) {string.Join(" and ", printed)} are not compatible in {_lastExpr}");
                _contd = false;
            }
        }

        private void EnsureSameType(params BaseType[] types)
        {
            var t0 = types[0];
            var t1 = types[1];
            var u = t0.GetType();
            var v = t1.GetType(); // :(
            if (!(u.IsAssignableFrom(v) || v.IsAssignableFrom(u)))
            {
                Console.WriteLine($"ABRT\tResolved top type(s) {PrintType(t0)} and {PrintType(t1)} are not compatible in {_lastExpr}");
                _contd = false;
            }
        }
    }
}
