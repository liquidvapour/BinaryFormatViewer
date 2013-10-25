namespace BinaryFormatViewer

import System
import System.Collections.Generic

class BinaryFormatterOutput:
    [Property(MainNode)]
    _mainNode as Node
    
    [Property(Assemblies)]
    _assemblies as List[of AssemblyNode]
    
    [Property(IdentifiedNodes)]
    _identifiedNodes as IEnumerable[of IdentifiedNode]
    
    public def constructor(mainNode as Node, assemblies as IEnumerable[of AssemblyNode], identifiedNodes as IEnumerable[of IdentifiedNode]):
        _mainNode = mainNode
        _assemblies = List[of AssemblyNode](assemblies)
        _identifiedNodes = List[of IdentifiedNode](identifiedNodes)
    

class BinaryFormatReader:
    private static logger = log4net.LogManager.GetLogger(BinaryFormatReader)
    
    private _partProvider = PartProvider()
    

    def ReadFull(stream as System.IO.Stream):
        logger.Info("Start ReadFull")	
        nodes = List[of Node]()
        context = ReadContext()
        
        
        using reader = System.IO.BinaryReader(stream):
            while true:
                node = _partProvider.ReadNextPart(reader, context)
                identifiedNode = node as IdentifiedNode
                
                if (identifiedNode != null):
                    context.ExistingObjects.Add(identifiedNode.Id, identifiedNode)
                    
                nodes.Add(node)
                if node isa EndNode: 
                    break;
                
            reader.Close()
        
        idNodes = BuildObjectIdHash(nodes)
        assembliesById = BuildAssemblyHash(nodes)
        identifiedNodes = GetIdentifiedNodes(nodes)
        ResolveAssemblyReferences(nodes, assembliesById)
        ResolveReferences(nodes, idNodes)
        
        return BinaryFormatterOutput(GetFirstObjectNode(nodes), GetAssemblyList(assembliesById), identifiedNodes)
        
    def GetAssemblyList(assembliesHash as Boo.Lang.Hash) as IEnumerable[of AssemblyNode]:
        #for a in assembliesHash.
        for a in assembliesHash.Values:
            yield a
            
    
    def Read(stream as System.IO.Stream):        
        return ReadFull(stream).MainNode
        
    private def BuildAssemblyHash(nodes as List[of Node]) as Hash:
        result = {}
        
        onEachNode = do(n as Node):
            if n isa AssemblyNode:
                assemblyNode = n as AssemblyNode
                result.Add(assemblyNode.Id, assemblyNode)
        
        ForEachNodeIn(nodes, onEachNode)
        
        return result
        
    private def ResolveAssemblyReferences(nodes as List[of Node], assembliesById as Hash):
        pass
        
    private def GetFirstObjectNode(nodes as List[of Node]):
        for i in range(nodes.Count):
            if not (nodes[i] isa AssemblyNode or nodes[i] isa StartNode):
                return nodes[i]

    private def GetIdentifiedNodes(nodes as List[of Node]):
        identifiedNodes = List[of IdentifiedNode]()
        
        addNodeToList = do(x as IdentifiedNode): 
            identifiedNodes.Add(x)
            
        ForEachIdentifiedNode(nodes, addNodeToList)
        return identifiedNodes
        

    private def BuildObjectIdHash(nodes as List[of Node]):
        result = {}
        
        ForEachIdentifiedNode(nodes, {x| result.Add(x.Id, x) if not result.ContainsKey(x.Id)})
        
        return result

    private def ForEachIdentifiedNode(nodes as IEnumerable[of Node], expr as callable(IdentifiedNode)):
        onEachNode = do(n as Node):
            if n isa IdentifiedNode:
                idNode = n as IdentifiedNode
                expr(idNode)
        
        ForEachNodeIn nodes, onEachNode

    private def ForEachNodeIn(nodes as IEnumerable[of Node], onEachNode as callable(Node)):
        if not onEachNode:
            raise ArgumentNullException("onEachNode")
        
        for node in nodes:
            onEachNode(node)
            if node isa IHaveChildren:
                parent = node as IHaveChildren
                ForEachIdentifiedNode(parent.Values, onEachNode)
    
    private def ResolveReferences(nodeList as IList[of Node], idNodes as Hash):
        resolves = 0
        done = false
        while not done:
            ResolveReferences(nodeList, idNodes, Stack[of Node](), resolves)
            if resolves == 0:
                done = true
            else:
                resolves = 0
        
    private def ResolveReferences(nodeList as IList[of FieldNode], idNodes as Hash, resolutionStack as Stack[of Node], ref resolves as int):        
        for i in range(nodeList.Count):
            if nodeList[i].Value isa ArrayOfStringNode:
                arrayOfStringNode as ArrayOfStringNode = nodeList[i].Value
                if not resolutionStack.Contains(arrayOfStringNode):
                    resolutionStack.Push(arrayOfStringNode)
                    ResolveReferences(arrayOfStringNode.Values, idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i].Value isa GenericArrayNode:
                genericArrayNode as GenericArrayNode = nodeList[i].Value
                if not resolutionStack.Contains(genericArrayNode):
                    resolutionStack.Push(genericArrayNode)
                    ResolveReferences(genericArrayNode.Values, idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i].Value isa ObjectNode:
                objectNode = nodeList[i].Value as ObjectNode
                if not resolutionStack.Contains(objectNode):
                    resolutionStack.Push(objectNode)
                    ResolveReferences(objectNode.Fields as IList[of FieldNode], idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i].Value isa ObjectReferenceNode:
                objectRefNode = nodeList[i].Value as ObjectReferenceNode
                resolves++
                nodeList[i].Value = idNodes[objectRefNode.RefId]
    
    
    private def ResolveReferences(nodeList as IList[of Node], idNodes as Hash, resolutionStack as Stack[of Node], ref resolves as int):        
        for i in range(nodeList.Count):
            if nodeList[i] isa ArrayOfStringNode:
                arrayOfStringNode as ArrayOfStringNode = nodeList[i] 
                if not resolutionStack.Contains(arrayOfStringNode):
                    resolutionStack.Push(arrayOfStringNode)
                    ResolveReferences(arrayOfStringNode.Values, idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i] isa GenericArrayNode:
                genericArrayNode as GenericArrayNode = nodeList[i]
                if not resolutionStack.Contains(genericArrayNode):
                    resolutionStack.Push(genericArrayNode)
                    ResolveReferences(genericArrayNode.Values, idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i] isa ObjectNode:
                objectNode = nodeList[i] as ObjectNode
                if not resolutionStack.Contains(objectNode):
                    resolutionStack.Push(objectNode)
                    ResolveReferences(objectNode.Fields as IList[of FieldNode], idNodes, resolutionStack, resolves)
                    resolutionStack.Pop()
            if nodeList[i] isa ObjectReferenceNode:
                objectRefNode = nodeList[i] as ObjectReferenceNode
                resolves++
                nodeList[i] = idNodes[objectRefNode.RefId]
