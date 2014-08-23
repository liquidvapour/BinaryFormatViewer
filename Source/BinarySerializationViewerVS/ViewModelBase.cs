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
	public class ViewModelBase : INotifyPropertyChanged
	{
	    public event PropertyChangedEventHandler PropertyChanged;
	    
	    protected void OnPropertyChanged(string name)
	        {
	            if (PropertyChanged != null)
	            {
	                PropertyChanged(this, new PropertyChangedEventArgs(name));
	            }
	        }	    
	}
}
