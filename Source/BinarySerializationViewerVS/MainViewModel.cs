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
using System.Linq;
using System.Windows.Input;
using BinaryFormatViewer;

namespace BinarySerializationViewer
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private IFilePickerViewModel filePicker;
        private Node nodeView;
        private MainModel mainModel;
        private IList<AssemblyNode> _assemblies;
        private IList<IdentifiedNode> _identifiedNodes; 
        
        public MainViewModel(MainModel mainModel)
        {
            this.mainModel = mainModel;
        }
        
        public IFilePickerViewModel FilePicker 
        { 
            get { return filePicker; } 
            set 
            {
                if (filePicker == value) return;
                
                filePicker = value;
                filePicker.FileSelected += FilePickerFileSelected;
                OnPropertyChanged("FilePicker");
            }
        }
        
        public Node NodeViewModel
        {
            get { return nodeView; }
            set 
            {
                if (nodeView == value) return;
                
                nodeView = value;
                OnPropertyChanged("NodeViewModel");
                OnPropertyChanged("Nodes");
            }
        }
        
        public IList<AssemblyNode> Assemblies
        {
            get { return _assemblies; }
            private set
            {
                if (_assemblies == value) return;
                
                _assemblies = value;
                OnPropertyChanged("Assemblies");
            }
        }
        
        public IList<IdentifiedNode> IdentifiedNodes
        {
            get { return _identifiedNodes; }
            private set 
            {
                if (_identifiedNodes == value) return;
                
                _identifiedNodes = value;
                OnPropertyChanged("IdentifiedNodes");
            }
        }
        
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        
        private void FilePickerFileSelected(object sender, EventArgs args)
        {
            var res = mainModel.LoadFile(filePicker.SourcePath);
            NodeViewModel = res.MainNode;
            Assemblies = res.Assemblies;
            IdentifiedNodes = new List<IdentifiedNode>(res.IdentifiedNodes.OrderBy(x => x.Id));
        }
    }
}
