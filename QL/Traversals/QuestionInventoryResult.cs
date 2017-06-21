using QL.Languages.QLang.Ast.Expressions;
using QL.Languages.QLang.Ast.Statements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QL.Traversals
{
    public class QuestionInventoryResult
    {
        public IList<Question> Questions { get; } = new List<Question>();
        public IList<QuestionReference> References { get; } = new List<QuestionReference>();

        public IEnumerable<IGrouping<string, Question>> DuplicateIds
        {
            get { return Questions.GroupBy(q => q.Id).Where(g => g.Count() > 1); }
        }

        public IEnumerable<string> ReferencesNotFound
        {
            get { return References.Where(r => !Questions.Select(q => q.Id).Contains(r.Id)).Select(r => r.Id); }
        }
    }
}
