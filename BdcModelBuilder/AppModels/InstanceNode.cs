// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WebberCross.BdcModelBuilder.AppModels
{
    [Serializable]
    public class InstanceNode : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name = "";
        private ObservableCollection<InstanceNode> _nestedInstances = null;
        private ObservableCollection<InstanceNode> _constructorParameters = null;
        private string _instanceTypeName = null;
        private string _instanceTypeFullName = null;
        private string _instanceTypeNameSpace = null;
        private string _value = "";
        private string _cfgKey = "";
        private bool _isEntity = false;
        private int _paramNumber = 0;
        private ObservableCollection<InstancePath> _instancePaths = null;
        private Guid _instanceID;
        private bool _isArray = false;
        private string _elementTypeName = "";

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
        public ObservableCollection<InstanceNode> NestedInstances
        {
            get { return _nestedInstances; }
            set 
            { 
                _nestedInstances = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("NestedInstances"));
            }
        }
        public ObservableCollection<InstanceNode> ConstructorParameters
        {
            get { return _constructorParameters; }
            set 
            { 
                _constructorParameters = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ConstructorParameters"));
            }
        }
        public string InstanceTypeName
        {
            get { return _instanceTypeName; }
            set 
            { 
                _instanceTypeName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InstanceTypeName"));
            }
        }
        public string InstanceTypeNameSpace
        {
            get { return _instanceTypeNameSpace; }
            set 
            { 
                _instanceTypeNameSpace = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InstanceTypeNameSpace"));
            }
        }
        public string InstanceTypeFullName
        {
            get { return _instanceTypeFullName; }
            set 
            { 
                _instanceTypeFullName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InstanceTypeFullName"));
            }
        }
        public string Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Value"));
            }
        }
        public string CfgKey
        {
            get { return _cfgKey; }
            set 
            { 
                _cfgKey = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CfgKey"));
            }
        }
        public bool IsEntity
        {
            get { return this._isEntity; }
            set 
            { 
                this._isEntity = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsEntity"));
            }
        }
        public int ParamNumber
        {
            get { return _paramNumber; }
            set 
            { 
                _paramNumber = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ParamNumber"));
            }
        }
        public ObservableCollection<InstancePath> InstancePaths
        {
            get { return _instancePaths; }
            set 
            { 
                _instancePaths = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("InstancePaths"));
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

        // Constructors
        public InstanceNode()
        {
            this._nestedInstances = new ObservableCollection<InstanceNode>();
            this._constructorParameters = new ObservableCollection<InstanceNode>();
            this._instanceID = Guid.NewGuid();
        }

        public InstanceNode(string name, Type t) : this()
        {
            this._name = name;

            if (t != null)
            {
                this._instanceTypeFullName = t.FullName;
                this._instanceTypeName = t.Name;
                this._instanceTypeNameSpace = t.Namespace;
                this._isArray = t.IsArray;
                if (this._isArray)
                    this._elementTypeName = t.GetElementType().Name;
            }
        }

        // Re-point instance paths to nodes
        public void RePointNodes()
        {
            // Get a list of all nodes
            Dictionary<Guid, InstanceNode> nodes = new Dictionary<Guid, InstanceNode>();
            GetAllNodes(nodes, this);

            // Fix node pointers
            FixNodePointers(nodes, this);
        }

        // Gets all nested nodes
        private void GetAllNodes(Dictionary<Guid, InstanceNode> nodes, InstanceNode node)
        {
            nodes.Add(node._instanceID, node);

            foreach (InstanceNode n in node._nestedInstances)
            {
                GetAllNodes(nodes, n);
            }
        }

        // Fix all nested paths
        private void FixNodePointers(Dictionary<Guid, InstanceNode> nodes, InstanceNode node)
        {
            if (node._instancePaths == null) return;

            foreach (InstancePath p in node._instancePaths)
            {
                if (nodes.ContainsKey(p.InstanceID))
                    p.Node = nodes[p.InstanceID];
            }

            foreach (InstanceNode n in node._nestedInstances)
            {
                FixNodePointers(nodes, n);
            }
        }
    }
}
