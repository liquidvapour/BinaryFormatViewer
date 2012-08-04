namespace BinaryFormatViewer

import System

class TypeSpecProvider:
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
		
	def GetTypeSpecFor(typeTag as byte, binaryReader as System.IO.BinaryReader) as TypeSpec:
		for tsr in _typeSpecReaders:
			if (tsr.CanRead(typeTag)):
				return tsr.Read(binaryReader)
				
		raise ArgumentException("No type spec reader for type tag ${typeTag}", "typeTag")

