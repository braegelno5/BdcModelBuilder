// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebberCross.BdcModelBuilder.AppModels;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public class ModelsViewModel
    {
        public delegate void OpPathChangedHandler(string path);
        public delegate void AssemblyPathChangedHandler(string path);

        public event OpPathChangedHandler OpPathChanged = null;
        public event AssemblyPathChangedHandler AssemblyPathChanged = null;

        private RelayCommand _opPathBrowseFolderCommand = null;
        private RelayCommand _assemblyPathOpenFileCommand = null;

        #region Commands

        public ICommand OpPathBrowseFolderCommand
        {
            get
            {
                if (_opPathBrowseFolderCommand == null)
                {
                    _opPathBrowseFolderCommand = new RelayCommand(
                        param => this.OpPathBrowseFolder(),
                        param => true
                        );
                }
                return _opPathBrowseFolderCommand;
            }
        }

        public ICommand AssemblyPathOpenFileCommand
        {
            get
            {
                if (_assemblyPathOpenFileCommand == null)
                {
                    _assemblyPathOpenFileCommand = new RelayCommand(
                        param => this.AssemblyPathOpenFile(),
                        param => true
                        );
                }
                return _assemblyPathOpenFileCommand;
            }
        }

        #endregion

        public ModelsViewModel()
        {

        }

        private void OpPathBrowseFolder()
        {
            // Show FolderBrowserDialog
            System.Windows.Forms.FolderBrowserDialog fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            fbDialog.Description = "Output Path";

            if (fbDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && this.OpPathChanged != null)
            {
                this.OpPathChanged(fbDialog.SelectedPath);
            }
        }

        private void AssemblyPathOpenFile()
        {
            // Show OpenFileDialog
            System.Windows.Forms.OpenFileDialog ofDialog = new System.Windows.Forms.OpenFileDialog();
            ofDialog.Title = "Assembly Path";

            if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && this.AssemblyPathChanged != null)
            {
                this.AssemblyPathChanged(ofDialog.FileName);
            }
        }
    }
}
