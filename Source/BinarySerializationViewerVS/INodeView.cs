using System.Collections.Generic;
using BinaryFormatViewer;

namespace BinarySerializationViewer
{
	public interface INodeViewModel
	{
	    IEnumerable<INodeItemViewModel> Nodes { get;  }
	}

	public interface INodeItemViewModel
	{
	    string Name { get;  }
	}
	
	public class NodeItemViewModel : INodeItemViewModel
	{
	    private Node node;
	    
	    public NodeItemViewModel(Node node)
	    {
	        this.node = node;
	        Name = node != null ? this.node.GetType().FullName : "Child";
	    }

	    public string Name { get; private set; }
	    
	    public IEnumerable<INodeItemViewModel> Children
	    {
	        get { return new List<INodeItemViewModel>(new INodeItemViewModel[]{ new NodeItemViewModel(null) }); }
	    }
	}
	
	public class NodeViewModel : INodeViewModel
	{
	    private IEnumerable<INodeItemViewModel> nodes;
	    
	    public NodeViewModel(Node rootNode)
	    {
	        nodes = new List<INodeItemViewModel>(new INodeItemViewModel[] { new NodeItemViewModel(rootNode) });
	    }
	    
	    public IEnumerable<INodeItemViewModel> Nodes
	    { 
	        get { return nodes; } 
        }
	}
}
