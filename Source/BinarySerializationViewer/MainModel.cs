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
using System.IO;
using System.Windows.Input;
using BinaryFormatViewer;
using System.IO.Compression;

namespace BinarySerializationViewer
{
	public class MainModel
	{
	    public BinaryFormatterOutput LoadFile(string path)
	    {
	        BinaryFormatterOutput result = null;
	        
	        using (var unzipedStream = new MemoryStream())
	        {
	        
		        using (var file = System.IO.File.OpenRead(path))
	        	using (var s = new GZipStream(file, CompressionMode.Decompress)) 
		        {
		        	var buffer = new byte[1024];
		        	var readBytes = 0;
		        	do 
		        	{
		        		readBytes = s.Read(buffer, 0, buffer.Length);
		        		unzipedStream.Write(buffer, 0, readBytes);
		        	} while (readBytes == buffer.Length);
		            s.Close();	            
		            file.Close();
		        }
		        
		        unzipedStream.Position = 0;
		        
		        result = new BinaryFormatReader().ReadFull(unzipedStream);
	        }
            return result;
	    }
	}
}
