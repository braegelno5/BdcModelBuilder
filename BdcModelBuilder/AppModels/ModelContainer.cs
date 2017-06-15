// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class ModelContainer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Model _currentModel = null;
        private bool _editMode = false;
        private string _filePath = "";
        private Guid _currentID;
        private ObservableCollection<Model> _models = null;
        private string _opPath = "";
        private string _targetNS = "";

        [XmlIgnore]
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        [XmlIgnore]
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                if (this._editMode != value)
                {
                    this._editMode = value;

                    if (PropertyChanged != null) 
                        PropertyChanged(this, new PropertyChangedEventArgs("EditMode"));
                }                     
            }
        }

        [XmlIgnore]
        public Model CurrentModel
        {
            get 
            {
                if (this._models.Count > 0)
                {
                    _currentModel = this._models.Single(m => m.ID == this._currentID) as Model;
                }

                return _currentModel; 
            }
        }

        public Guid CurrentID
        {
            get { return _currentID; }
            set
            {
                if (this._currentID != value)
                {
                    this._currentID = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentModel"));
                        PropertyChanged(this, new PropertyChangedEventArgs("CurrentID"));
                    }
                }
            }
        }

        public ObservableCollection<Model> Models
        {
            get { return _models; }           
            set 
            {
                if (_models != value)
                {
                    _models = value;

                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs("Models"));
                }
            }
        }

        public string OpPath
        {
            get { return _opPath; }
            set 
            { 
                _opPath = value;
                foreach (Model m in _models) { m.OpPath = value; }
                
                if (PropertyChanged != null) 
                    PropertyChanged(this, new PropertyChangedEventArgs("OpPath"));
            }
        }

        public string TargetNameSpace
        {
            get { return _targetNS; }
            set
            { 
                _targetNS = value;
                foreach (Model m in _models) { m.TargetNameSpace = value; }
                
                if (PropertyChanged != null) 
                    PropertyChanged(this, new PropertyChangedEventArgs("TargetNameSpace"));
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ModelContainer()
        {
            this._models = new ObservableCollection<Model>();
        }

        // Add new model
        public void NewModel()
        {
            Model m = new Model();
            this.Models.Add(m);
            this.CurrentID = m.ID;            
        }

        /// <summary>
        /// Save settings to file
        /// </summary>
        public void Save()
        {
            // Serialize this object to file
            XmlTextWriter xWriter = new XmlTextWriter(this._filePath, Encoding.Default);
            xWriter.Formatting = Formatting.Indented;
            xWriter.Indentation = 1;
            xWriter.IndentChar = '\t';

            XmlSerializer xSerializer = new XmlSerializer(this.GetType());
            xSerializer.Serialize(xWriter, this);

            // Clear up resources
            xSerializer = null;
            xWriter.Close();
            xWriter = null;
        }
        /// <summary>
        /// Save settings to file
        /// </summary>
        /// <param name="_filePath">File path</param>
        public void Save(string filePath)
        {
            this._filePath = filePath;
            this.Save();
        }

        /// <summary>
        /// Open settings from file
        /// </summary>
        /// <param name="_filePath"></param>
        /// <returns></returns>
        public static ModelContainer Open(string filePath)
        {
            // Deserialize this object
            XmlTextReader xReader = new XmlTextReader(filePath);
            XmlSerializer xSerializer = new XmlSerializer(typeof(ModelContainer));
            ModelContainer ruleSettings = (ModelContainer)xSerializer.Deserialize(xReader);

            // Set file path
            ruleSettings.FilePath = filePath;

            // Clear up resources
            xSerializer = null;
            xReader.Close();
            xReader = null;

            return ruleSettings;
        }

        
    }
}

