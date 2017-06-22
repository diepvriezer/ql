namespace QL.Runtime
{
    public class BoolValue : Value
    {
        public BoolValue(bool value)
        {
            Value = value;
        }

        public bool Value { get; }
    }
}
