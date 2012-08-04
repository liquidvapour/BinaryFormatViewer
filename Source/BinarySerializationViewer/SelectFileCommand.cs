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
	public sealed class SelectFileCommand : ICommand
	{
	    public event EventHandler CanExecuteChanged;
	
	    private IFilePickerViewModel vm;
	    private bool canExecute;
	
	    public SelectFileCommand(IFilePickerViewModel vm)
	    {
	        this.vm = vm;
	        this.canExecute = true;
	    }
	    
	    public bool CanExecute(object parameter)
	    {
	        return canExecute;
	    }
	    
	    public void Execute(object parameter)
	    {
	        SetCanExecute(false);
	        try
	        {
	    	        // Configure open file dialog box
	                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
	                dlg.FileName = ""; // Default file name
	                dlg.DefaultExt = ".bin"; // Default file extension
	                dlg.Filter = "Binary file (.bin)|*.bin"; // Filter files by extension
	                
	                // Show open file dialog box
	                Nullable<bool> result = dlg.ShowDialog();
	                
	                // Process open file dialog box results
	                if (result == true)
	                {
	                    // Open document
	                    vm.SourcePath = dlg.FileName;
	                }
	        }
	        finally
	        {
	            SetCanExecute(true);
	        }
	    }
	    
	    private void SetCanExecute(bool canExecute)
	    {
	        this.canExecute = canExecute;
	        OnCanExecuteChanged();
	    }
	    
	    private void OnCanExecuteChanged()
	    {
	        if (CanExecuteChanged != null)
	        {
	            CanExecuteChanged(this, EventArgs.Empty);
	        }
	    }
	}
}
