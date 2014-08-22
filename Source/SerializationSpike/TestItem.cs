using System;

namespace SerializationSpike
{
    [Serializable]
    public class TestItem
    {
        public TestItem(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}