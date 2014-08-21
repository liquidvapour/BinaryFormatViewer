﻿using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringArrayTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 6;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new StringArrayTypeSpec();
        }
    }
}