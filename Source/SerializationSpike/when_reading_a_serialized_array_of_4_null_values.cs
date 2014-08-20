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
    public class when_reading_a_serialized_TestItem : BinarySerializedObjectSpec
    {
        protected TestItem[] testArray = new TestItem[] { null, null, null, null };

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
            var node = (RuntimeObjectNode)result;
            Assert.AreEqual("SerializationSpike.TestItem", node.Name);
        }
    }

}
