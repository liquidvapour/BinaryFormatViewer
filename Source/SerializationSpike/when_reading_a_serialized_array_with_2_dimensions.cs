/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 16/08/2010
 * Time: 09:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class when_reading_a_serialized_array_with_2_dimensions : BinarySerializedObjectSpec
    {
        protected override object GetObjectToSerialize()
        {
            var testArray = new TestItem[2, 2];
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
        public void should_have_2_cells_in_dimention_1()
        {
            var genericArrayNode = (GenericArrayNode) result;
            Assert.That(genericArrayNode.ElementCountPerDimension[0], Is.EqualTo(2));
        }

        [Test]
        public void should_have_2_cells_in_dimention_2()
        {
            var genericArrayNode = (GenericArrayNode) result;
            Assert.That(genericArrayNode.ElementCountPerDimension[1], Is.EqualTo(2));
        }

        [Test]
        public void should_have_2_dimentions()
        {
            var genericArrayNode = (GenericArrayNode) result;
            Assert.That(genericArrayNode.ElementCountPerDimension.Count, Is.EqualTo(2));
        }

        [Test]
        public void should_return_result_of_generic_array_node()
        {
            Assert.That(result, Is.TypeOf(typeof (GenericArrayNode)));
        }
    }
}