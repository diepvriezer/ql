using System;
using System.Windows.Forms;
using QL.Runtime;

namespace QL.UI.Widgets
{
    public class UpDownWidget : NumericUpDown, IWidget
    {
        public UpDownWidget()
        {
            DecimalPlaces = 2;
        }

        public bool CanReceiveValue { get => ReadOnly; set => ReadOnly = value; }

        public Value GetValue()
        {
            return new NumValue(Value);
        }
        
        public void SetValue(Value value)
        {
            if (value is NumValue)
            {
                Value = ((NumValue)value).Value;
            }
            else
            {
                Value = 0;
            }
        }
    }
}
