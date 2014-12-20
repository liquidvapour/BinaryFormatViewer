/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 04/12/2010
 * Time: 11:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using TypeCode = BinaryFormatViewer.TypeCode;

namespace BinarySerializationViewer
{
    /// <summary>
    /// Description of PrimitiveTypeValueConverter.
    /// </summary>
    public class PrimitiveTypeValueConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is byte)
            {
                switch ((TypeCode)value) 
                {
                    case TypeCode.Boolean: return "System.Boolean";
                    case TypeCode.Byte: return "System.Byte";
                    case TypeCode.Int16: return "System.Int16";
                    case TypeCode.Int32: return "System.Int32";
                    case TypeCode.Int64: return "System.Int64";
                    case TypeCode.DateTime: return "System.DateTime";
                    case TypeCode.UInt32: return "System.UInt32";
                    case TypeCode.UInt64: return "System.UInt64";
                    default: return "No Name for primitiveId: " + (TypeCode)value;
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
