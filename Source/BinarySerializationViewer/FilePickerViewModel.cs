/*
 * Created by SharpDevelop.
 * User: ra-el
 * Date: 25/08/2010
 * Time: 09:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Windows.Input;
using BinaryFormatViewer;

namespace BinarySerializationViewer
{
	public sealed class FilePickerViewModel : ViewModelBase, IFilePickerViewModel
	{
	    public event EventHandler FileSelected;
	    
	    private string sourcePath = string.Empty;
	    
	    public ICommand SelectFile { get { return new SelectFileCommand(this); } }
	    
	    public string SourcePath 
	    { 
	        get { return sourcePath; }
	        set 
	        {
	            if (sourcePath == value) return;
	            
	            sourcePath = value;
	            OnPropertyChanged("SourcePath");
	            OnFileSelected();
	        }
	    }
	    
	    private void OnFileSelected()
	    {
	        if (FileSelected != null)
	        {
	            FileSelected(this, EventArgs.Empty);
	        }
	    }
	}
}
