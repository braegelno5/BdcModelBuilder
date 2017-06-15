using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using WebberCross.BdcModelBuilder.AppModels;
using WebberCross.BdcModelBuilder.Builders;
using WebberCross.BdcModelBuilder.TabControls;

namespace WebberCross.BdcModelBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private ModelContainer _mContainer = null;
        //private CurrentModel _currentModel = null;
        //private Builder _builder = null;

        public MainWindow()
        {
            InitializeComponent();

            //// Create CurrentModel Contrainer
            //this._mContainer = new ModelContainer();
            //this.modelsTabControl.ModelContainer = this._mContainer;

            //// Save events
            //this.modelsTabControl.ModelsTabSaveRequest += new TabControls.ModelsTab.SaveRequest(modelsTabControl_ModelsTabSaveRequest);
            //this.modelsTabControl.ModelsTabCurrentModelChanged += new TabControls.ModelsTab.CurrentModelChanged(modelsTabControl_ModelsTabCurrentModelChanged);

            //// Load settings
            //this.LoadSettings(args);
        }

        //private void modelsTabControl_ModelsTabCurrentModelChanged(CurrentModel model)
        //{
        //    this._currentModel = model;
        //    this.UpdateCurrentModels();
        //}

        //private void modelsTabControl_ModelsTabSaveRequest()
        //{
        //    this.SaveSettings(false);
        //}

        //// Load settings from file
        //private void LoadSettings(string[] args)
        //{
        //    // Get path from args
        //    string _filePath = "";
        //    if (args != null && args.Length > 0)
        //        _filePath = args[0];

        //    this.LoadSettings(_filePath);
        //}

        //private void LoadSettings(string _filePath)
        //{
        //    try
        //    {                
        //        // Get path from settings
        //        if (_filePath == "") _filePath = Properties.Settings.Default.FilePath;

        //        if (_filePath == "") 
        //            _mContainer = new ModelContainer();
        //        else
        //        {
        //            if (File.Exists(_filePath))
        //            {
        //                this._mContainer = ModelContainer.Open(_filePath);

        //                foreach (CurrentModel m in this._mContainer.Models)
        //                {
        //                    if (m.ID == this._mContainer.CurrentID)
        //                    {
        //                        this._currentModel = m;
        //                        break;
        //                    }
        //                }

        //                // Assign to Controls
        //                this.UpdateCurrentModels();
        //                this.UpdateContainers();
        //            }
        //            else
        //            {
        //                string warning = "Unable to load: " + _filePath;
        //                MessageBox.Show(warning, "WebberCross.BDCModelBuilder", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        ///// <summary>
        ///// Save settings
        ///// </summary>
        ///// <param name="prompt">Prompt user for destination</param>
        //private void SaveSettings(bool prompt)
        //{
        //    try
        //    {
        //        this.SetWaitCursor();

        //        // Add model if new
        //        if (this._mContainer.Models.Where(m => m.ID == this._currentModel.ID).Count() == 0)
        //        {
        //            this._mContainer.Models.Add(this._currentModel);
        //        }
        //        this._mContainer.CurrentID = this._currentModel.ID;

        //        // Save to file
        //        if (this._mContainer.FilePath == "" || prompt)
        //        {
        //            // Get a path from user
        //            System.Windows.Forms.SaveFileDialog sfDialog = new System.Windows.Forms.SaveFileDialog();
        //            if (sfDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //            {
        //                this._mContainer.FilePath = sfDialog.FileName;
        //            }
        //            else
        //            {
        //                this.ResetCursor();
        //                return;
        //            }
        //        }

        //        // Save settings
        //        this._mContainer.Save();

        //        // Finish edit
        //        this.EndEdit();

        //        this.ResetCursor();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //// Sets the cursor to wait symbol
        //public void SetWaitCursor()
        //{
        //    this.Cursor = Cursors.Wait;
        //}

        //// Reset cursor
        //public void ResetCursor()
        //{
        //    this.Cursor = Cursors.Arrow;
        //}

        //private void StartEdit()
        //{
        //    this._mContainer.EditMode = true;
        //    this._currentModel.EditMode = true;
        //}

        //private void EndEdit()
        //{
        //    this._mContainer.EditMode = false;
        //    this._currentModel.EditMode = false;
        //}

        //private void UpdateCurrentModels()
        //{
        //    try
        //    {
        //        this.sslModel.Text = string.Format("Current CurrentModel: {0}", this._currentModel.Name);

        //        this.dataSourceTabControl.CurrentModel = this._currentModel;
        //        this.entityTabControl.CurrentModel = this._currentModel;
        //        this.methodsTabControl.CurrentModel = this._currentModel;
        //        this.modelsTabControl.CurrentModel = this._currentModel;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void UpdateContainers()
        //{
        //    try
        //    {
        //        this.modelsTabControl.ModelContainer = this._mContainer;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void ErrorHandler(Exception ex)
        //{
        //    this.ResetCursor();

        //    MessageBox.Show(ex.ToString(), "Webber-Cross.BDCModelGen Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        //}

        //private void MenuItem_Click(object sender, RoutedEventArgs e)
        //{
        //    MenuItem ci = sender as MenuItem;
        //    switch (ci.Header.ToString())
        //    {
        //        case "New":
        //            // Create new model
        //            this.NewModel();
        //            break;

        //        case "Open":
        //            this.OpenFile();
        //            break;

        //        case "Save":
        //            this.SaveSettings(false);
        //            break;

        //        case "Save As":
        //            this.SaveSettings(true);
        //            break;

        //        case "Exit":
        //            this.Exit();
        //            break;

        //        case "Edit":
        //            if (this._currentModel != null)
        //                this.StartEdit();
        //            break;

        //        case "Write":
        //            this.WriteModel();
        //            break;

        //        case "Write All":
        //            this.WriteAllModels();
        //            break;

        //        case "Delete":
        //            this.DeleteModel();
        //            break;
        //    }
        //}

        //private void NewModel()
        //{
        //    try
        //    {
        //        if (this._currentModel != null)
        //            this.SaveSettings(false);

        //        this._currentModel = new CurrentModel();
        //        this._mContainer.CurrentID = this._currentModel.ID;
        //        this._currentModel.Methods = new MethodSet();

        //        this.UpdateCurrentModels();

        //        this.StartEdit();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void OpenFile()
        //{
        //    try
        //    {
        //        this.SetWaitCursor();

        //        // Load settings from file
        //        System.Windows.Forms.OpenFileDialog ofDialog = new System.Windows.Forms.OpenFileDialog();
        //        if (ofDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //        {
        //            this.LoadSettings(ofDialog.FileName);
        //        }

        //        this.ResetCursor();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void WriteModel()
        //{
        //    try
        //    {
        //        if (_currentModel != null)
        //        {
        //            this._builder = new Builder(_mContainer);
        //            this._builder.BuildEntity(_currentModel, true);
        //            this._builder.BuildBDCM(_currentModel, true);
        //            this._builder.BuildEntityService(_currentModel, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void WriteAllModels()
        //{
        //    try
        //    {
        //        foreach (CurrentModel m in this._mContainer.Models)
        //        {
        //            if (!m.Output) continue;

        //            this._builder = new Builder(_mContainer);
        //            this._builder.BuildEntity(m, true);
        //            this._builder.BuildBDCM(m, true);
        //            this._builder.BuildEntityService(m, true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void DeleteModel()
        //{
        //    try
        //    {
        //        string message = string.Format("Delete CurrentModel: {0}?", this._currentModel.Name);

        //        if (MessageBox.Show(message, "Webber-Cross.BDCModelGen"
        //            , MessageBoxButton.OKCancel, MessageBoxImage.Exclamation) == MessageBoxResult.OK)
        //        {
        //            // Delete current model from container
        //            this._mContainer.Models.Remove(this._currentModel);
        //            if (this._mContainer.Models.Count > 0)
        //            {
        //                this._currentModel = this._mContainer.Models[0];
        //                this._mContainer.CurrentID = this._currentModel.ID;
        //            }

        //            // Re-bind
        //            this.UpdateCurrentModels();
        //            // this.UpdateContainers();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.ErrorHandler(ex);
        //    }
        //}

        //private void entityPreviewTabControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    CodeViewTab cvt = sender as CodeViewTab;
        //    this._builder = new Builder(_mContainer);
        //    cvt.BindData(this._builder.BuildEntity(_currentModel, false));
        //}

        //private void bdcmPreviewTabControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    CodeViewTab cvt = sender as CodeViewTab;
        //    this._builder = new Builder(_mContainer);
        //    cvt.BindData(this._builder.BuildBDCM(_currentModel, false));
        //}

        //private void servicePreviewTabControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    CodeViewTab cvt = sender as CodeViewTab;
        //    this._builder = new Builder(_mContainer);
        //    cvt.BindData(this._builder.BuildEntityService(_currentModel, false));
        //}

    }
}
