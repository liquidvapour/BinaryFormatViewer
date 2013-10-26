namespace BinaryFormatViewer

import System

class TypeSpec:
    private _name as string
    
    def constructor(): 
        self(GetType().FullName)
    
    def constructor(name as string):
        _name = name
        
    def ToString():
        return _name
        
class GeneralTypeSpec(TypeSpec):
    [Property(TypeName)]
    _typeName as string
    
    [Property(AssemblyId)]
    _assemblyId as int
    
    def constructor(typeName as string, assemblyId as int):
        _typeName = typeName
        _assemblyId = assemblyId
    
class ObjectTypeSpec(TypeSpec):
    pass
    
class StringTypeSpec(TypeSpec):
    public def constructor():
        super("String")
    
class StringArrayTypeSpec(TypeSpec):
    pass

class ArrayOfObjectTypeSpec(TypeSpec):
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