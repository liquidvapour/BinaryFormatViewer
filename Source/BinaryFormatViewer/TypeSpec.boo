namespace BinaryFormatViewer

import System

class TypeSpec:
	pass
	
class GeneralTypeSpec(TypeSpec):
	[Property(TypeName)]
	_typeName as string
	
	[Property(AssemblyId)]
	_assemblyId as int
	
	public def constructor(typeName as string, assemblyId as int):
		_typeName = typeName
		_assemblyId = assemblyId
	
class ObjectTypeSpec(TypeSpec):
	pass
	
class StringTypeSpec(TypeSpec):
	pass
	
class StringArrayTypeSpec(TypeSpec):
	pass

class PrimitiveTypeSpec(TypeSpec):
	[Property(TypeCode)]
	_typeCode as byte
	
	def constructor(typeCode as byte):
		_typeCode = typeCode
		
class ArrayOfPrimitiveTypeSpec(TypeSpec):
	[Property(TypeCode)]
	_typeCode as byte
	
	def constructor(typeCode as byte):
		_typeCode = typeCode
		
class RuntimeTypeSpec(TypeSpec):
    [Property(TypeName)]
    _typeName as string
    
    def constructor(typeName as string):
        _typeName = typeName