using System;

namespace SerializationSpike
{

    [Serializable]
    public class TestItem
    {
        public string Name { get; set; }
        
        public TestItem(string name)
        {
            Name = name;
        }
    }
    

}
