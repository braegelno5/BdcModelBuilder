// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class MethodSet : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _editMode = false;
        private MethodDetails _readListMethod = null;
        private MethodDetails _readItemMethod = null;
        private MethodDetails _updateMethod = null;
        private MethodDetails _createMethod = null;
        private MethodDetails _deleteMethod = null;
        private ObservableCollection<MethodDetails> _methodDetailsList = null;

        [XmlIgnore]
        public bool EditMode
        {
            get { return _editMode; }
            set
            {
                this._editMode = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EditMode"));
            }
        }

        public MethodDetails ReadListMethod
        {
            get { return _readListMethod; }
            set 
            { 
                _readListMethod = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReadListMethod"));
            }
        }
        public MethodDetails ReadItemMethod
        {
            get { return _readItemMethod; }
            set 
            { 
                _readItemMethod = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReadItemMethod"));
            }
        }
        public MethodDetails UpdateMethod
        {
            get { return _updateMethod; }
            set 
            { 
                _updateMethod = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("UpdateMethod"));
            }
        }
        public MethodDetails CreateMethod
        {
            get { return _createMethod; }
            set 
            { 
                _createMethod = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CreateMethod"));
            }
        }
        public MethodDetails DeleteMethod
        {
            get { return _deleteMethod; }
            set 
            { 
                _deleteMethod = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DeleteMethod"));
            }
        }

        public MethodSet()
        {
            this._readListMethod = new MethodDetails("Read List", true);
            this._readItemMethod = new MethodDetails("Read Item", true);
            this._updateMethod = new MethodDetails("Update", false);
            this._createMethod = new MethodDetails("Create", false);
            this._deleteMethod = new MethodDetails("Delete", false);
        }

        public ObservableCollection<MethodDetails> MethodDetailsList
        {
            get
            {
                this._methodDetailsList = new ObservableCollection<MethodDetails>();
                this._methodDetailsList.Add(this._readListMethod);
                this._methodDetailsList.Add(this._readItemMethod);
                this._methodDetailsList.Add(this._updateMethod);
                this._methodDetailsList.Add(this._createMethod);
                this._methodDetailsList.Add(this._deleteMethod);

                return this._methodDetailsList;
            }
        }
    }
}
