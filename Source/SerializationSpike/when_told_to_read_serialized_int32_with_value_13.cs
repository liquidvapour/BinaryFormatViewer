﻿/*
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
	public class when_told_to_read_serialized_int32_with_value_13 : BinarySerializedObjectSpec
	{
	    [Test]
	    public void should_return_a_runtime_object_node()
	    {
	        Assert.That(result, Is.TypeOf(typeof(RuntimeObjectNode)));
	    }
	    
	    [Test]
	    public void the_runtime_object_should_contain_an_int_32_node()
	    {
	        var runtimeObjectNode = (RuntimeObjectNode)result;
	        Assert.That(runtimeObjectNode.Values[0], Is.TypeOf(typeof(ValueNode<int>)));
	    }
	
	    [Test]
	    public void int_32_node_should_have_value_13()
	    {
	        var runtimeObjectNode = (RuntimeObjectNode)result;
	        var int32Node = (ValueNode<int>)runtimeObjectNode.Values[0];
	        Assert.That(int32Node.Value, Is.EqualTo(13));
	    }
	    
	    protected override object GetObjectToSerialize()
	    {
	        return 13;
	    }
	    
	    protected override string GetFileName()
	    {
	        return "int32_test.bin";
	    }        
	}
}
