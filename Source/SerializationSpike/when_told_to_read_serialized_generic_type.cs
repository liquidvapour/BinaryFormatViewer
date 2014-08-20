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
	public class when_told_to_read_serialized_generic_type : BinarySerializedObjectSpec
	{
	    [Serializable]
	    class Test<T, U>
	    {
	        private T _tValue;
	        private U _uValue;
	        
	        public Test(T value, U uValue)
	        {
	            _tValue = value;
	            _uValue = uValue;
	        }
	    }
	    
	    [Test]
	    public void should_return_a_runtime_object_node()
	    {
	        Assert.That(result, Is.TypeOf(typeof(ObjectNode)));
	    }
	    
	
	    
	    [Test]
	    public void should_contain_an_int32_value_node_with_name_tvalue_and_value_123()
	    {
	        var objectNode = (ObjectNode)result;
	        
	        bool nodeFound = false;
	        
	        var valueNode = FindNodeWithNameIn("_tValue", objectNode) as ValueNode<int>;
	        
	        if (valueNode != null)
	        {
	            Assert.That(valueNode.Value, Is.EqualTo(123));
	            nodeFound = true;
	        }    
	                    
	        Assert.That(nodeFound);
	    }
	
	    [Test]
	    public void should_contain_an_uint32_value_node_with_name_uvalue_and_value_321()
	    {
	        var objectNode = (ObjectNode)result;
	        
	        bool nodeFound = false;
	        
	        var valueNode = FindNodeWithNameIn("_uValue", objectNode) as ValueNode<uint>;
	        
	        if (valueNode != null)
	        {
	            Assert.That(valueNode.Value, Is.EqualTo(321));
	            nodeFound = true;
	        }    
	                    
	        Assert.That(nodeFound);
	    }
	    
	    
	    protected override object GetObjectToSerialize()
	    {
	        return new Test<int, uint>(123, 321);
	    }
	    
	    protected override string GetFileName()
	    {
	        return "generic_test.bin";
	    }        
	}
}
