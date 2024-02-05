using CloverParser.Models;

namespace CloverParser.Helpers
{
    public interface ITypeHelper
    {
        object ConvertValue(DataType dataType, string value);
    }
}