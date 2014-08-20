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
}