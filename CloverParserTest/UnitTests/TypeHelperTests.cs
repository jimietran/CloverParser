using CloverParser.Helpers;
using CloverParser.Models;

namespace CloverParserTest.UnitTests
{
    public class TypeHelperTests
    {
        private ITypeHelper _typeHelper;

        [SetUp]
        public void Setup()
        {
            _typeHelper = new TypeHelper();
        }

        [Test]
        public void TestConvertValue_UsingInt_ReturnsCorrectly()
        {
            var integerValue = _typeHelper.ConvertValue(DataType.INTEGER, "-11");

            Assert.That(integerValue, Is.TypeOf<int>());
            Assert.That(integerValue, Is.EqualTo(-11));
        }

        [Test]
        public void TestConvertValue_UsingInvalidIntValue_ReturnsZero()
        {
            var integerValue = _typeHelper.ConvertValue(DataType.INTEGER, "-11ab");

            Assert.That(integerValue, Is.TypeOf<int>());
            Assert.That(integerValue, Is.EqualTo(0));
        }
    }
}
