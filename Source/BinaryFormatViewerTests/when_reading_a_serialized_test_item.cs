using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_test_item : BinarySerializedObjectSpec
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
}