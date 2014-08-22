/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 02/08/2010
 * Time: 08:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BinaryFormatViewer;

namespace SerializationSpike
{
    [Serializable]
    internal class A
    {
        private readonly string msg = "hello";
        private B bval = new B("bye");
        private C cval = new C {Info = new[] {"hello", "world"}};

        public A() : this("hello")
        {
        }

        public A(string msg)
        {
            this.msg = msg;
        }

        public string Msg
        {
            get { return msg; }
        }
    }

    [Serializable]
    internal class B
    {
        private readonly string str;

        public B(string str)
        {
            this.str = str;
        }

        public string Str
        {
            get { return str; }
        }
    }

    [Serializable]
    internal struct C
    {
        private string[] info;

        public string[] Info
        {
            get { return info; }
            set { info = value; }
        }
    }

    [Serializable]
    internal class BoxedPrimitive
    {
        private object _val;

        public BoxedPrimitive()
        {
            _val = 11;
        }
    }

    internal class MoreTests
    {
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            using (var stream = new MemoryStream())
            {
                var test = new BoxedPrimitive();

                var bf = new BinaryFormatter();
                bf.Serialize(stream, test);

                stream.Position = 0;

                Node node = new BinaryFormatReader().Read(stream);
                stream.Close();
            }
        }
    }
}