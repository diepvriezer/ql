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
                Console.WriteLine($"WARN\tDuplicate questions ({group.Count()} for ID {group.Key}");
                var types = group.Select(q => q.Type.GetType()).ToList();
                var t = types[0];
                foreach(var u in types.Skip(1))
                {
                    if (!(t.IsAssignableFrom(u) || u.IsAssignableFrom(t)))
                    {
                        Console.WriteLine("ABRT\tThese questions do not share the same type!");
                        contd = false;
                        break;
                    }
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
