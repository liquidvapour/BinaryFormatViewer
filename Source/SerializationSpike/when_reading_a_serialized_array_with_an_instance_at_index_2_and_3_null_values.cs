/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 16/08/2010
 * Time: 09:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_array_with_an_instance_at_index_2_and_3_null_values :
        BinarySerializedObjectSpec
    {
        protected TestItem[] testArray = {null, null, new TestItem("Bob"), null};

        protected override object GetObjectToSerialize()
        {
            return testArray;
        }

        protected override string GetFileName()
        {
            return "array_that_contains_an_instance_at_index_2_and_3_null_values.bin";
        }

        [Test]
        public void should_contain_a_null_node_at_index_0_1_and_3()
        {
            var arrayNode = (GenericArrayNode) result;
            Assert.That(arrayNode.Values[0], Is.TypeOf(typeof (NullNode)));
            Assert.That(arrayNode.Values[1], Is.TypeOf(typeof (NullNode)));
            Assert.That(arrayNode.Values[3], Is.TypeOf(typeof (NullNode)));
        }

        [Test]
        public void should_contain_a_object_node_at_index_2()
        {
            var arrayNode = (GenericArrayNode) result;
            Assert.That(arrayNode.Values[2], Is.TypeOf(typeof (ObjectNode)));
        }

        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof (GenericArrayNode)));
        }
    }
}