/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 19/08/2010
 * Time: 19:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using BinaryFormatViewer;

namespace WinFormsBSV
{
    /// <summary>
    /// Description of MainFormViewModel.
    /// </summary>
    public class MainFormViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private Node rootNode;
        
        public MainFormViewModel()
        {
        }
        
        public void LoadFile(string fileName)
        {
            using (var file = System.IO.File.OpenRead(fileName))
            {
                rootNode = new BinaryFormatReader().Read(file);
                file.Close();                    
            }
        }
        
        public Node RootNode
        {
            get { return rootNode; }
            set
            {
                if (rootNode == value) return;
                rootNode = value;
                OnPropertyChanged("RootNode");
            }
        }
        
        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
