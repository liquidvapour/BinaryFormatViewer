﻿using System;
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
    public class when_told_to_read_a_serialized_two_way_reference : BinarySerializedObjectSpec
    {

        [Test]
        public void field_names_should_be_as_exptected()
        {
            var fooNode = (ObjectNode) result;
            Assert.That(fooNode.Name, Is.EqualTo("SerializationSpike.Foo"));

            AssertField(fooNode.Fields[0], "Bar");

            AssertField(fooNode.Fields[1], "Shmoo");
        }

        [Test]
        public void referenced_objects_field_names_should_be_as_expexted()
        {
            var fooNode = (ObjectNode) result;
            var shmooNode = (ObjectNode) fooNode.Fields[1].Value;
            Assert.That(shmooNode.Name, Is.EqualTo("SerializationSpike.Shmoo"));

            AssertField(shmooNode.Fields[0], "Bar");

            AssertField(shmooNode.Fields[1], "Foo");
        }

        [Test]
        public void reference_back_to_original_object_will_refer_to_the_same_object()            
        {
            var fooNode = (ObjectNode)result;
            var shmooNode = (ObjectNode)fooNode.Fields[1].Value; 
            var shmooFooNode = shmooNode.Fields[1];

            Assert.That(shmooFooNode.Value, Is.EqualTo(fooNode));
        }

        private static void AssertField(FieldNode fieldNode, string fieldName)
        {
            Assert.That(fieldNode.Name, Is.EqualTo("<{0}>k__BackingField".FormatUsing(fieldName)));
            Assert.That(fieldNode.Value, Is.TypeOf<ObjectNode>());
            Assert.That(((ObjectNode) fieldNode.Value).Name, Is.EqualTo("SerializationSpike.{0}".FormatUsing(fieldName)));
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