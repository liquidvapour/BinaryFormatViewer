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
    public class when_told_to_read_seialized_class_that_contains_a_date_time : BinarySerializedObjectSpec
    {
        [Serializable]
        private class ContainsADateTime
        {
            private DateTime maxDate;
            private DateTime minDate;

            public ContainsADateTime()
            {
                minDate = DateTime.MinValue;
                maxDate = DateTime.MaxValue;
            }
        }

        protected override object GetObjectToSerialize()
        {
            return new ContainsADateTime();
        }

        protected override string GetFileName()
        {
            return "contains_a_date_time.bin";
        }

        [Test]
        public void min_date_field_should_have_value_date_time_min_value()
        {
            var objectNode = (ObjectNode) result;
            var node = (ValueNode<DateTime>) base.FindNodeWithNameIn("minDate", objectNode);
            Assert.That(node.Value, Is.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void object_node_should_contain_date_time_value_node_called_min_date()
        {
            var objectNode = (ObjectNode) result;
            Node node = base.FindNodeWithNameIn("minDate", objectNode);
            Assert.That(node, Is.TypeOf(typeof (ValueNode<DateTime>)));
        }

        [Test]
        public void should_contain_an_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof (ObjectNode)));
        }
    }
}