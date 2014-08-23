using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_a_serialized_array_is_read : BinarySerializedObjectSpec
    {
        protected TestItem[] TestArray = {new TestItem("Bob"), new TestItem("Janet")};

        protected override object GetObjectToSerialize()
        {
            return TestArray;
        }

        protected override string GetFileName()
        {
            return "single_dimension_array.bin";
        }

        [Test]
        public void object_node_at_index_0_should_be_a_string()
        {
            var node = (ObjectNode) ((GenericArrayNode) result).Values[0];
            Assert.That(node.Values[0], Is.TypeOf(typeof (StringNode)));
        }

        [Test]
        public void object_node_at_index_0_should_be_type_test_item()
        {
            var node = (ObjectNode) ((GenericArrayNode) result).Values[0];
            Assert.That(node.Name, Is.StringContaining("TestItem"));
        }

        [Test]
        public void returned_generic_array_node_should_have_2_values()
        {
            Assert.That(((GenericArrayNode) result).Values.Count, Is.EqualTo(2));
        }

        [Test]
        public void should_return_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof (GenericArrayNode)));
        }

        [Test]
        public void value_at_index_0_should_be_an_object_node_for_test_item()
        {
            Assert.That(((GenericArrayNode) result).Values[0], Is.TypeOf(typeof (ObjectNode)));
        }
    }
}