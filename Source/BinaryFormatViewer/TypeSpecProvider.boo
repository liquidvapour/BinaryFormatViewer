﻿namespace BinaryFormatViewer

import System

class TypeSpecProvider:
    private static logger = log4net.LogManager.GetLogger(TypeSpecProvider)
    _typeSpecReaders as List[of TypeSpecReader]
    
    def constructor():
        _typeSpecReaders = List[of TypeSpecReader]()
        _typeSpecReaders.Add(PrimitiveTypeSpecReader())
        _typeSpecReaders.Add(GeneralTypeTypeSpecReader())
        _typeSpecReaders.Add(StringTypeSpecReader())
        _typeSpecReaders.Add(ObjectTypeSpecReader())
        _typeSpecReaders.Add(StringArrayTypeSpecReader())
        _typeSpecReaders.Add(RuntimeTypeSpecReader())
        _typeSpecReaders.Add(ArrayOfPrimitiveTypeSpecReader())
        _typeSpecReaders.Add(ArrayOfObject())
        
    def GetTypeSpecFor(typeTag as byte, binaryReader as System.IO.BinaryReader) as TypeSpec:
        logger.Debug("Getting TypeSpec for typeTag: '${typeTag}' at position: '${binaryReader.BaseStream.Position}'.")
        for tsr in _typeSpecReaders:
            if (tsr.CanRead(typeTag)):
                logger.Debug("Found TypeSpecReader '${tsr.GetType().FullName}'.")
                return tsr.Read(binaryReader)
                
        raise ArgumentException("No type spec reader for type tag ${typeTag}", "typeTag")

