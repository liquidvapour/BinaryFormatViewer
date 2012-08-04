/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 16/08/2010
 * Time: 09:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using BinaryFormatViewer;

namespace SerializationSpike
{

    [Serializable]
    public class TestItem
    {
        public string Name { get; set; }
        
        public TestItem(string name)
        {
            Name = name;
        }
    }
    
    [TestFixture]
    public class when_a_serialized_array_is_read : BinarySerializedObjectSpec
    {
        protected TestItem[] testArray = new TestItem[] { new TestItem("Bob"), new TestItem("Janet") };
        
        protected override object GetObjectToSerialize()
        {
            return testArray;
        }
        
        protected override string GetFileName()
        {
            return "single_dimension_array.bin";
        }
        
        [Test]
        public void should_return_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof(GenericArrayNode)));
        }
        
        [Test]
        public void returned_generic_array_node_should_have_2_values()
        {
            Assert.That(((GenericArrayNode)result).Values.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void value_at_index_0_should_be_an_object_node_for_test_item()
        {
            Assert.That(((GenericArrayNode)result).Values[0], Is.TypeOf(typeof(ObjectNode)));
        }
        
        [Test]
        public void object_node_at_index_0_should_be_type_test_item()
        {
            var node = (ObjectNode)((GenericArrayNode)result).Values[0];
            Assert.That(node.Name, Is.StringContaining("TestItem"));
        }

        [Test]
        public void object_node_at_index_0_should_be_a_string()
        {
            var node = (ObjectNode)((GenericArrayNode)result).Values[0];
            Assert.That(node.Values[0], Is.TypeOf(typeof(StringNode)));
        }
        
    }
    
    [TestFixture]
    public class when_reading_a_serialized_array_of_4_null_values : BinarySerializedObjectSpec
    {
        protected TestItem[] testArray = new TestItem[] { null, null, null, null };

        protected override object GetObjectToSerialize()
        {
            return testArray;
        }
        
        protected override string GetFileName()
        {
            return "array_that_contains_nulls.bin";
        }
        
        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof(GenericArrayNode)));
        }
        
        [Test]
        public void should_contain_a_null_node_at_index_0_through_3()
        {
            var arrayNode = (GenericArrayNode)result;
            Assert.That(arrayNode.Values[0], Is.TypeOf(typeof(NullNode)));
            Assert.That(arrayNode.Values[1], Is.TypeOf(typeof(NullNode)));
            Assert.That(arrayNode.Values[2], Is.TypeOf(typeof(NullNode)));
            Assert.That(arrayNode.Values[3], Is.TypeOf(typeof(NullNode)));
        }
    }

    [TestFixture]
    public class when_reading_a_serialized_array_with_an_instance_at_index_2_and_3_null_values : BinarySerializedObjectSpec
    {
        protected TestItem[] testArray = new TestItem[] { null, null, new TestItem("Bob"), null };

        protected override object GetObjectToSerialize()
        {
            return testArray;
        }
        
        protected override string GetFileName()
        {
            return "array_that_contains_an_instance_at_index_2_and_3_null_values.bin";
        }
        
        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof(GenericArrayNode)));
        }
        
        [Test]
        public void should_contain_a_null_node_at_index_0_1_and_3()
        {
            var arrayNode = (GenericArrayNode)result;
            Assert.That(arrayNode.Values[0], Is.TypeOf(typeof(NullNode)));
            Assert.That(arrayNode.Values[1], Is.TypeOf(typeof(NullNode)));
            Assert.That(arrayNode.Values[3], Is.TypeOf(typeof(NullNode)));
        }
        
        [Test]
        public void should_contain_a_object_node_at_index_2()
        {
            var arrayNode = (GenericArrayNode)result;
            Assert.That(arrayNode.Values[2], Is.TypeOf(typeof(ObjectNode)));
        }
    }
    
    [TestFixture]
    public class when_reading_a_serialized_array_with_2_dimensions : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            var testArray = new TestItem[2,2];
            testArray[0, 0] = new TestItem("Bob");
            testArray[0, 1] = new TestItem("Jill");
            testArray[1, 0] = new TestItem("John");
            testArray[1, 1] = new TestItem("Fran");
            return testArray;
        }
        
        protected override string GetFileName()
        {
            return "array_with_2_dimensions.bin";
        }
        
        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof(GenericArrayNode)));
        }
        
        [Test]
        public void should_have_2_dimentions()
        {
            var genericArrayNode = (GenericArrayNode)result;
            Assert.That(genericArrayNode.ElementCountPerDimension.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void should_have_2_cells_in_dimention_1()
        {
            var genericArrayNode = (GenericArrayNode)result;
            Assert.That(genericArrayNode.ElementCountPerDimension[0], Is.EqualTo(2));
        }
        
        [Test]
        public void should_have_2_cells_in_dimention_2()
        {
            var genericArrayNode = (GenericArrayNode)result;
            Assert.That(genericArrayNode.ElementCountPerDimension[1], Is.EqualTo(2));
        }

    }
    
        [TestFixture]
    public class when_reading_a_serialized_jagged_array_with_2_dimensions : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            TestItem[][] testArray = new TestItem[2][];
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
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof(GenericArrayNode)));
        }
    }
}
