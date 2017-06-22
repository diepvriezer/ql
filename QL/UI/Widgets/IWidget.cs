using QL.Runtime;

namespace QL.UI.Widgets
{
    public interface IWidget
    {
        void SetValue(Value value);
        Value GetValue();
        bool CanReceiveValue { get; set; }
        bool Visible { get; set; }
    }
}
