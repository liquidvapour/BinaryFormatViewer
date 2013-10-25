namespace BinaryFormatViewer

import System
import System.Collections.Generic

class ReadContext:
    [Property(ExistingObjects)]
    private _existingObjects as IDictionary[of uint, Node] = Dictionary[of uint, Node]()

class PartReader:
    abstract def Read(binaryReader as System.IO.BinaryReader, context as ReadContext) as Node:
        pass
        
    abstract def CanRead(partCode as int) as bool:
        pass
        
class HeaderPartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        binaryReader.ReadBytes(16) 
        return StartNode()
    
    def CanRead(partCode as int):
        return partCode == 0

class RefTypeObjectPartReader(ObjectReaderBase):
    
    def constructor(partProvider as PartProvider, primitiveTypeReader as PrimitiveTypeReader):
        super(partProvider, primitiveTypeReader)
    
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        typeMetaDataObjectId = binaryReader.ReadUInt32()
        
        # use _objects dictionary to lookup typespecs by typeMetaDataObjectId
        
        metaDataObject as IHaveTypeSpecs = context.ExistingObjects[typeMetaDataObjectId] 
        
        assemblyId = 0
        assemblyRef = metaDataObject.Assembly as AssemblyRefNode
        if assemblyRef:
            assemblyId = assemblyRef.Id
        else:
            assemblyInfo = metaDataObject.Assembly as AssemblyNode
            if assemblyInfo:
                assemblyId = assemblyInfo.Id
                
        fieldData = Dictionary[of string, Node]()
        
        for i in range(metaDataObject.Fields.Count):
            fieldData.Add(metaDataObject.Fields[i].Name, super.GetNodeBy(binaryReader, metaDataObject.Fields[i].TypeSpec, context))
        
        return ObjectNode(objectId, metaDataObject.Name, assemblyId, metaDataObject.Fields) 
    
    def CanRead(partCode as int):
        return partCode == 1

class AssemblyPartReader(PartReader):
    static PartCode = 12
    
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        assemblyId = binaryReader.ReadUInt32()
        assemblyName = binaryReader.ReadString()
        return AssemblyNode(assemblyId, assemblyName)
    
    def CanRead(partCode as int):
        return partCode == 12

abstract class ObjectReaderBase(PartReader):
    _typeSpecProvider = TypeSpecProvider()
    _partProvider as PartProvider = null
    _primitiveTypeReader as PrimitiveTypeReader
    
    protected def constructor(partProvider as PartProvider, primitiveTypeReader as PrimitiveTypeReader):
        _partProvider = partProvider
        _primitiveTypeReader = primitiveTypeReader
    
    protected def GetNodeBy(binaryReader as System.IO.BinaryReader, typeSpec as TypeSpec, context as ReadContext):
        if typeSpec isa PrimitiveTypeSpec:
            prm = typeSpec as PrimitiveTypeSpec            
            return _primitiveTypeReader.Read(binaryReader, prm.TypeCode)
        elif typeSpec isa ArrayOfPrimitiveTypeSpec:
            aryOfPrm = typeSpec as ArrayOfPrimitiveTypeSpec            
            return _primitiveTypeReader.Read(binaryReader, aryOfPrm.TypeCode)
        else:
            return _partProvider.ReadNextPart(binaryReader, context)
            
    protected def ReadTypeSpecs(binaryReader as System.IO.BinaryReader, fieldCount as uint):
        typeTags = List[of int](fieldCount)
        for i in range(0, fieldCount):
            typeTags.Add(binaryReader.ReadByte())
        
        typeSpecs = List[of TypeSpec](fieldCount)
        for tag in typeTags:
            typeSpecs.Add(_typeSpecProvider.GetTypeSpecFor(tag, binaryReader))
        return typeSpecs    

class RuntimeObjectPartReader(ObjectReaderBase):
    def constructor(partProvider as PartProvider, primitiveTypeReader as PrimitiveTypeReader):
        super(partProvider, primitiveTypeReader)
        
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        name = binaryReader.ReadString()
        fieldCount = binaryReader.ReadUInt32()
        
        fieldNames = List[of string](fieldCount)
        for i in range(0, fieldCount):
            fieldNames.Add(binaryReader.ReadString())
            
        typeSpecs = ReadTypeSpecs(binaryReader, fieldCount)
            
        nodes = List[of Node]()
        for typeSpec in typeSpecs:
            nodes.Add(GetNodeBy(binaryReader, typeSpec, context))
            
        fieldNodes = List[of FieldNode]()
        for i in range(fieldNames.Count):
            fieldNodes.Add(FieldNode(fieldNames[i], nodes[i], typeSpecs[i]))
            
        return RuntimeObjectNode(objectId, name, fieldNodes)
        
    def CanRead(partCode as int):
        return partCode == 4


class ExternalObjectPartReader(ObjectReaderBase):
    def constructor(partProvider as PartProvider, primitiveTypeReader as PrimitiveTypeReader):
        super(partProvider, primitiveTypeReader)
    
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        name = binaryReader.ReadString()
        fieldCount = binaryReader.ReadUInt32()
        
        fieldNames = List[of string](fieldCount)
        for i in range(0, fieldCount):
            fieldNames.Add(binaryReader.ReadString())
            
        typeSpecs = ReadTypeSpecs(binaryReader, fieldCount)
            
        assemblyId = binaryReader.ReadUInt32()
        
        nodes = List[of Node]()
        for typeSpec in typeSpecs:
            nodes.Add(GetNodeBy(binaryReader, typeSpec, context))
            
        fieldNodes = List[of FieldNode]()
        for i in range(fieldNames.Count):
            fieldNodes.Add(FieldNode(fieldNames[i], nodes[i], typeSpecs[i]))
            
            
        return ObjectNode(objectId, name, assemblyId, fieldNodes)
        
    def CanRead(partCode as int):
        return partCode == 5


class ObjectReferencePartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        refId = binaryReader.ReadUInt32()
        return ObjectReferenceNode(refId)
        
    def CanRead(partCode as int):
        return partCode == 9


class NullValuePartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        return NullNode()
        
    def CanRead(partCode as int):
        return partCode == 10


class StringPartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        val = binaryReader.ReadString()
        return StringNode(objectId, val)
        
    def CanRead(partCode as int):
        return partCode == 6    


class GenericArrayReader(ObjectReaderBase):
    def constructor(partProvider as PartProvider, primitiveTypeReader as PrimitiveTypeReader):
        super(partProvider, primitiveTypeReader)
    
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        typeOfArray = binaryReader.ReadByte();
        numberOfDimentions = binaryReader.ReadUInt32();
        
        elementCountPerDimention = List[of uint]()
        for i in range(numberOfDimentions):
            elementCountPerDimention.Add(binaryReader.ReadUInt32())
            
        typeSpecs = ReadTypeSpecs(binaryReader, 1)
        
        nodes = List[of Node]()
        nullItemsLeft = 0
        for i in range(GetTotalElementCount(elementCountPerDimention)):            
            if nullItemsLeft == 0:
                node = GetNodeBy(binaryReader, typeSpecs[0], context) 
                # if node is a array filter node add x null nodes to the nodes list.
                arrayFilter = node as ArrayFilterNode
                nullItemsLeft = arrayFilter.NumberOfNullItems if arrayFilter
                                    
            if nullItemsLeft > 0:
                node = NullNode()
                nullItemsLeft--
                
            nodes.Add(node)
            
        return GenericArrayNode(objectId, nodes, elementCountPerDimention, typeSpecs[0])
        
    def CanRead(partCode as int):
        return partCode == 7
        
    def GetTotalElementCount(elementCountPerDimention as List[of uint]):
        result as ulong = 0
        for count in elementCountPerDimention:
            result += count
            
        return result


class ArrayOfObjectPartReader(PartReader):
    private _partProvider as PartProvider = null
    
    def constructor(partProvider as PartProvider):
        _partProvider = partProvider
//TODO RP implement ArrayOfObjectPart
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        numberOfElements = binaryReader.ReadUInt32()
        elements = List[of Node]()
        for i in range(numberOfElements):
            elements.Add(_partProvider.ReadNextPart(binaryReader, context))
            
        return ArrayOfObjectNode(objectId, elements)
        
    def CanRead(partCode as int):
        return partCode == 16
	

class ArrayOfStringPartReader(PartReader):
    private _partProvider as PartProvider = null
    
    def constructor(partProvider as PartProvider):
        _partProvider = partProvider

    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        objectId = binaryReader.ReadUInt32()
        numberOfElements = binaryReader.ReadUInt32()
        elements = List[of Node]()
        for i in range(numberOfElements):
            elements.Add(_partProvider.ReadNextPart(binaryReader, context))
            
        return ArrayOfStringNode(objectId, elements)
        
    def CanRead(partCode as int):
        return partCode == 17    


class EndPartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        return EndNode()
        
    def CanRead(partCode as int):
        return partCode == 11


class ArrayFilterBytePartReader(PartReader):
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        return ArrayFilterNode(binaryReader.ReadByte())
        
    def CanRead(partCode as int):
        return partCode == 13
    

class BoxedPrimitiveTypePartReader(PartReader):
    _primitiveTypeReader as PrimitiveTypeReader
    
    def constructor(primitiveTypeReader as PrimitiveTypeReader):
        _primitiveTypeReader = primitiveTypeReader
    
    def Read(binaryReader as System.IO.BinaryReader, context as ReadContext):
        return _primitiveTypeReader.Read(binaryReader)
        
    def CanRead(partCode as int):
        return partCode == 8
    