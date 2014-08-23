using System;

namespace SerializationSpike
{
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
}