using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeSpec : TypeSpec
    {
        protected byte _typeCode;

        public byte TypeCode
        {
            get
            {
                return this._typeCode;
            }
            set
            {
                this._typeCode = value;
            }
        }

        public PrimitiveTypeSpec(byte typeCode)
        {
            this._typeCode = typeCode;
        }
    }
}
