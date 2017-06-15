// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebberCross.BdcModelBuilder.AppModels;
using System.Windows.Input;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public class MethodDetailsViewModel
    {
        private MethodDetails _selectedDetails = null;
        private string _assemblyPath = "";

        private RelayCommand _methodDetailsCommand = null;

        public MethodDetails SelectedDetails
        {
            set { this._selectedDetails = value; }
            get { return this._selectedDetails; }
        }

        public string AssemblyPath
        {
            set { this._assemblyPath = value; }
        }

        #region Commands

        public ICommand MethodDetailsCommand
        {
            get
            {
                if (_methodDetailsCommand == null)
                {
                    _methodDetailsCommand = new RelayCommand(
                        param => this.EditMethodDetails(),
                        param => true
                        );
                }
                return _methodDetailsCommand;
            }
        }

        #endregion

        private void EditMethodDetails()
        {
            AssemblyTree _tree = new AssemblyTree(this._assemblyPath);
            _tree.ShowDialog();

            if (_tree.DialogResult.HasValue && _tree.DialogResult.Value)
            {
                this._selectedDetails = _tree.ViewModel.GetMethodDetails(this._selectedDetails);
            }
        }
    }
}
