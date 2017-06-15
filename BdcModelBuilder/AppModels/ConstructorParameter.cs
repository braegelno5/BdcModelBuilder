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
    public class ConstructorParameter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name = "";
        private string _value = "";
        private string _cfgKey = "";
        int _paramNumber = 0;
        private string _paramTypeName = null;
        private string _paramTypeFullName = null;
        private string _paramTypeNameSpace = null;

        public string Name
        {
            get { return this._name; }
            set 
            { 
                this._name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public string Value
        {
            get { return this._value; }
            set 
            { 
                this._value = value;

                // Poke value into node
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
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CfgKey"));
            }
        }
        public int ParamNumber
        {
            get { return _paramNumber; }
            set
            {
                this._paramNumber = value;

                // Poke value into node
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamNumber"));
            }
        }
        public string ParamTypeName
        {
            get { return _paramTypeName; }
            set
            {
                _paramTypeName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamTypeName"));
            }
        }
        public string ParamTypeNameSpace
        {
            get { return _paramTypeNameSpace; }
            set
            {
                _paramTypeNameSpace = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamTypeNameSpace"));
            }
        }
        public string ParamTypeFullName
        {
            get { return _paramTypeFullName; }
            set
            {
                _paramTypeFullName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamTypeFullName"));
            }
        }
        
        // Constructor
        public ConstructorParameter()
        {

        }

        public ConstructorParameter(string name, Type t)
        {
            this._name = name;

            if (t != null)
            {
                this._paramTypeFullName = t.FullName;
                this._paramTypeName = t.Name;
                this._paramTypeNameSpace = t.Namespace;
            }
        }
    }
}
