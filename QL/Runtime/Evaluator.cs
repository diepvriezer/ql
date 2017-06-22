using QL.Languages.QLang.Ast;
using QL.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QL.Languages.QLang.Ast.Statements;
using QL.Languages.QLang.Ast.Expressions;

namespace QL.Runtime
{
    public class Evaluator : DefaultVisitor<Value>
    {
        public Evaluator(IList<UIBinding> bindings, IList<ComputedQuestion> computedQuestions)
        {
            Bindings = bindings;
            Lookup = bindings
                .ToDictionary(b => b.QuestionId);
            QuestionLookup = bindings
                .Where(b => !b.Control.CanReceiveValue)
                .ToDictionary(b => b.QuestionId);
            ComputedLookup = computedQuestions
                .ToDictionary(q => q.Id);
        }

        protected readonly IList<UIBinding> Bindings;
        protected readonly IDictionary<string, UIBinding> Lookup;
        protected readonly IDictionary<string, UIBinding> QuestionLookup;
        protected readonly IDictionary<string, ComputedQuestion> ComputedLookup;

        private bool HideFutureQuestions = false;

        public override Value Visit(IfThenElse node)
        {
            var result = node.Condition.Accept(this);

            // Update values but hide them if we're in some other else block.
            if (!HideFutureQuestions)
            {
                node.Then.Accept(this);
                if (node.Else != null)
                    node.Else.Accept(this);
            }
            else
            {
                // On positive result, enable then and disable else.
                if (result is BoolValue boolValue && boolValue.Value)
                {
                    node.Then.Accept(this);
                    if (node.Else != null)
                    {
                        HideFutureQuestions = true;
                        node.Else.Accept(this);
                        HideFutureQuestions = false;
                    }
                }
                else
                {
                    HideFutureQuestions = true;
                    node.Then.Accept(this);
                    HideFutureQuestions = false;

                    if (node.Else != null)
                        node.Else.Accept(this);
                }
            }

            return new UndefinedValue();
        }

        public override Value Visit(Question node)
        {
            // Check if hiding.
            Lookup[node.Id].Visible = !HideFutureQuestions;
            return new UndefinedValue();
        }

        public override Value Visit(ComputedQuestion node)
        {
            var question = Lookup[node.Id];

            // Evaluate.
            var result = node.Expression.Accept(this);
            question.Control.SetValue(result);

            // Set hidden, also if undefined.
            question.Visible = !(HideFutureQuestions || result is UndefinedValue);

            return new UndefinedValue();
        }


        public override Value Visit(QuestionReference node)
        {
            // If question is computed, evaluate and return. Else return UI value.
            if (ComputedLookup.ContainsKey(node.Id))
            {
                var result = ComputedLookup[node.Id].Expression.Accept(this);
                return result;
            }
            else
            {
                var question = QuestionLookup[node.Id];
                return question.Control.GetValue();
            }
        }

        // Expression evaluation.
        public override Value Visit(LiteralBool node) => new BoolValue(node.Value);
        public override Value Visit(LiteralNum node) => new NumValue(node.Value);
        public override Value Visit(LiteralString node) => new StringValue(node.Value);

        // Math.
        public override Value Visit(Add node) => Calculation(node, (x, y) => x + y);
        public override Value Visit(Subtract node) => Calculation(node, (x, y) => x - y);
        public override Value Visit(Divide node) => Calculation(node, (x, y) => x / y);
        public override Value Visit(Multiply node) => Calculation(node, (x, y) => x * y);

        // Comparisions.
        public override Value Visit(Equal node) => Comparision(node, (x, y) => x == y);
        public override Value Visit(NotEqual node) => Comparision(node, (x, y) => x != y);
        public override Value Visit(GreaterThan node) => Comparision(node, (x, y) => x.CompareTo(y) > 0);
        public override Value Visit(GreaterThanOrEqual node) => Comparision(node, (x, y) => x.CompareTo(y) >= 0);
        public override Value Visit(LessThan node) => Comparision(node, (x, y) => x.CompareTo(y) < 0);
        public override Value Visit(LessThanOrEqual node) => Comparision(node, (x, y) => x.CompareTo(y) <= 0);

        // Logical.
        public override Value Visit(LogicalAnd node) => Logical(node, (x, y) => x && y);
        public override Value Visit(LogicalOr node) => Logical(node, (x, y) => x || y);

        // Various unary.
        public override Value Visit(Not node)
        {
            var result = node.Expression.Accept(this);
            if (result is BoolValue)
                return new BoolValue(!(result as BoolValue).Value);
            return new UndefinedValue();
        }
        public override Value Visit(Minus node)
        {
            var result = node.Expression.Accept(this);
            if (result is NumValue)
                return new NumValue(-1 * (result as NumValue).Value);
            return new UndefinedValue();
        }

        private Value Calculation(Binary node, Func<decimal, decimal, decimal> f)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);

            if (left is NumValue && right is NumValue)
                return new NumValue(f((left as NumValue).Value, (right as NumValue).Value));

            return new UndefinedValue();
        }

        private Value Comparision(Binary node, Func<IComparable, IComparable, bool> f)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);

            if (left is NumValue && right is NumValue)
                return new BoolValue(f((left as NumValue).Value, (right as NumValue).Value));

            if (left is BoolValue && right is BoolValue)
                return new BoolValue(f((left as BoolValue).Value, (right as BoolValue).Value));

            if (left is StringValue && right is StringValue)
                return new BoolValue(f((left as StringValue).Value, (right as StringValue).Value));

            return new UndefinedValue();
        }

        private Value Logical(Binary node, Func<bool, bool, bool> f)
        {
            var left = node.Left.Accept(this);
            var right = node.Right.Accept(this);

            if (left is BoolValue && right is BoolValue)
                return new BoolValue(f((left as BoolValue).Value, (right as BoolValue).Value));

            return new UndefinedValue();
        }
    }
}
