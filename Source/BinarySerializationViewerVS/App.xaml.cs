using System;
using System.Windows;
using System.Data;
using System.Xml;
using System.Configuration;

namespace BinarySerializationViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    	
    	
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            log4net.Config.XmlConfigurator.Configure();
            
            var mainWindow = new MainWindow();
            
            var mainViewModel = new MainViewModel(new MainModel());
            mainViewModel.FilePicker = new FilePickerViewModel();
            //mainViewModel.FilePicker.SourcePath = "Hello";
            mainWindow.DataContext = mainViewModel;
            
            mainWindow.Show();
        }
    }
}