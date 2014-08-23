/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 04/12/2010
 * Time: 11:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BinarySerializationViewer
{
    /// <summary>
    /// Description of PrimitiveTypeValueConverter.
    /// </summary>
    public class PrimitiveTypeValueConverter : System.Windows.Data.IValueConverter
    {
        public PrimitiveTypeValueConverter()
        {
        }
        
//        	public Boolean = PrimitiveTypeNodeFactory(1, {x as BinaryReader| ValueNode[of bool](x.ReadBoolean())})
//	public Byte = PrimitiveTypeNodeFactory(2, {x as BinaryReader| ValueNode[of byte](x.ReadByte())})
//	public Int16 = PrimitiveTypeNodeFactory(7, {x as BinaryReader| ValueNode[of short](x.ReadInt16())})
//	public Int32 = PrimitiveTypeNodeFactory(8, {x as BinaryReader| ValueNode[of int](x.ReadInt32())})
//	public Int64 = PrimitiveTypeNodeFactory(9, {x as BinaryReader| ValueNode[of long](x.ReadInt64())})
//	public DateTime = PrimitiveTypeNodeFactory(13, {x as BinaryReader| ValueNode[of DateTime](System.DateTime.FromBinary(x.ReadInt64()))})
//	public UInt32 = PrimitiveTypeNodeFactory(15, {x as BinaryReader| ValueNode[of uint](x.ReadUInt32())})
//	public UInt64 = PrimitiveTypeNodeFactory(16, {x as BinaryReader| ValueNode[of ulong](x.ReadUInt64())})

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte)
            {
                var primitiveId = (byte)value;
                switch (primitiveId) 
                {
                    case 1: return "System.Boolean";
                    case 2: return "System.Byte";
                    case 7: return "System.Int16";
                    case 8: return "System.Int32";
                    case 9: return "System.Int64";
                    case 13: return "System.DateTime";
                    case 15: return "System.UInt32";
                    case 16: return "System.UInt64";
                    default: return "No Name for primitiveId: " + primitiveId;
                }
            }
            
            return value.ToString();
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
