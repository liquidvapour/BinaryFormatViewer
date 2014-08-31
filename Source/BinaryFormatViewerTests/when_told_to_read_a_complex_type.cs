using System;
using System.Linq;
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
        public void should_have_an_object_node_in_address_field()
        {
            var r = (ObjectNode)result;
            var addressField = r.Fields.First(x => x.Name == "<Address>k__BackingField");
            Assert.That(addressField.Value, Is.TypeOf<ObjectNode>());
        }
    }
}