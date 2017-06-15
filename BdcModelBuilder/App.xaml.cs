using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using WebberCross.BdcModelBuilder.ViewModels;

namespace WebberCross.BdcModelBuilder
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mw = null;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create main view model
            MainViewModel mvm = new MainViewModel();
            mvm.RequestClose += new EventHandler(mvm_RequestClose);
            mvm.LoadSettings(e.Args);
            
            // Create main window
            this.mw = new MainWindow();
            mw.DataContext = mvm;
            
            // Show main window
            mw.ShowDialog();
            
            this.Shutdown();
        }

        private void mvm_RequestClose(object sender, EventArgs e)
        {
            if (this.mw != null)
                this.mw.Close();
        }
    }
}
