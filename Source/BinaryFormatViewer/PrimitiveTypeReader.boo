namespace BinaryFormatViewer

import System
import System.IO

class PrimitiveTypeReader:
    private static logger = log4net.LogManager.GetLogger(PrimitiveTypeReader)
    private _strategies = List[of PrimitiveTypeReaderFactoryBase]()

    public def constructor():
        _strategies.Add(PrimitiveTypeNodeFactories.Boolean)
        _strategies.Add(PrimitiveTypeNodeFactories.Byte)
        _strategies.Add(PrimitiveTypeNodeFactories.Int16)
        _strategies.Add(PrimitiveTypeNodeFactories.Int32)
        _strategies.Add(PrimitiveTypeNodeFactories.Int64)
        _strategies.Add(PrimitiveTypeNodeFactories.DateTime)
        _strategies.Add(PrimitiveTypeNodeFactories.UInt32)
        _strategies.Add(PrimitiveTypeNodeFactories.UInt64)
        
    def Read(binaryReader as BinaryReader, typeCode as byte):        
        strategy = GetPrimitiveTypeReaderStrategy(typeCode)
        return strategy.Read(binaryReader)
    
    def Read(binaryReader as BinaryReader):
        typeCode = binaryReader.ReadByte()
        strategy = GetPrimitiveTypeReaderStrategy(typeCode)
        return strategy.Read(binaryReader)
        
    private def GetPrimitiveTypeReaderStrategy(typeCode as byte):
        for strategy in _strategies:
            if strategy.CanRead(typeCode):
                logger.DebugFormat("Found PrimitiveTypeReader '{0}' for typeCode: '{1}'.", strategy.GetType().FullName, typeCode)
                return strategy
                
        raise ArgumentException("Unhandled type code: ${typeCode}", "typeCode")

static class PrimitiveTypeNodeFactories:
    public Boolean = PrimitiveTypeNodeFactory(1, {x as BinaryReader| ValueNode[of bool](x.ReadBoolean())})
    public Byte = PrimitiveTypeNodeFactory(2, {x as BinaryReader| ValueNode[of byte](x.ReadByte())})
    public Int16 = PrimitiveTypeNodeFactory(7, {x as BinaryReader| ValueNode[of short](x.ReadInt16())})
    public Int32 = PrimitiveTypeNodeFactory(8, {x as BinaryReader| ValueNode[of int](x.ReadInt32())})
    public Int64 = PrimitiveTypeNodeFactory(9, {x as BinaryReader| ValueNode[of long](x.ReadInt64())})
    public DateTime = PrimitiveTypeNodeFactory(13, {x as BinaryReader| ValueNode[of DateTime](System.DateTime.FromBinary(x.ReadInt64()))})
    public UInt32 = PrimitiveTypeNodeFactory(15, {x as BinaryReader| ValueNode[of uint](x.ReadUInt32())})
    public UInt64 = PrimitiveTypeNodeFactory(16, {x as BinaryReader| ValueNode[of ulong](x.ReadUInt64())})
    

abstract class PrimitiveTypeReaderFactoryBase:
    abstract def CanRead(typeCode as byte) as bool:
        pass
    abstract def Read(binaryReader as BinaryReader) as Node:
        pass

class PrimitiveTypeNodeFactory(PrimitiveTypeReaderFactoryBase):
    private static logger = log4net.LogManager.GetLogger(PrimitiveTypeNodeFactory)
    
    private _typeId as byte
    private _read as callable(BinaryReader) as Node
    
    def constructor(typeId as byte, read as callable(BinaryReader) as Node):
        _typeId = typeId
        _read = read
    
    def CanRead(typeCode as byte):
        return _typeId == typeCode
        
    def Read(binaryReader as BinaryReader):
        result = _read(binaryReader)
        logger.DebugFormat("Read Primitive value: '{0}'", result);
        return result
    
        
        