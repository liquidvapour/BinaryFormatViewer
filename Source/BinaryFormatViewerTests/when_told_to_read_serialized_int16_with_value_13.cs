/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 02/08/2010
 * Time: 08:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_told_to_read_serialized_int16_with_value_13 : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            short result = 13;
            return result;
        }

        protected override string GetFileName()
        {
            return "int16_test.bin";
        }

        [Test]
        public void int_16_node_should_have_value_13()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            var shortNode = (ValueNode<short>) runtimeObjectNode.Children[0];
            Assert.That(shortNode.Value, Is.EqualTo(13));
        }

        [Test]
        public void should_return_a_runtime_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof (RuntimeObjectNode)));
        }

        [Test]
        public void the_runtime_object_should_contain_an_int_32_node()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            Assert.That(runtimeObjectNode.Children[0], Is.TypeOf(typeof (ValueNode<short>)));
        }
    }
}