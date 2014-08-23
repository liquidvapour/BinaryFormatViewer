using System;

namespace SerializationSpike
{
    [Serializable]
    public class Customer
    {
        public Address Address { get; set; }

        public int Age { get; set; }

        public string Name { get; set; }
    }
}