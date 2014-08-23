using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_TestItem : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            return new TestItem("Smith");
        }

        protected override string GetFileName()
        {
            return "lone TestItem.bin";
        }

        [Test]
        public void should_have_name_of_TestItem()
        {
            var node = (RuntimeObjectNode) result;
            Assert.AreEqual("SerializationSpike.TestItem", node.Name);
        }
    }

    [TestFixture]
    public class when_reading_a_serialized_array_of_TestItem : BinarySerializedObjectSpec
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
            Assert.That(typedResult.Values.Count, Is.EqualTo(2));
        }

        [Test]
        public void first_item_should_contain_jill()
        {
            var typedResult = (GenericArrayNode)result;
            //Assert.That(typedResult.Values.Count, Is.EqualTo(2));
            var itemZero = (RuntimeObjectNode)typedResult.Values[0];
            Assert.That(itemZero.Values[0], Is.TypeOf<StringNode>());
            var stringItem = (StringNode) itemZero.Values[0];
            Assert.That(stringItem.Value, Is.EqualTo("Jill"));
        }

        [Test]
        public void second_item_should_contain_chris()
        {
            var typedResult = (GenericArrayNode)result;
            //Assert.That(typedResult.Values.Count, Is.EqualTo(2));
            var itemOne = (RuntimeObjectNode)typedResult.Values[1];
            Assert.That(itemOne.Values[0], Is.TypeOf<StringNode>());
            var stringItem = (StringNode)itemOne.Values[0];
            Assert.That(stringItem.Value, Is.EqualTo("Chris"));
        }
    }
}