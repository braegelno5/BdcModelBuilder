// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Serialization;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class EntityDetails : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _editMode = false;
        private string _entitySource = "";
        private string _entityName = "";
        private string _entityFullName = "";
        private string _displayName = "";
        private ObservableCollection<EntityProperties> _entityProps;

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

        public string EntitySource
        {
            get { return _entitySource; }
            set 
            { 
                this._entitySource = value; 
                if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EntitySource"));
            }
        }
        public string EntityName
        {
            get { return _entityName; }
            set 
            { 
                this._entityName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EntityName"));
            }
        }
        public string EntityFullName
        {
            get { return _entityFullName; }
            set 
            { 
                this._entityFullName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EntityFullName"));
            }
        }
        public string DisplayName
        {
            get { return _displayName; }
            set 
            { 
                this._displayName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DisplayName"));
            }
        }
        public ObservableCollection<EntityProperties> EntityProps
        {
            get { return _entityProps; }
            set 
            { 
                this._entityProps = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EntityProps"));
            }
        }

        public EntityDetails()
        {
            this._entityProps = new ObservableCollection<EntityProperties>();
        }

        public List<string> GetIdentifierNames()
        {
            return this._entityProps.Where(i => i.IsIdentifier).Select(i => i.Name).ToList();
        }

        public List<string> GetIdentifierTypes()
        {
            return this._entityProps.Where(i => i.IsIdentifier).Select(i => i.PropTypeFullName).ToList();
        }
    }
}
