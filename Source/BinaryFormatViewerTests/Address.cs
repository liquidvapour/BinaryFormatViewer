using System;

namespace SerializationSpike
{
    [Serializable]
    public class Address
    {
        public string Line1 { get; set; }

        public string PostCode { get; set; }
    }
}