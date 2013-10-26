namespace BinaryFormatViewer

import System
import System.Text
import System.Collections.Generic

interface IHaveChildren:
    Values as List[of Node]:
        get:
            pass
        
interface IHaveTypeSpecs:
#    TypeSpecs as List[of TypeSpec]:
#        get:
#            pass
    Name as string:
        get:
            pass
            
    Assembly as Node:
        get:
            pass
            
    Fields as List[of FieldNode]:
        get:
            pass

public class Node:
    pass

class ObjectReferenceNode(Node):
    private _refId as uint
    
    def constructor(refId as uint):
        _refId = refId
        
    RefId as uint:
        get:
            return _refId
            
    def ToString():
        return "ObjectReferenceNode RefId: '${_refId}'"

class AssemblyNode(IdentifiedNode):
    private _name as string
    
    public def constructor(id as uint, name as string):
        super(id)
        _name = name
            
    Name as string:
        get:
            return _name
            
    def ToString():
        return "AssemblyNode\r\n${super.ToString()}\r\nName: '${_name}'"
        
class AssemblyRefNode(Node):
    private _id as uint
    
    def constructor(id as uint):
        _id = id
        
    Id as uint:
        get:
            return _id
    
    def ToString():
        return "AssemblyRef: ${_id}"
        
class IdentifiedNode(Node):
    _id as uint

    def constructor(id as uint):
        _id = id

    Id as uint:
        get:
            return _id
            
    def ToString():
        return "Id: '${_id}'"

class RuntimeObjectNode(IdentifiedNode, IHaveChildren, IHaveTypeSpecs):

    [Property(Assembly)]
    private _assembly as Node

    private _fieldValues as List[of FieldNode]
    private _name as string
    
    def constructor(id as uint, name as string, fields as List[of FieldNode]):
        self(id, name, fields, null)
    
    def constructor(id as uint, name as string, fields as List[of FieldNode], assembly as Node):
        super(id)
        _fieldValues = fields
        _name = name
        _assembly = assembly
        
    Values as List[of Node]:
        get:
            result = List[of Node]()
            for f in Fields:
                result.Add(f.Value)                
            return result
    
    Fields as List[of FieldNode]:
        get:
            return _fieldValues
            
    Name as string:
        get:
            return _name
            
    def ToString() :
        sb = StringBuilder()
        sb.AppendLine("Runtime Object")
        sb.AppendLine("--------------")
        sb.AppendLine(super.ToString())
        sb.AppendLine("Name: '${_name}'")
        if _assembly:
            sb.AppendLine("Assembly: '${_assembly.ToString()}'.")
            
        for field in Fields:
            sb.AppendLine("field: ${field.ToString()}")
            
        sb.AppendLine("--------------")
        return sb.ToString()
        
            
class FieldNode(Node):
    _value as Node
    _name as string
    _typeSpec as TypeSpec
    
    def constructor(name as string, value as Node, typeSpec as TypeSpec):
        _name = name
        _value = value
        _typeSpec = typeSpec
        
    Name as string:
        get:
            return _name
        
    Value as Node:
        get:
            return _value
        set:
            _value = value;
            
    TypeSpec as TypeSpec:
        get:
            return _typeSpec
    
    def ToString():
        return "${_name} of ${_typeSpec}: '${_value}'"
        
class ObjectNode(RuntimeObjectNode):
    def constructor(id as uint, name as string, assemblyId as uint, fields as List[of FieldNode]):
        super(id, name, fields, AssemblyRefNode(assemblyId))
        

class StringNode(IdentifiedNode):
    private _val as string
    
    def constructor(objectId as uint, val as string):
        super(objectId)
        _val = val
                    
    Value as string:
        get:
            return _val
            
    def ToString():
        return _val

class GenericArrayNode(IdentifiedNode, IHaveChildren):
    _vals as List[of Node]
    _elementCountPerDimension as List[of uint]
    _typeSpec as TypeSpec
    
    def constructor(objectId as uint, vals as List[of Node], elementCountPerDimension as List[of uint], typeSpec as TypeSpec):
        super(objectId)
        _vals = vals
        _elementCountPerDimension = elementCountPerDimension
        _typeSpec = typeSpec
        
    Values as List[of Node]:
        get:
            return _vals
            
    ElementCountPerDimension as List[of uint]:
        get:
            return _elementCountPerDimension
            
    TypeSpec as TypeSpec:
        get:
            return _typeSpec

class ArrayOfObjectNode(IdentifiedNode,IHaveChildren):
    _nodes as List[of Node]
    
    def constructor(objectId as uint, elements as List[of Node]):
        super(objectId)
        _nodes = elements
        
    Values as List[of Node]:
        get:
            return _nodes

class ArrayOfStringNode(IdentifiedNode,IHaveChildren):
    _nodes as List[of Node]
    
    def constructor(objectId as uint, elements as List[of Node]):
        super(objectId)
        _nodes = elements
        
    Values as List[of Node]:
        get:
            return _nodes
    
class ValueNode[of T](Node):
    _value as T
    
    def constructor(va as T):
        _value = va
        
    Value as T:
        get:
            return _value
            
    def ToString():
        return "ValueNode of type '${GetType().Name}' with value: '${_value.ToString()}"
    
class EndNode(Node):    
    pass
    
class StartNode(Node):
    def ToString():
        return "HEADER"
    

class ArrayFilterNode(Node):
    _value as uint
    
    def constructor(val as uint):
        _value = val
        
    NumberOfNullItems as uint:
        get:
            return _value
    
class NullNode(Node):
    pass