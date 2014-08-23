using System;

namespace SerializationSpike
{
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
}