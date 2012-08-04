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

    [Serializable]
    class A
    {
        public A() : this("hello") { }
        
        public A(string msg)
        {
            this.msg = msg;
        }
        
        B bval = new B("bye");
        C cval = new C() { Info = new string[] {"hello","world"}};
        string msg = "hello";
        
        public string Msg
        {
            get { return msg; }
        }
    }
    
    [Serializable]
    class B
    {
        string str;
        
        public string Str
        {
            get { return str; }
        }
        
        public B(string str)
        {
            this.str = str;
        }
    }
    
    [Serializable]
    struct C
    {
        string[] info;
        
        public string[] Info
        {
            get { return info; }
            set { info = value; }
        }
    }
    
    [Serializable]
    class BoxedPrimitive
    {
        public BoxedPrimitive()
        {
            _val = 11;
        }
        
        object _val;
        
    }
    
    class MoreTests
    {
        
    }
    
    class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = new System.IO.MemoryStream())
            {
                var test = new BoxedPrimitive();
                
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(stream, test);
                
                stream.Position = 0;
                
                var node = new BinaryFormatReader().Read(stream);
                stream.Close();
            }
        }
    }
    
    [TestFixture]
    public abstract class SpecificationBase<T>
    {
        protected T sut;
        
        [SetUp]
        public void SetUp()
        {
            try
            {
                SetUpContext();
                sut = CreateSUT();
                Because();
            }
            finally
            {
                OnSetUpCompleted();
            }
        }
        
        protected virtual void SetUpContext() { }
        protected virtual void OnSetUpCompleted() { }
        protected abstract T CreateSUT();
        protected abstract void Because();
    }

    public abstract class BinarySerializedObjectSpec : SpecificationBase<BinaryFormatReader>
    {
        private System.IO.MemoryStream  stream;
        protected Node result;
        
        protected override BinaryFormatReader CreateSUT()
        {
            return new BinaryFormatReader();
        }

        protected override void SetUpContext()
        {
            stream = new System.IO.MemoryStream();
            object test = GetObjectToSerialize();
            
            var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bf.Serialize(stream, test);
            
            WriteToFile(stream);
            
            stream.Position = 0;            
        }

        protected override void OnSetUpCompleted()
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
        
        protected override void Because()
        {
            result = sut.Read(stream);
        }

        protected abstract object GetObjectToSerialize();
        protected abstract string GetFileName();

        protected Node FindNodeWithNameIn(string name, RuntimeObjectNode objectNode)
        {
            for (var i = 0; i < objectNode.Fields.Count; i++)
            {
                if (objectNode.Fields[i].Name == name)
                {
                    return objectNode.Values[i];
                }
            }            
            
            return null;
        }
        
        private void WriteToFile(System.IO.MemoryStream stream)
        {
            stream.Position = 0;

            using (var file = System.IO.File.OpenWrite(GetFileName()))
            {
                stream.WriteTo(file);
                file.Close();
            }
        }
    }
    
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

    [TestFixture]
    public class when_told_to_read_blob_bin : SpecificationBase<BinaryFormatReader>
    {
        private System.IO.MemoryStream  stream;
        protected Node result;
        
        protected override BinaryFormatReader CreateSUT()
        {
            return new BinaryFormatReader();
        }

        protected override void SetUpContext()
        {
            stream = new System.IO.MemoryStream();
            

            ReadFile(stream);
            
            stream.Position = 0;            
        }

        protected override void OnSetUpCompleted()
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
        
        protected override void Because()
        {
            result = sut.Read(stream);
        }

        [Test]
        public void ShouldWork()
        {
            Assert.IsNotNull(result);
        }

        protected Node FindNodeWithNameIn(string name, RuntimeObjectNode objectNode)
        {
            for (var i = 0; i < objectNode.Fields.Count; i++)
            {
                if (objectNode.Fields[i].Name == name)
                {
                    return objectNode.Values[i];
                }
            }            
            
            return null;
        }
        
        private void ReadFile(System.IO.MemoryStream stream)
        {
            stream.Position = 0;

            using (var file = System.IO.File.OpenRead(GetFileName()))
            {
                var buff = new byte[file.Length];
                file.Read(buff, 0, buff.Length);
                file.Close();
                stream.Write(buff, 0, buff.Length);
            }
            
            
            
            stream.Position = 0;
        }

        
        
        protected string GetFileName()
        {
            return "blob.bin";
        }        
    }    
    
    [TestFixture]
    public class when_told_to_read_serialized_int16_with_value_13 : BinarySerializedObjectSpec
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
            Assert.That(runtimeObjectNode.Values[0], Is.TypeOf(typeof(ValueNode<short>)));
        }

        [Test]
        public void int_16_node_should_have_value_13()
        {
            var runtimeObjectNode = (RuntimeObjectNode)result;
            var shortNode = (ValueNode<short>)runtimeObjectNode.Values[0];
            Assert.That(shortNode.Value, Is.EqualTo(13));
        }
        
        protected override object GetObjectToSerialize()
        {
            short result = 13;
            return result;
        }
        
        protected override string GetFileName()
        {
            return "int16_test.bin";
        }        
    }

    [TestFixture]
    public class when_told_to_read_serialized_date_time_with_value_min_value : BinarySerializedObjectSpec
    {
        [Test]
        public void should_return_a_runtime_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof(RuntimeObjectNode)));
        }
        
        [Test]
        public void the_runtime_object_should_contain_a_int64_value_node_called_ticks()
        {
            var runtimeObjectNode = (RuntimeObjectNode)result;
            var node = FindNodeWithNameIn("ticks", runtimeObjectNode) as ValueNode<long>;
            Assert.That(node, Is.Not.Null);
        }

        [Test]
        public void ticks_should_be_0()
        {
            var runtimeObjectNode = (RuntimeObjectNode)result;
            var node = FindNodeWithNameIn("ticks", runtimeObjectNode) as ValueNode<long>;            
            Assert.That(node.Value, Is.EqualTo(0));
        }
        
        protected override object GetObjectToSerialize()
        {
            var result = DateTime.MinValue;
            return result;
        }
        
        protected override string GetFileName()
        {
            return "date_time_test.bin";
        }        
    }

    [TestFixture]
    public class when_told_to_read_seialized_class_that_contains_a_date_time : BinarySerializedObjectSpec
    {
        [Serializable]
        class ContainsADateTime
        {
            private DateTime minDate;
            private DateTime maxDate;
            
            public ContainsADateTime()
            {
                minDate = DateTime.MinValue;
                maxDate = DateTime.MaxValue;
            }
        }
        
        [Test]
        public void should_contain_an_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof(ObjectNode)));
        }
        
        [Test]
        public void object_node_should_contain_date_time_value_node_called_min_date()
        {
            var objectNode = (ObjectNode)result;
            var node = base.FindNodeWithNameIn("minDate", objectNode);
            Assert.That(node, Is.TypeOf(typeof(ValueNode<DateTime>)));
        }
        
        [Test]
        public void min_date_field_should_have_value_date_time_min_value()
        {
            var objectNode = (ObjectNode)result;
            var node = (ValueNode<DateTime>)base.FindNodeWithNameIn("minDate", objectNode);
            Assert.That(node.Value, Is.EqualTo(DateTime.MinValue));
        }
        
		protected override object GetObjectToSerialize()
		{
		    return new ContainsADateTime();
		}
		
		protected override string GetFileName()
		{
			return "contains_a_date_time.bin";
		}
    }
    
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

    [TestFixture]
    public class when_told_to_read_serialized_byte_with_value_123 : BinarySerializedObjectSpec
    {
        [Test]
        public void should_return_a_runtime_object_node()
        {
            Assert.That(result, Is.TypeOf(typeof(RuntimeObjectNode)));
        }
        
        [Test]
        public void the_runtime_object_should_contain_a_byte_node()
        {
            var runtimeObjectNode = (RuntimeObjectNode)result;
            Assert.That(runtimeObjectNode.Values[0], Is.TypeOf(typeof(ValueNode<byte>)));
        }

        [Test]
        public void boolean_node_should_have_value_true()
        {
            var runtimeObjectNode = (RuntimeObjectNode)result;
            var byteNode = (ValueNode<byte>)runtimeObjectNode.Values[0];
            Assert.That(byteNode.Value, Is.EqualTo(123));
        }
        
        protected override object GetObjectToSerialize()
        {
            return (byte)123;
        }
        
        protected override string GetFileName()
        {
            return "boolean_test.bin";
        }        
    }

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
    
    [TestFixture]
    public class when_told_to_read_boxed_primitives : BinarySerializedObjectSpec
    {
        [Test]
        public void should_return_an_object_node()
        {
            var objectNode = result as ObjectNode;
            Assert.That(result, Is.TypeOf(typeof(ObjectNode)));
        }
        
        [Test]
        public void should_create_a_node_with_one_int32_child_node()
        {
            var objectNode = (ObjectNode)result;
            Assert.That(objectNode.Values[0], Is.TypeOf(typeof(ValueNode<int>)));
        }

        protected override object GetObjectToSerialize()
        {
            return new BoxedPrimitive();
        }
        
        protected override string GetFileName()
        {
            return "custom class with primitive type.bin";
        }
    }
}