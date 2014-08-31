using System;
using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{

    [Serializable]
    public class Shmoo
    {
        public Bar Bar { get; set; }
        public Foo Foo { get; set; }
    }

    [Serializable]
    public class Foo
    {
        public Bar Bar { get; set; }
        public Shmoo Shmoo { get; set; }
    }

    [Serializable]
    public class Bar
    {
        public Foo Foo { get; set; }
        public Shmoo Shmoo { get; set; }
    }

    [TestFixture]
    public class when_tole_to_read_a_serialized_two_way_reference : BinarySerializedObjectSpec
    {

        [Test]
        public void should_work()
        {
            var fooNode = (ObjectNode)result;
            Assert.That(fooNode.Name, Is.EqualTo("SerializationSpike.Foo"));
            var barNode = (ObjectNode)fooNode.Fields[0].Value;
            Assert.That(barNode.Name, Is.EqualTo("SerializationSpike.Bar"));

        }

        protected override object GetObjectToSerialize()
        {
            var foo = new Foo();
            var bar = new Bar();
            var shmoo = new Shmoo();
            foo.Bar = bar;
            foo.Shmoo = shmoo;

            bar.Foo = foo;
            bar.Shmoo = shmoo;

            shmoo.Bar = bar;
            shmoo.Foo = foo;

            return foo;
        }

        protected override string GetFileName()
        {
            return "two_way_reference.bin";
        }
    }
}