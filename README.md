BinaryFormatViewer
==================

An implementation of a binary serialization reader implemented in Boo and C#.

This toolset takes a *System.IO.Stream* and returns a *BinaryFormatViewer.BinaryFormatterOutput*.

BinaryFormatViewer.BinaryFormatterOutput holds a reference to the root BinaryFormatViewer.Node which represents the tree of deserialized  objects and their values.

example:
```C#
public static void Main(string[] args)
{
    using (var stream = new System.IO.MemoryStream())
    {
        var test = new BoxedPrimitive();
        
        var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        bf.Serialize(stream, test);
        
        stream.Position = 0;
        
        var node = new BinaryFormatReader().Read(stream);
        stream.Close();
    }
}
```
