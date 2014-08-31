using System.Windows;

namespace BinarySerializationViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
    	
    	
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            log4net.Config.XmlConfigurator.Configure();
            
            var mainWindow = new MainWindow();
            
            var mainViewModel = new MainViewModel(new MainModel()) {FilePicker = new FilePickerViewModel()};
            //mainViewModel.FilePicker.SourcePath = "Hello";
            mainWindow.DataContext = mainViewModel;
            
            mainWindow.Show();
        }
    }
}