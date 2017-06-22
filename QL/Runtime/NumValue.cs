namespace QL.Runtime
{
    public class NumValue : Value
    {
        public NumValue(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; }
    }
}
