// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using WebberCross.BdcModelBuilder.AppModels;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public delegate void DataSourceChangedHandler(InstanceNode source);

    public class DataSourceViewModel
    {
        private RelayCommand _dataourceCommand = null;
        private string _assemblyPath = "";

        public event DataSourceChangedHandler DataSourceChanged = null;

        public string AssemblyPath
        {
            set { this._assemblyPath = value; }
        }

        #region Commands

        public ICommand DataSourceCommand
        {
            get
            {
                if (_dataourceCommand == null)
                {
                    _dataourceCommand = new RelayCommand(
                        param => this.EditDataSource(),
                        param => true
                        );
                }
                return _dataourceCommand;
            }
        }

        #endregion

        private void EditDataSource()
        {
            AssemblyTree tree = new AssemblyTree(this._assemblyPath);
            tree.ShowDialog();

            if (tree.DialogResult.HasValue && tree.DialogResult.Value && this.DataSourceChanged != null)
            {
                InstanceNode source = tree.ViewModel.GetInstanceRoot();
                this.DataSourceChanged(source);
            }
        }
    }
}
