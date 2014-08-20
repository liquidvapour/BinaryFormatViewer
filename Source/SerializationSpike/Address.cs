using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SerializationSpike
{
    [Serializable]
    public class Address
    {
        public string Line1 { get; set; }

        public string PostCode { get; set; }
    }
}
