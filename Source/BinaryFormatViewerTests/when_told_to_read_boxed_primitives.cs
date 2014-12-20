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
    public class when_told_to_read_boxed_primitives : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            return new BoxedPrimitive();
        }

        protected override string GetFileName()
        {
            return "custom class with primitive type.bin";
        }

        [Test]
        public void should_create_a_node_with_one_int32_child_node()
        {
            var objectNode = (ObjectNode) result;
            Assert.That(objectNode.Values[0], Is.TypeOf(typeof (ValueNode<int>)));
        }

        [Test]
        public void should_return_an_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof (ObjectNode)));
        }
    }
}