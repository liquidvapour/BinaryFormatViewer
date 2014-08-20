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
