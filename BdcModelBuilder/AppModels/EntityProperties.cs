// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;


namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class EntityProperties : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name = "";
        private string _propTypeName = "";
        private string _propTypeFullName = null;
        private bool _read = false;
        private bool _update = false;
        private bool _delete = false;
        private bool _create = false;
        private bool _readOnly = false;
        private bool _isIdentifier = false;

        public string Name
        {
            get { return this._name; }
            set 
            { 
                this._name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string PropTypeName
        {
            get { return this._propTypeName; }
            set 
            { 
                this._propTypeName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PropTypeName"));
            }
        }
        public string PropTypeFullName
        {
            get 
            {
                if (this._propTypeFullName == "System.Byte[]")
                    return "System.String";

                return this._propTypeFullName; 
            
            }
            set 
            { 
                this._propTypeFullName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PropTypeFullName"));
            }
        }
        public bool Read
        {
            get { return this._read; }
            set 
            { 
                this._read = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Read"));
            }
        }
        public bool Update
        {
            get { return this._update; }
            set 
            { 
                this._update = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Update"));
            }
        }
        public bool Delete
        {
            get { return this._delete; }
            set 
            { 
                this._delete = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Delete"));
            }
        }
        public bool Create
        {
            get { return this._create; }
            set 
            { 
                this._create = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Create"));
            }
        }
        public bool ReadOnly
        {
            get { return this._readOnly; }
            set 
            { 
                this._readOnly = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReadOnly"));
            }
        }
        public bool IsIdentifier
        {
            get { return this._isIdentifier; }
            set
            {
                this._isIdentifier = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsIdentifier"));
            }
        }

        public EntityProperties()
        {

        }
        public EntityProperties(PropertyInfo pi)
        {
            Type underType = ReflectionUtility.GetUnderlyingType(pi.PropertyType);

            this._propTypeFullName = underType.FullName;
            this._name = pi.Name;
            this._propTypeName = underType.Name; 
        }

        /// <summary>
        /// Gets EntityProperties List for PropertyInfo array
        /// </summary>
        /// <param name="pi"></param>
        /// <returns></returns>
        public static List<EntityProperties> GetEntityPropertiesList(PropertyInfo[] pi)
        {
            List<EntityProperties> ents = new List<EntityProperties>();
            foreach(PropertyInfo p in pi)
                ents.Add(new EntityProperties(p));

            return ents;
        }
    }
}
