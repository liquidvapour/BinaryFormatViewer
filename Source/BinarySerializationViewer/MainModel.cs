/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 25/08/2010
 * Time: 09:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Input;
using BinaryFormatViewer;

namespace BinarySerializationViewer
{
	public class MainModel
	{
	    public BinaryFormatterOutput LoadFile(string path)
	    {
	        BinaryFormatterOutput result = null;
	        using (var file = System.IO.File.OpenRead(path))
	        {
	            result = new BinaryFormatReader().ReadFull(file);
	            file.Close();
	        }
	        return result;
	    }
	}
}
