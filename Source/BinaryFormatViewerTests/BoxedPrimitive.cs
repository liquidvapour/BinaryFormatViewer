using System;

namespace SerializationSpike
{
    [Serializable]
    internal class BoxedPrimitive
    {
        private object _val;

        public BoxedPrimitive()
        {
            _val = 11;
        }
    }
}