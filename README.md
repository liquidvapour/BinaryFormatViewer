BinaryFormatViewer
==================

An implementation of a binary serialization reader implemented in Boo and C#.

This toolset takes a System.IO.Stream and returns a BinaryFormatViewer.BinaryFormatterOutput.

BinaryFormatViewer.BinaryFormatterOutput holds a reference to the root BinaryFormatViewer.Node which represents the tree of deserialized  objects and their values.
