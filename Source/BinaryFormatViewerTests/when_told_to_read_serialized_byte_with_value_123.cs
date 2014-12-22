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
    public class when_told_to_read_serialized_byte_with_value_123 : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            return (byte) 123;
        }

        protected override string GetFileName()
        {
            return "byte_test.bin";
        }

        [Test]
        public void boolean_node_should_have_value_true()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            var byteNode = (ValueNode<byte>) runtimeObjectNode.Children[0];
            Assert.That(byteNode.Value, Is.EqualTo(123));
        }

        [Test]
        public void should_return_a_runtime_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof (RuntimeObjectNode)));
        }

        [Test]
        public void the_runtime_object_should_contain_a_byte_node()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            Assert.That(runtimeObjectNode.Children[0], Is.TypeOf(typeof (ValueNode<byte>)));
        }
    }
}