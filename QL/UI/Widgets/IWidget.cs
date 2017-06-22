using QL.Runtime;
using System;

namespace QL.UI.Widgets
{
    public interface IWidget
    {
        void SetValue(Value value);
        Value GetValue();
        Action UpdateCallback { get; set; }
        bool CanReceiveValue { get; set; }
        bool Visible { get; set; }
    }
}
