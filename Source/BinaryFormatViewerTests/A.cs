using System;

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
}