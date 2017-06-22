using QL.Languages.QLang.Ast;
using System.Collections.Generic;
using QL.Languages.QLang.Ast.Statements;
using QL.UI.Widgets;
using QL.Languages.QLang.Ast.Types;
using System.Windows.Forms;
using System.Linq;

namespace QL.UI
{
    public class CreateBindings : DefaultVisitor<IList<UIBinding>>
    {
        public CreateBindings(ITypeVisitor<IWidget> widgetFactory)
        {
            Bindings = new List<UIBinding>();
            WidgetFactory = widgetFactory;
        }

        protected readonly ITypeVisitor<IWidget> WidgetFactory;
        protected readonly IList<UIBinding> Bindings;

        public override IList<UIBinding> Visit(ComputedQuestion node)
        {
            if (IsUnique(node))
            {
                var binding = new UIBinding
                {
                    QuestionId = node.Id,
                    Label = LabelForQuestion(node),
                    Control = WidgetFactory.Visit((dynamic)node.Type)
                };
                binding.Control.CanReceiveValue = true;
                Bindings.Add(binding);
            }
            return Bindings;
        }

        public override IList<UIBinding> Visit(Question node)
        {
            if (IsUnique(node))
            {
                var binding = new UIBinding
                {
                    QuestionId = node.Id,
                    Label = LabelForQuestion(node),
                    Control = WidgetFactory.Visit((dynamic)node.Type)
                };
                binding.Control.CanReceiveValue = false;
                Bindings.Add(binding);
            }
            return Bindings;
        }

        protected Label LabelForQuestion(Question q)
        {
            return new StyledLabel(q.Text);
        }

        protected bool IsUnique(Question q)
        {
            return !Bindings.Any(b => b.QuestionId == q.Id);
        }
    }
}
