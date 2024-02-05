using CloverParser.Models;

namespace CloverParser.Services
{
    public interface ISpecProcessor
    {
        List<SpecModel> Process(string filePath);
    }
}