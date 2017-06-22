using System.Windows.Forms;
using QL.Runtime;
using System;

namespace QL.UI.Widgets
{
    public class TextFieldWidget : TextBox, IWidget
    {
        public TextFieldWidget()
        {
            TextChanged += (o, e) => UpdateCallback();
        }

        public bool CanReceiveValue { get => ReadOnly; set => ReadOnly = value; }
        public Action UpdateCallback { get; set; }

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
