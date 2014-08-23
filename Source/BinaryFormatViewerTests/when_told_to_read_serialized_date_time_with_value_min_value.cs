/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 02/08/2010
 * Time: 08:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_told_to_read_serialized_date_time_with_value_min_value : BinarySerializedObjectSpec
    {
        private DateTime time;

        protected override object GetObjectToSerialize()
        {
            time = DateTime.Now;
            return time;
        }

        protected override string GetFileName()
        {
            return "date_time_test.bin";
        }

        [Test]
        public void should_return_a_runtime_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof (RuntimeObjectNode)));
        }

        [Test]
        public void the_runtime_object_should_contain_a_int64_value_node_called_ticks()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            var node = FindNodeWithNameIn("ticks", runtimeObjectNode) as ValueNode<long>;
            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void ticks_should_be_0()
        {
            var runtimeObjectNode = (RuntimeObjectNode) result;
            var node = FindNodeWithNameIn("ticks", runtimeObjectNode) as ValueNode<long>;
            Assert.That(node.Value, Is.EqualTo(time.Ticks));
        }
    }
}