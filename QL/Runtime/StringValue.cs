namespace QL.Runtime
{
    public class StringValue : Value
    {
        public StringValue(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
