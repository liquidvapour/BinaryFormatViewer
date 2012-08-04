/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 25/08/2010
 * Time: 09:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
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
	        if (node != null)
	        {
	            Name = this.node.GetType().FullName;
	        }
	        else
	        {
	            Name = "Child";
	        }
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
