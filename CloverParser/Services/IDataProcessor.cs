using CloverParser.Models;

namespace CloverParser.Services
{
    public interface IDataProcessor
    {
        List<Dictionary<string, object>> Process(string filePath, List<SpecModel> specs);
    }
}