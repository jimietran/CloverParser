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
                    if (!int.TryParse(value, out int val))
                        Console.WriteLine($"{value} is not a valid integer.");
                    return val;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
