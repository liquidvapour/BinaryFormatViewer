namespace BinaryFormatViewer

import System

class TypeSpecReader:
	abstract def CanRead(typeTag as byte) as bool:
		pass
		
	abstract def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		pass

class PrimitiveTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 0
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return PrimitiveTypeSpec(binaryReader.ReadByte())
		
class StringTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 1
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return StringTypeSpec()

class ObjectTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 2
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return ObjectTypeSpec()

class RuntimeTypeSpecReader(TypeSpecReader):
    def CanRead(typeTag as byte) as bool:
        return typeTag == 3
        
    def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
        typeName = binaryReader.ReadString()
        return RuntimeTypeSpec(typeName)
		
class GeneralTypeTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 4
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		typeName = binaryReader.ReadString()
		assemblyId = binaryReader.ReadInt32()
		return GeneralTypeSpec(typeName, assemblyId)

class ArrayOfObject(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 5
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return ArrayOfObjectTypeSpec()
		

class StringArrayTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 6
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return StringArrayTypeSpec()

class ArrayOfPrimitiveTypeSpecReader(TypeSpecReader):
	def CanRead(typeTag as byte) as bool:
		return typeTag == 7
		
	def Read(binaryReader as System.IO.BinaryReader) as TypeSpec:
		return ArrayOfPrimitiveTypeSpec(binaryReader.ReadByte())
		
