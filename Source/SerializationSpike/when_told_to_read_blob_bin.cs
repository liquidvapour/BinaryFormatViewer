using System;
using BinaryFormatViewer;
using NUnit.Framework;

namespace SerializationSpike
{
    [TestFixture]
    [Ignore("the blob.bin has gone AWOL unignore once it returns.")]
	public class when_told_to_read_blob_bin : SpecificationBase<BinaryFormatReader>
	{
	    private System.IO.MemoryStream  stream;
	    protected Node result;
	    
	    protected override BinaryFormatReader CreateSUT()
	    {
	        return new BinaryFormatReader();
	    }
	
	    protected override void SetUpContext()
	    {
	        stream = new System.IO.MemoryStream();
	        
	
	        ReadFile(stream);
	        
	        stream.Position = 0;            
	    }
	
	    protected override void OnSetUpCompleted()
	    {
	        if (stream != null)
	        {
	            stream.Close();
	        }
	    }
	    
	    protected override void Because()
	    {
	        result = sut.Read(stream);
	    }
	
	    [Test]
	    public void ShouldWork()
	    {
	        Assert.IsNotNull(result);
	    }
	
	    protected Node FindNodeWithNameIn(string name, RuntimeObjectNode objectNode)
	    {
	        for (var i = 0; i < objectNode.Fields.Count; i++)
	        {
	            if (objectNode.Fields[i].Name == name)
	            {
	                return objectNode.Values[i];
	            }
	        }            
	        
	        return null;
	    }
	    
	    private void ReadFile(System.IO.MemoryStream stream)
	    {
	        stream.Position = 0;
	
	        using (var file = System.IO.File.OpenRead(GetFileName()))
	        {
	            var buff = new byte[file.Length];
	            file.Read(buff, 0, buff.Length);
	            file.Close();
	            stream.Write(buff, 0, buff.Length);
	        }
	        
	        stream.Position = 0;
	    }
	
	    protected string GetFileName()
	    {
	        return "blob.bin";
	    }        
	}    
}
