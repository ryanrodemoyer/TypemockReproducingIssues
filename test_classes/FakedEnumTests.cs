using System;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;

namespace ClassLibrary1.Tests
{
    public class ColorConverter
    {
        public Color ConvertToColor(string s)
        {
            bool result = Enum.TryParse<Color>(s, ignoreCase: true, result: out var color);
            if (result)
            {
                return color;
            }

            throw new ArgumentException("invalid color name");
        }
    }

    public enum Color
    {
        Red = 4,
        Green = 5,
        Blue = 6,
    }

    [TestFixture]
    public class ColorConverterTests
    {
        [Test]
        public void FakedInstance_MethodThatReturnsEnumReturnsNonDefaultValue()
        {
            var converter = Isolate.Fake.Instance<ColorConverter>();

            Color color = converter.ConvertToColor(null);

            Console.WriteLine($"color is {color}");

            Assert.AreEqual((int)default(Color), (int)color, "Value returned from the faked instance is non-standard.");
        }

        [Test]
        public void DefaultValueForEnumIs0()
        {
            Assert.AreEqual(0, (int)default(Color));
        }
    }
}
