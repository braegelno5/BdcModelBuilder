// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class MethodDetails : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _methodType = "";
        private bool _include = false;
        private string _methodName = "";
        private string _returnTypeName = "";
        private string _returnTypeFullName = null;
        private InstanceNode _returnObject = null;
        private InstanceNode _methodParams = null;
        private bool _isArray = false;
        private string _elementTypeName = "";

        public string MethodType
        {
            get { return _methodType; }
            set 
            { 
                _methodType = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MethodType"));
            }
        }
        public bool Include
        {
            get { return _include; }
            set 
            { 
                _include = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Include"));
            }
        }
        public string MethodName
        {
            get { return _methodName; }
            set 
            { 
                _methodName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MethodName"));
            }
        }
        public string ReturnTypeName
        {
            get { return _returnTypeName; }
            set 
            { 
                _returnTypeName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReturnTypeName"));
            }
        }
        public string ReturnTypeFullName
        {
            get { return _returnTypeFullName; }
            set 
            { 
                _returnTypeFullName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReturnTypeFullName"));
            }
        }
        public InstanceNode ReturnObject
        {
            get { return _returnObject; }
            set 
            { 
                _returnObject = value;
                _returnObject.RePointNodes();
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ReturnObject"));
            }
        }
        public InstanceNode MethodParams
        {
            get { return _methodParams; }
            set 
            {
                _methodParams = value;

                // Needed to fix pointers after de-serialization
                _methodParams.RePointNodes();
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MethodParams"));
            }
        }
        public bool IsArray
        {
            get { return _isArray; }
            set 
            {
                _isArray = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsArray"));
            }
        }
        public string ElementTypeName
        {
            get { return _elementTypeName; }
            set 
            { 
                _elementTypeName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ElementTypeName"));
            }
        }

        // Constructor
        public MethodDetails()
        {

        }

        public MethodDetails(string methodType, bool include)
        {
            this._methodType = methodType;
            this._include = include;
        }        
    }
}
