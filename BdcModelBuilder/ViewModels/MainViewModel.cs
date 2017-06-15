// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebberCross.BdcModelBuilder.AppModels;
using WebberCross.BdcModelBuilder.Builders;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RequestClose;

        private ModelContainer _mContainer = null;        
        private Builder _builder = null;

        private RelayCommand _newFileCommand = null;
        private RelayCommand _openCommand = null;
        private RelayCommand _saveCommand = null;
        private RelayCommand _saveAsCommand = null;
        private RelayCommand _exitCommand = null;
        private RelayCommand _newModelCommand = null;
        private RelayCommand _editCommand = null;
        private RelayCommand _writeCommand = null;
        private RelayCommand _writeAllCommand = null;
        private RelayCommand _deleteCommand = null;
        private RelayCommand _refreshCommand = null;

        private ModelsViewModel _mvm = null;
        private EntityDetailsViewModel _edvm = null;
        private MethodDetailsViewModel _mdvm = null;
        private DataSourceViewModel _dsvm = null;
        
        public ModelContainer ModelContainer
        {
            get { return this._mContainer; }
            set
            {
                if (value == null) return;

                if (this._mContainer != value)
                {
                    this._mContainer = value;

                    if (this.PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ModelContainer"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("EntityCode"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("BDCMCode"));
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ServiceCode"));
                    }
                }
                
            }
        }

        public ModelsViewModel MVM
        {
            get { return _mvm; }
        }

        public EntityDetailsViewModel EDVM
        {
            get { return _edvm; }
        }

        public MethodDetailsViewModel MDVM
        {
            get { return _mdvm; }
        }

        public DataSourceViewModel DSVM
        {
            get { return _dsvm; }
        }

        public List<string> EntityCode
        {
            get 
            {
                Builder builder = new Builder(this._mContainer);

                return builder.BuildEntity(false); ; 
            }
        }

        public List<string> BDCMCode
        {
            get 
            {
                Builder builder = new Builder(this._mContainer);

                return builder.BuildBDCM(false); 
            }
        }

        public List<string> ServiceCode
        {
            get
            {
                Builder builder = new Builder(this._mContainer);

                return builder.BuildEntityService(false);
            }
        }

        #region Commands

        public ICommand NewFileCommand
        {
            get
            {
                if (_newFileCommand == null)
                {
                    _newFileCommand = new RelayCommand(
                        param => this.NewFile(),
                        param => true
                        );
                }
                return _newFileCommand;
            }
        }

        public ICommand OpenCommand
        {
            get
            {
                if (_openCommand == null)
                {
                    _openCommand = new RelayCommand(
                        param => this.OpenFile(),
                        param => true
                        );
                }
                return _openCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                        param => this.SaveSettings(false),
                        param => true
                        );
                }
                return _saveCommand;
            }
        }

        public ICommand SaveAsCommand
        {
            get
            {
                if (_saveAsCommand == null)
                {
                    _saveAsCommand = new RelayCommand(
                        param => this.SaveSettings(true),
                        param => true
                        );
                }
                return _saveAsCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (_exitCommand == null)
                {
                    _exitCommand = new RelayCommand(
                        param => this.Exit(),
                        param => true
                        );
                }
                return _exitCommand;
            }
        }

        public ICommand NewModelCommand
        {
            get
            {
                if (_newModelCommand == null)
                {
                    _newModelCommand = new RelayCommand(
                        param => this.NewModel(),
                        param => true
                        );
                }
                return _newModelCommand;
            }
        }

        public ICommand EditCommand
        {
            get
            {
                if (_editCommand == null)
                {
                    _editCommand = new RelayCommand(
                        param => this.StartEdit(),
                        param => true
                        );
                }
                return _editCommand;
            }
        }

        public ICommand WriteCommand
        {
            get
            {
                if (_writeCommand == null)
                {
                    _writeCommand = new RelayCommand(
                        param => this.WriteModel(),
                        param => true
                        );
                }
                return _writeCommand;
            }
        }

        public ICommand WriteAllCommand
        {
            get
            {
                if (_writeAllCommand == null)
                {
                    _writeAllCommand = new RelayCommand(
                        param => this.WriteAllModels(),
                        param => true
                        );
                }
                return _writeAllCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                {
                    _deleteCommand = new RelayCommand(
                        param => this.DeleteModel(),
                        param => true
                        );
                }
                return _deleteCommand;
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                if (_refreshCommand == null)
                {
                    _refreshCommand = new RelayCommand(
                        param => this.RefreshCode(),
                        param => true
                        );
                }
                return _refreshCommand;
            }
        }

        #endregion

        public MainViewModel()
        {
            // Create View Models
            // Models ViewModel
            this._mvm = new ModelsViewModel();
            this._mvm.AssemblyPathChanged += new ModelsViewModel.AssemblyPathChangedHandler(_mvm_AssemblyPathChanged);
            this._mvm.OpPathChanged += new ModelsViewModel.OpPathChangedHandler(_mvm_OpPathChangedHandler);

            // EntityDetails ViewModel
            this._edvm = new EntityDetailsViewModel();
            this._edvm.EntityDetailsChanged += new EntityDetailsChangedHandler(_edvm_EntityDetailsChanged);

            // MethodDetails ViewModel
            this._mdvm = new MethodDetailsViewModel();

            // DataSource ViewModel
            this._dsvm = new DataSourceViewModel();
            this._dsvm.DataSourceChanged += new DataSourceChangedHandler(_dsvm_DataSourceChanged);

            // Create CurrentModel Contrainer
            this._mContainer = new ModelContainer();            
        }

        #region View Model Event Handlers

        private void _mvm_AssemblyPathChanged(string path)
        {
            if (this._mContainer != null && this._mContainer.CurrentModel != null)
                this._mContainer.CurrentModel.AssemblyPath = path;

            this._edvm.AssemblyPath = path;
            this._mdvm.AssemblyPath = path;
            this._dsvm.AssemblyPath = path;
        }

        private void _mvm_OpPathChangedHandler(string path)
        {
            this._mContainer.OpPath = path;
        }

        private void _edvm_EntityDetailsChanged(EntityDetails details)
        {
            if (this._mContainer != null && this._mContainer.CurrentModel != null)
                this._mContainer.CurrentModel.EntityDetails = details;
        }

        private void _dsvm_DataSourceChanged(InstanceNode source)
        {
            if(this._mContainer != null && this._mContainer.CurrentModel != null)
                this._mContainer.CurrentModel.DataSourceInstance = source;
        }

        #endregion

        // Load settings from file
        public void LoadSettings(string[] args)
        {
            // Get path from args
            string filePath = "";
            if (args != null && args.Length > 0)
                filePath = args[0];

            this.LoadSettings(filePath);
        }

        public void LoadSettings(string filePath)
        {
            try
            {                
                // Get path from settings
                if (filePath == "") filePath = Properties.Settings.Default.FilePath;

                if (filePath == "") 
                    this.ModelContainer = new ModelContainer();
                else
                {
                    if (File.Exists(filePath))
                    {
                        this.ModelContainer = ModelContainer.Open(filePath);

                        this._edvm.AssemblyPath = this._mContainer.CurrentModel.AssemblyPath;
                        this._mdvm.AssemblyPath = this._mContainer.CurrentModel.AssemblyPath;
                        this._dsvm.AssemblyPath = this._mContainer.CurrentModel.AssemblyPath;
                    }
                    else
                    {
                        string warning = "Unable to load: " + filePath;
                        MessageBox.Show(warning, "WebberCross.BDCModelBuilder", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        /// <summary>
        /// Save settings
        /// </summary>
        /// <param name="prompt">Prompt user for destination</param>
        public void SaveSettings(bool prompt)
        {
            try
            {
                // Add model if new
                if (this._mContainer.Models.Where(m => m.ID == this._mContainer.CurrentModel.ID).Count() == 0)
                {
                    this._mContainer.Models.Add(this._mContainer.CurrentModel);
                }

                // Save to file
                if (this._mContainer.FilePath == "" || prompt)
                {
                    // Get a path from user
                    System.Windows.Forms.SaveFileDialog sfDialog = new System.Windows.Forms.SaveFileDialog();
                    sfDialog.DefaultExt = "bdcmb";
                    sfDialog.Filter = "BDC Model Builder Files|*.bdcmb";
                    if (sfDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this._mContainer.FilePath = sfDialog.FileName;

                        // Update settings
                        Properties.Settings.Default.FilePath = sfDialog.FileName;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        return;
                    }
                }

                // Save settings
                this._mContainer.Save();

                // Finish edit
                this.EndEdit();
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void StartEdit()
        {
            this.ModelContainer.EditMode = true;
        }

        private void EndEdit()
        {
            this.ModelContainer.EditMode = false;
        }

        private void ErrorHandler(Exception ex)
        {
            MessageBox.Show(ex.ToString(), "Webber-Cross.BDCModelGen Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void NewFile()
        {
            try
            {
                this.ModelContainer = new ModelContainer();
                this.NewModel();

                this.StartEdit();
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void NewModel()
        {
            try
            {
                this.ModelContainer.NewModel();

                this.StartEdit();
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void OpenFile()
        {
            try
            {
                // Load settings from file
                System.Windows.Forms.OpenFileDialog ofDialog = new System.Windows.Forms.OpenFileDialog();
                if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.LoadSettings(ofDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void WriteModel()
        {
            try
            {
                if (this._mContainer.CurrentModel != null)
                {
                    this._builder = new Builder(_mContainer);
                    this._builder.BuildEntity(this._mContainer.CurrentModel, true);
                    this._builder.BuildBDCM(this._mContainer.CurrentModel, true);
                    this._builder.BuildEntityService(this._mContainer.CurrentModel, true);
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void WriteAllModels()
        {
            try
            {
                foreach (Model m in this._mContainer.Models)
                {
                    if (!m.Output) continue;

                    this._builder = new Builder(_mContainer);
                    this._builder.BuildEntity(m, true);
                    this._builder.BuildBDCM(m, true);
                    this._builder.BuildEntityService(m, true);
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void DeleteModel()
        {
            try
            {
                string message = string.Format("Delete CurrentModel: {0}?", this._mContainer.CurrentModel.Name);

                if (MessageBox.Show(message, "Webber-Cross.BDCModelGen"
                    , MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
                {
                    // Delete current model from container
                    this._mContainer.Models.Remove(this._mContainer.CurrentModel);
                    if (this._mContainer.Models.Count > 0)
                    {
                        this._mContainer.CurrentID = this._mContainer.Models[0].ID;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ex);
            }
        }

        private void RefreshCode()
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs("EntityCode"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("BDCMCode"));
            this.PropertyChanged(this, new PropertyChangedEventArgs("ServiceCode"));
        }

        private void Exit()
        {
            if (this.RequestClose != null)
                this.RequestClose(this, null);
        }       
    }
}
