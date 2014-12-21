using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    public class ValueNodeTests
    {
        [Test]
        public void when_told_to_provide_type_argument_name_for_int_should_return_Int32()
        {
            var sut = new ValueNode<int>(2);

            var result = sut.GetTypeArgumentName();

            Assert.That(result, Is.EqualTo("Int32"));

        }
    }
}
