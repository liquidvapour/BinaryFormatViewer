/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 19/08/2010
 * Time: 20:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using BinaryFormatViewer;


namespace WinFormsBSV
{
    /// <summary>
    /// Description of NodeTree.
    /// </summary>
    public partial class NodeTree : TreeView
    {
        private Node rootNode;
        
        public NodeTree()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        public Node RootNode 
        {
            get { return rootNode; }
            set
            {
                if (rootNode == value) return;
                
                
                Populate(rootNode);
            }
        }
        
        private void Populate(Node node)
        {
            
            this.Nodes.Clear();
            
            var runtimeObjectNode = node as RuntimeObjectNode;
                
            string name;
            
            if (runtimeObjectNode != null)
            {
                name = runtimeObjectNode.Name;
            
                var root = this.Nodes.Add(name);
                
                foreach(var child in runtimeObjectNode.Values)
                {
                    Populate(child, root);
                }
            }
            
        }
        
        private void Populate(Node node, TreeNode root)
        {
            
        }
    }
}
