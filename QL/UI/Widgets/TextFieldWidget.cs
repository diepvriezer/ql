using System.Windows.Forms;
using QL.Runtime;

namespace QL.UI.Widgets
{
    public class TextFieldWidget : TextBox, IWidget
    {
        public bool CanReceiveValue { get => ReadOnly; set => ReadOnly = value; }

        public Value GetValue()
        {
            return new StringValue(Text);
        }

        public void SetValue(Value value)
        {
            if (value is StringValue)
            {
                Text = ((StringValue)value).Value;
            }
            else
            {
                Text = "";
            }
        }
    }
}
