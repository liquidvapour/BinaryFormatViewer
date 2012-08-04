/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 19/08/2010
 * Time: 19:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using BinaryFormatViewer;

namespace WinFormsBSV
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        
        private MainFormViewModel viewModel;
        
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            
            viewModel = new MainFormViewModel();
            viewModel.PropertyChanged += HandlePropertyChanged;
        }
        
        private void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            BuildTree(viewModel.RootNode);
        }
        
        void Button1Click(object sender, EventArgs e)
        {
            viewModel.LoadFile(textBox1.Text);
        }
        
        private void BuildTree(Node rootNode)
        {
            var treeRootNode = treeView.Nodes.Add("Root");
            
            treeRootNode.Nodes.Add("Child 1");
            treeRootNode.Nodes.Add("Child 2");
        }
    }
}
