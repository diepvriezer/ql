using System.Windows.Forms;
using QL.Runtime;

namespace QL.UI.Widgets
{
    public class CheckBoxWidget : CheckBox, IWidget
    {
        public bool CanReceiveValue { get => !Enabled; set => Enabled = !value; }

        public Value GetValue()
        {
            return new BoolValue(Checked);
        }

        public void SetValue(Value value)
        {
            Checked = value is BoolValue boolValue && boolValue.Value;
        }
    }
}
