// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class Model : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _assemblyPath = "";
        private string _opPath = "";
        private string _name = "";
        private string _modelNS = "";
        private string _targetNS = "";
        private bool _output = true;
        private Guid _id;
        private EntityDetails _entityDetails;
        private InstanceNode _dataSourceInstance;
        private MethodSet _methods = null;

        public string AssemblyPath
        {
            get { return _assemblyPath; }
            set 
            { 
                this._assemblyPath = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AssemblyPath"));
            }
        }
        public string OpPath
        {
            get { return _opPath; }
            set 
            { 
                this._opPath = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OpPath"));
            }
        }
        public string Name
        {
            get { return _name; }
            set 
            { 
                this._name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string ModelNS
        {
            get { return _modelNS; }
            set 
            { 
                this._modelNS = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ModelNS"));
            }
        }
        public string TargetNameSpace
        {
            get { return _targetNS; }
            set 
            { 
                this._targetNS = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TargetNameSpace"));
            }
        }
        public bool Output
        {
            get { return _output; }
            set 
            { 
                this._output = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Output"));
            }
        }
        public Guid ID
        {
            get { return _id; }
            set 
            { 
                this._id = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ID"));
            }
        }
        public EntityDetails EntityDetails
        {
            get { return _entityDetails; }
            set 
            { 
                this._entityDetails = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EntityDetails"));
            }
        }
        public InstanceNode DataSourceInstance
        {
            get { return _dataSourceInstance; }
            set
            {
                _dataSourceInstance = value;

                // Needed to fix pointers after de-serialization
                _dataSourceInstance.RePointNodes();
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DataSourceInstance"));
            }
        }
        public MethodSet Methods
        {
            get { return _methods; }
            set 
            { 
                this._methods = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Methods"));
            }
        }

        public Model()
        {
            this._id = Guid.NewGuid();
            this._entityDetails = new EntityDetails();            
            this._dataSourceInstance = new InstanceNode();
            this._methods = new MethodSet();
        }
    }
}
