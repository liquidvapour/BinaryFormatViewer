namespace BinaryFormatViewer

import System

class PartProvider:
    private _partReaders as List[of PartReader]
    private static logger = log4net.LogManager.GetLogger(PartProvider)

    
    private _primitiveTypeReader = PrimitiveTypeReader()

    def constructor():
        _partReaders = List[of PartReader]()
        _partReaders.Add(HeaderPartReader())
        _partReaders.Add(RefTypeObjectPartReader(self, _primitiveTypeReader))
        _partReaders.Add(AssemblyPartReader())
        _partReaders.Add(RuntimeObjectPartReader(self, _primitiveTypeReader))
        _partReaders.Add(ExternalObjectPartReader(self, _primitiveTypeReader))
        _partReaders.Add(ObjectReferencePartReader())
        _partReaders.Add(NullValuePartReader())
        _partReaders.Add(GenericArrayReader(self, _primitiveTypeReader))
        _partReaders.Add(StringPartReader())
        _partReaders.Add(ArrayOfStringPartReader(self))
        _partReaders.Add(ArrayOfObjectPartReader(self))
        _partReaders.Add(BoxedPrimitiveTypePartReader(_primitiveTypeReader))
        _partReaders.Add(EndPartReader())
        _partReaders.Add(ArrayFilterBytePartReader())        

    def ReadNextPart(reader as System.IO.BinaryReader, context as ReadContext):
        partCode as int = reader.ReadByte()
        logger.Debug("Finding part for partCode: '${partCode}' at position: '${reader.BaseStream.Position}'.")
        
        partReader = GetPartReader(partCode)
        node as Node
        if partReader:
            node = partReader.Read(reader, context)
            logger.Debug("Part read:\r\n${node}.")
        return node
        
    private def GetPartReader(partCode as int):
        for r in _partReaders:
            if r.CanRead(partCode):
                logger.Debug("Found part reader '${r.GetType().FullName}' for partCode: '${partCode}'.")
                return r
                
        raise ArgumentException("No part reader for partCode: ${partCode}", "partCode")

