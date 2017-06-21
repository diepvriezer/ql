using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Grammar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using QL.Languages.QLang.Ast.Types;
using QL.Languages.QLang.Ast.Expressions;

namespace QL.Languages.QLang
{
    public class AstBuilderVisitor : QLBaseVisitor<AstNode>
    {
        public override AstNode VisitForm([NotNull] QLParser.FormContext context)
        {
            var form = new Form
            {
                Name = context.ID().GetText(),
                Statement = context.statement().Accept(this) as Statement
            };

            return form;
        }

        #region Statements

        public override AstNode VisitBlock([NotNull] QLParser.BlockContext context)
        {
            var block = new Block();
            block.Statements = context
                .statement()
                .Select(child => child.Accept(this))
                .Cast<Statement>()
                .ToList();

            return block;
        }

        public override AstNode VisitQuestion([NotNull] QLParser.QuestionContext context)
        {
            var question = new Question
            {
                Id = context.ID().GetText(),
                Text = StripQuotes(context.QUOTED_STRING().GetText()),
                Type = context.type().Accept(this) as BaseType
            };
            return question;
        }

        public override AstNode VisitComputedQuestion([NotNull] QLParser.ComputedQuestionContext context)
        {
            var question = new ComputedQuestion
            {
                Id = context.ID().GetText(),
                Text = StripQuotes(context.QUOTED_STRING().GetText()),
                Type = context.type().Accept(this) as BaseType,
                Expression = context.expr().Accept(this) as Expression
            };
            return question;
        }

        public override AstNode VisitIfStatement([NotNull] QLParser.IfStatementContext context)
        {
            var ifs = context
                .conditionBlock()
                .Select(child => child.Accept(this))
                .Cast<IfStatement>()
                .ToList();

            var root = ifs.First();
            var last = root;
            foreach(var other in ifs.Skip(1))
            {
                last.Else = other;
                last = other;
            }

            var finalElse = context.statement();
            if (finalElse != null)
                last.Else = finalElse.Accept(this) as Statement;

            return root;
        }

        public override AstNode VisitConditionBlock([NotNull] QLParser.ConditionBlockContext context)
        {
            var ifStatement = new IfStatement
            {
                Condition = context.expr().Accept(this) as Expression,
                Then = context.statement().Accept(this) as Statement
            };
            return ifStatement;
        }

        #endregion

        #region Expression language

        public override AstNode VisitUnary([NotNull] QLParser.UnaryContext context)
        {
            var map = new Dictionary<string, Func<Unary>>()
            {
                ["!"] = () => new Not(),
                ["-"] = () => new Minus()
            };

            var op = context.op.Text;
            var expr = map[op].Invoke();

            expr.Expression = context.expr().Accept(this) as Expression;
            return expr;
        }

        public override AstNode VisitBinary([NotNull] QLParser.BinaryContext context)
        {
            var map = new Dictionary<string, Func<Binary>>()
            {
                ["*"] = () => new Multiply(),
                ["/"] = () => new Divide(),
                ["+"] = () => new Add(),
                ["-"] = () => new Subtract(),
                [">="] = () => new GreaterThanOrEqual(),
                [">"] = () => new GreaterThan(),
                ["<"] = () => new LessThan(),
                ["<="] = () => new LessThanOrEqual(),
                ["=="] = () => new Equal(),
                ["!="] = () => new NotEqual(),
                ["&&"] = () => new LogicalAnd(),
                ["||"] = () => new LogicalOr()
            };

            var op = context.op.Text;
            var expr = map[op].Invoke();

            expr.Left = context.expr(0).Accept(this) as Expression;
            expr.Right = context.expr(1).Accept(this) as Expression;
            return expr;
        }

        public override AstNode VisitQuestionReference([NotNull] QLParser.QuestionReferenceContext context)
        {
            var questionRef = new QuestionReference
            {
                Id = context.ID().GetText()
            };
            return questionRef;
        }

        public override AstNode VisitStringConst([NotNull] QLParser.StringConstContext context)
        {
            return new LiteralString
            {
                Value = StripQuotes(context.QUOTED_STRING().GetText())
            };
        }

        public override AstNode VisitNumConst([NotNull] QLParser.NumConstContext context)
        {
            return new LiteralNum
            {
                Value = decimal.Parse(context.NUMBER().GetText())
            };
        }

        public override AstNode VisitBoolConst([NotNull] QLParser.BoolConstContext context)
        {
            return new LiteralBool
            {
                Value = context.TRUE() != null
            };
        }

        #endregion

        #region Types

        public override AstNode VisitBoolType([NotNull] QLParser.BoolTypeContext context)
        {
            return new BoolType();
        }

        public override AstNode VisitStringType([NotNull] QLParser.StringTypeContext context)
        {
            return new StringType();
        }

        public override AstNode VisitNumType([NotNull] QLParser.NumTypeContext context)
        {
            return new NumType();
        }

        #endregion

        private string StripQuotes(string raw)
        {
            return raw.Substring(1, raw.Length - 2);
        }
    }
}
