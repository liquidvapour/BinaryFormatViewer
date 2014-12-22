using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_array_of_test_item : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            return new[] {new TestItem("Jill"), new TestItem("Chris")};
        }

        protected override string GetFileName()
        {
            return "array of two test items.bin";
        }

        [Test]
        public void should_contain_an_object_array_of_two_items()
        {
            var typedResult = (GenericArrayNode)result;
            Assert.That(typedResult.Children.Count, Is.EqualTo(2));
        }

        [Test]
        public void first_item_should_contain_jill()
        {
            var typedResult = (GenericArrayNode)result;
            var itemZero = (RuntimeObjectNode)typedResult.Children[0];
            Assert.That(itemZero.Children[0], Is.TypeOf<StringNode>());
            var stringItem = (StringNode) itemZero.Children[0];
            Assert.That(stringItem.Value, Is.EqualTo("Jill"));
        }

        [Test]
        public void second_item_should_contain_chris()
        {
            var typedResult = (GenericArrayNode)result;
            var itemOne = (RuntimeObjectNode)typedResult.Children[1];
            Assert.That(itemOne.Children[0], Is.TypeOf<StringNode>());
            var stringItem = (StringNode)itemOne.Children[0];
            Assert.That(stringItem.Value, Is.EqualTo("Chris"));
        }
    }
}