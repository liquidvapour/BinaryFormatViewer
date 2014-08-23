using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_told_to_read_a_complex_type : BinarySerializedObjectSpec
    {
        protected override string GetFileName()
        {
            return "compex type.bin";
        }

        protected override object GetObjectToSerialize()
        {
            return new Customer
            {
                Name = "Jill",
                Age = 30,
                Address = new Address {Line1 = "45 Hat Road", PostCode = "SE2 8ID"}
            };
        }

        [Test]
        public void should_work()
        {
            Node r = result;
        }
    }

    [TestFixture]
    public class when_told_to_read_an_array_of_complex_type : BinarySerializedObjectSpec
    {
        protected override string GetFileName()
        {
            return "array of compex type.bin";
        }

        protected override object GetObjectToSerialize()
        {
            return new[]
            {
                new Customer
                {
                    Name = "Jill",
                    Age = 30,
                    Address = new Address {Line1 = "45 Hat Road", PostCode = "SE2 8ID"}
                },
                new Customer
                {
                    Name = "Bob",
                    Age = 28,
                    Address = new Address {Line1 = "20 Smith Street", PostCode = "H6 9UG"}
                }

            };
        }

        [Test]
        public void should_work()
        {
            Node r = result;
        }
    }
}