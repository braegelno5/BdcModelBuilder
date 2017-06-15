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
    public class InstancePath : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _path = "";
        private string _value = "";
        private string _cfgKey = "";
        private bool _isEntity = false;
        private InstanceNode _node = null;
        private Guid _instanceID;
        int _paramNumber = 0;

        public string Path
        {
            get { return this._path; }
            set 
            { 
                this._path = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Path"));
            }
        }
        public string Value
        {
            get { return this._value; }
            set 
            { 
                this._value = value;

                // Poke value into node
                if (this._node != null) this._node.Value = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
        public string CfgKey
        {
            get { return this._cfgKey; }
            set
            {
                this._cfgKey = value;

                // Poke value into node
                if (this._node != null) this._node.CfgKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CfgKey"));
            }
        }
        public bool IsEntity
        {
            get { return this._isEntity; }
            set
            {
                this._isEntity = value;

                // Poke value into node
                if (this._node != null) this._node.IsEntity = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsEntity"));
            }
        }
        public InstanceNode Node
        {
            get { return this._node; }
            set 
            { 
                this._node = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Node"));
            }
        }
        public Guid InstanceID
        {
            get { return _instanceID; }
            set 
            { 
                _instanceID = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InstanceID"));
            }
        }
        public int ParamNumber
        {
            get { return _paramNumber; }
            set
            {
                this._paramNumber = value;

                // Poke value into node
                if (this._node != null) this._node.ParamNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamNumber"));
            }
        }
        
        // Constructor
        public InstancePath()
        {

        }

        public InstancePath(string name, InstanceNode node)
        {
            this._path = name;
            this._node = node;
            this._instanceID = node.InstanceID;
        }
    }
}
