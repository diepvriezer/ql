using QL.Languages.QLang.Ast.Types;

namespace QL.UI.Widgets
{
    public class WidgetFactory : ITypeVisitor<IWidget>
    {
        public IWidget Visit(BoolType type)
        {
            return new CheckBoxWidget();
        }

        public IWidget Visit(StringType type)
        {
            return new TextFieldWidget();
        }

        public IWidget Visit(NumType type)
        {
            return new UpDownWidget();
        }
    }
}