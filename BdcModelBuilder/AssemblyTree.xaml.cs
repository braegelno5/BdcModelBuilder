using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

using WebberCross.BdcModelBuilder.AppModels;
using System.Collections.ObjectModel;
using WebberCross.BdcModelBuilder.ViewModels;

namespace WebberCross.BdcModelBuilder
{
    /// <summary>
    /// Interaction logic for AssemblyTree.xaml
    /// </summary>
    public partial class AssemblyTree : Window
    {
        private AssemblyViewModel _avm = null;

        public AssemblyViewModel ViewModel
        {
            get { return this._avm; }
        }


        public AssemblyTree(string assemblyPath)
        {
            InitializeComponent();

            this._avm = new AssemblyViewModel(assemblyPath);
            base.DataContext = _avm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}