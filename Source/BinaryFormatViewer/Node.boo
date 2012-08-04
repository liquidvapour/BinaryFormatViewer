namespace BinaryFormatViewer

import System
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
    _refId as uint
    
    def constructor(refId as uint):
        _refId = refId
        
    RefId as uint:
        get:
            return _refId

class AssemblyNode(IdentifiedNode):
    private _name as string
    
    public def constructor(id as uint, name as string):
        super(id)
        _name = name
            
    Name as string:
        get:
            return _name
        
class AssemblyRefNode(Node):
    private _id as uint
    
    def constructor(id as uint):
        _id = id
        
    Id as uint:
        get:
            return _id
        
class IdentifiedNode(Node):
    _id as uint

    def constructor(id as uint):
        _id = id

    Id as uint:
        get:
            return _id

class RuntimeObjectNode(IdentifiedNode, IHaveChildren, IHaveTypeSpecs):

    [Property(Assembly)]
    _assembly as Node

    _fieldValues as List[of FieldNode]
    _name as string
    
    def constructor(id as uint, name as string, fields as List[of FieldNode]):
        self(id, name, fields, null)
    
    def constructor(id as uint, name as string, fields as List[of FieldNode], assembly as Node):
        super(id)
        _fieldValues = fields
        _name = name
        _assembly = assembly
        
#    def constructor(id as uint, name as string, fields as Dictionary[of string, Node], typeSpecs as List[of TypeSpec]):
#        fieldsList = List[of KeyValuePair[of string, Node]](fields)
#        
#        fieldValues = List[of Node]()
#        
#        for i in range(fieldsList.Count):
#            fieldValues.Add(FieldNode(fieldsList[i].Key, fieldsList[i].Value, typeSpecs[i]))
#
#        self(id, name, fieldValues)
        
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
        
class ObjectNode(RuntimeObjectNode):
    def constructor(id as uint, name as string, assemblyId as uint, fields as List[of FieldNode]):
        super(id, name, fields, AssemblyRefNode(assemblyId))
        

class StringNode(IdentifiedNode):
    _val as string
    
    def constructor(objectId as uint, val as string):
        super(objectId)
        _val = val
                    
    Value as string:
        get:
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
    
class EndNode(Node):    
    pass
    
class StartNode(Node):
    pass

class ArrayFilterNode(Node):
    _value as uint
    
    def constructor(val as uint):
        _value = val
        
    NumberOfNullItems as uint:
        get:
            return _value
    
class NullNode(Node):
    pass