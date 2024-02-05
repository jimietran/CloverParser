using CloverParser.Models;

namespace CloverParser.Helpers
{
    public class TypeHelper : ITypeHelper
    {
        public object ConvertValue(DataType dataType, string value)
        {
            switch (dataType)
            {
                case DataType.TEXT:
                    return value;
                case DataType.BOOLEAN:
                    return value.Equals("1") || value.ToLower().Equals("true");
                case DataType.INTEGER:
                    bool successful = int.TryParse(value, out int val);
                    if (!successful)
                        Console.WriteLine($"{value} is not a valid integer.");
                    return val;
                default:
                    throw new NotImplementedException();

            }
        }
    }
}
