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
	public class when_told_to_read_serialized_boolean_with_value_true : BinarySerializedObjectSpec
	{
	    [Test]
	    public void should_return_a_runtime_object_node()
	    {
	        Assert.That(result, Is.TypeOf(typeof(RuntimeObjectNode)));
	    }
	    
	    [Test]
	    public void the_runtime_object_should_contain_a_field_node()
	    {
	        var runtimeObjectNode = (RuntimeObjectNode)result;
	        Assert.That(runtimeObjectNode.Fields.Count, Is.EqualTo(1));
	    }
	
	    [Test]
	    public void the_field_node_should_contain_a_boolean_node()
	    {
	        var runtimeObjectNode = (RuntimeObjectNode)result;            
	        var fieldNode = (FieldNode)runtimeObjectNode.Fields[0];
	        Assert.That(fieldNode.Value, Is.TypeOf(typeof(ValueNode<bool>)));
	    }
	
	    
	    [Test]
	    public void boolean_node_should_have_value_true()
	    {
	        var runtimeObjectNode = (RuntimeObjectNode)result;
	        var shortNode = (ValueNode<bool>)runtimeObjectNode.Values[0];
	        Assert.That(shortNode.Value, Is.EqualTo(true));
	    }
	    
	    protected override object GetObjectToSerialize()
	    {
	        return true;
	    }
	    
	    protected override string GetFileName()
	    {
	        return "boolean_test.bin";
	    }        
	}
}
