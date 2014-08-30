using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_jagged_array_with_2_dimensions : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            var testArray = new TestItem[2][];
            testArray[0] = new TestItem[2];
            testArray[1] = new TestItem[2];
            testArray[0][0] = new TestItem("Bob");
            testArray[0][1] = new TestItem("Jill");
            testArray[1][0] = new TestItem("John");
            testArray[1][1] = new TestItem("Fran");
            return testArray;
        }

        protected override string GetFileName()
        {
            return "jagged_array_2_by_2.bin";
        }


        [Test]
        [Ignore("This will fail as there is a bug in the jaggered array reading. Unignore and fix!")]
        public void should_contain_values_for_each_item()
        {
            var node = (GenericArrayNode) result;

            Assert.That(node.Values.Count, Is.EqualTo(2));

            Assert.That(node.Values[0], Is.TypeOf<GenericArrayNode>());
            Assert.That(node.Values[1], Is.TypeOf<GenericArrayNode>());
            var val0 = (GenericArrayNode) node.Values[0];
            Assert.That(val0.Values[0], Is.TypeOf<ObjectNode>());
            var obj0 = (ObjectNode) val0.Values[0];
            Assert.That(obj0.Values[0], Is.TypeOf<StringNode>());
            var str0 = (StringNode) obj0.Values[0];
            Assert.That(str0.Value, Is.EqualTo("Bob"));

            var val1 = (GenericArrayNode) node.Values[0];
            Assert.That(val1.Values[1], Is.TypeOf<ObjectNode>());
            var obj1 = (ObjectNode) val0.Values[1];
            Assert.That(obj1.Values[0], Is.TypeOf<StringNode>());
            var str1 = (StringNode) obj1.Values[0];
            Assert.That(str0.Value, Is.EqualTo("Jill"));
        }

        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof (GenericArrayNode)));
        }
    }
}