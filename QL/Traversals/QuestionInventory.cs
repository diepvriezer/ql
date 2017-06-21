using QL.Languages.QLang.Ast;
using QL.Languages.QLang.Ast.Statements;
using QL.Languages.QLang.Ast.Expressions;
using System;
using System.Linq;

namespace QL.Traversals
{
    public class QuestionInventory : DefaultVisitor<QuestionInventoryResult>, IValidationTraversal
    {
        private QuestionInventoryResult _result = new QuestionInventoryResult();

        public bool Continue()
        {
            bool contd = true;

            foreach (var group in _result.DuplicateIds)
            {
                var dupeList = string.Join(", ", group.Select(q => q.Id));
                Console.WriteLine($"WARN\tDuplicate questions by ID for {dupeList}");
                if (group.Select(q => q.Text).Distinct().Count() > 1)
                {
                    Console.WriteLine("ABRT\tQuestions do not share label");
                    contd = false;
                }
            }

            var notFound = _result.ReferencesNotFound.ToList();
            if (notFound.Any())
            {
                Console.WriteLine($"ABRT\tNon existant references to question ID's for {string.Join(", ", notFound)}");
                contd = false;
            }

            if (contd)
                Console.WriteLine("INFO\tBasic question lookup passed");

            return contd;
        }

        public override QuestionInventoryResult Visit(Question node)
        {
            _result.Questions.Add(node);
            return _result;
        }

        public override QuestionInventoryResult Visit(ComputedQuestion node)
        {
            _result.Questions.Add(node);
            node.Expression.Accept(this);
            return _result;
        }

        public override QuestionInventoryResult Visit(QuestionReference node)
        {
            _result.References.Add(node);
            return _result;
        }
    }
}
