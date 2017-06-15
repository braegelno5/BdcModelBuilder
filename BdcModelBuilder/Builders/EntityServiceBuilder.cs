// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using WebberCross.BdcModelBuilder.AppModels;

namespace WebberCross.BdcModelBuilder.Builders
{
    public class EntityServiceBuilder
    {
        private Model _model;

        private string _fileName = "";
        private string _assemblyNS = "";
        private string _className = "";
        private string _opDir = "";
        private string _entityName = "";
        private List<string> _identifierNames = null;
        private List<string> _identifierTypes = null;
        private string _targetNS = "";
        private EntityCollectionType _entCollectionType = EntityCollectionType.IEnumerableCollection;
        private string _dataSourceNS = "";
        private List<EntityProperties> _entProperties = null;
        private List<string> _codeFile = null;
        private InstanceNode _rootInstance = null;
        private List<string> _instances = null;
        private MethodSet _methods = null;

        public string ModelNS
        {
            get { return _assemblyNS; }
            set { _assemblyNS = value; }
        }
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public string OpDir
        {
            get { return _opDir; }
            set { _opDir = value; }
        }
        public string TargetNameSpace
        {
            get { return _targetNS; }
            set { _targetNS = value; }
        }
        public string EntityName
        {
            get { return _entityName; }
            set { _entityName = value; }
        }
        public List<string> IdentifierNames
        {
            get { return _identifierNames; }
            set { _identifierNames = value; }
        }
        public List<string> IdentifierTypes
        {
            get { return _identifierTypes; }
            set { _identifierTypes = value; }
        }
        public EntityCollectionType EntCollectionType
        {
            get { return _entCollectionType; }
            set { _entCollectionType = value; }
        }
        public string DataSourceNS
        {
            get { return _dataSourceNS; }
            set { _dataSourceNS = value; }
        }
        public List<EntityProperties> EntityProperties
        {
            get { return _entProperties; }
            set { _entProperties = value; }
        }
        public List<string> CodeFile
        {
            get { return _codeFile; }
            set { _codeFile = value; }
        }
        public InstanceNode RootInstance
        {
            get { return _rootInstance; }
            set { _rootInstance = value; }
        }
        public MethodSet Methods
        {
            get { return _methods; }
            set { _methods = value; }
        }

        // Constructors
        public EntityServiceBuilder(Model model)
        {
            this._model = model;
        }
        public EntityServiceBuilder(string opDir, Model model)
            : this(model)
        {
            this._opDir = opDir;
        }
    
        // Builds entity
        public void BuildEntityService()
        {
            // Create file name
            _fileName = string.Format("{0}\\{1}EntityService.cs", _opDir, _className);

            this._codeFile = new List<string>();

            // Write header
            this.CreateEntityService();        
        }

        /// <summary>
        /// Writes header section
        /// </summary>
        /// <param name="stw"></param>
        private void CreateEntityService()
        {
            // Write header
            this._codeFile.Add("// Webber-Cross SharePoint Entity Service");
            this._codeFile.Add("// Automatically built by WebberCross.BDCModel");
            this._codeFile.Add("");
            this._codeFile.Add("using System;");
            this._codeFile.Add("using System.Collections.Generic;");
            this._codeFile.Add("using System.Configuration;");
            this._codeFile.Add("using System.Linq;");
            this._codeFile.Add("using System.Text;");
            this._codeFile.Add("using System.ServiceModel;");
            this._codeFile.Add("using WebberCross.BdcModelBuilder.Common;");
            this._codeFile.Add(string.Format("using {0};", this._targetNS));
            this._codeFile.Add(string.Format("using {0};", this._rootInstance.NestedInstances[0].InstanceTypeNameSpace)); 
            
            this._codeFile.Add("");
            this._codeFile.Add("namespace " +  _assemblyNS);
            this._codeFile.Add("{");          
            this._codeFile.Add("\t[Serializable]");
            this._codeFile.Add("\tpublic class " + _className);
            this._codeFile.Add("\t{");

            // Create Read Method
            this.CreateReadMethod();

            // Create ReadItem Method
            this.CreateReadItemMethod();

            // Create Create Method
            if (_methods.CreateMethod.Include)
                this.CreateCreateMethod();

            // Create Delete Method
            if (_methods.DeleteMethod.Include)
                this.CreateDeleteMethod();

            // Create Update Method
            if (_methods.UpdateMethod.Include)
                this.CreateUpdateMethod();

            // Write footer
            this._codeFile.Add("\t}");
            this._codeFile.Add("}");
        }

        // Create Read Method
        private void CreateReadMethod()
        {
            // Collection
            string collectionType = "";

            // Collection options
            switch (_entCollectionType)
            {
                case EntityCollectionType.IEnumerableCollection:
                    collectionType = string.Format("IEnumerable<{0}>", this._entityName);
                    break;
            }

            // Method start
            this._codeFile.Add(string.Format("\t\tpublic static {0} ReadList()", collectionType));
            this._codeFile.Add("\t\t{");

            // Return item
            this._codeFile.Add("\t\t\t// Return Item");
            this._codeFile.Add(string.Format("\t\t\tList<{0}> entityList = new List<{0}>();", this._entityName));

            // Instances
            this._codeFile.Add("");

            // Try start
            this._codeFile.Add("\t\t\ttry");
            this._codeFile.Add("\t\t\t{");

            this._codeFile.Add("\t\t\t\t// Instances");
            this._codeFile.Add(this.GetRootInstance(this._rootInstance));
            if (this._instances == null)
            {
                this._instances = new List<string>();

                foreach (InstanceNode property in this._rootInstance.NestedInstances[0].NestedInstances)
                {
                    this.GetInstanceProperties(this._instances, property, string.Format("_{0}", this._rootInstance.NestedInstances[0].Name));
                }
            }

            foreach (string s in _instances)
            {
                this._codeFile.Add(s);
            }

            // Method parameters
            string paramList = "";

            if (this._methods.ReadListMethod.MethodParams != null &&
                this._methods.ReadListMethod.MethodParams.NestedInstances != null && 
                this._methods.ReadListMethod.MethodParams.NestedInstances.Count != 0)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Method parameters");

                List<string> _methodParams = new List<string>();
                this.GetInstanceProperties(_methodParams, this._methods.ReadListMethod.MethodParams, "");

                foreach (string s in _methodParams)
                {
                    this._codeFile.Add(s);
                }

                foreach (InstanceNode node in this._methods.ReadListMethod.MethodParams.NestedInstances)
                {
                    if (paramList != "") paramList += ", ";
                    paramList += "_" + node.Name;
                }
            }
            
            // Call method
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Call method");
            this._codeFile.Add(string.Format("\t\t\t\t{0} retObj = _{1}.{2}({3});",
                this._methods.ReadListMethod.ReturnObject.NestedInstances[0].InstanceTypeName, 
                this._rootInstance.NestedInstances[0].InstanceTypeName, 
                this._methods.ReadListMethod.MethodName, 
                paramList));

            // Parse ret object
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Parse retObj");

            string retEnt = this._methods.ReadListMethod.ReturnObject.InstancePaths[0].Path;
            retEnt = retEnt.Replace(retEnt.Split('.')[0], "retObj");

            this._codeFile.Add(string.Format("\t\t\t\tforeach({0} retEnt in {1})",
                this._methods.ReadListMethod.ElementTypeName,
                retEnt));
            this._codeFile.Add("\t\t\t\t{");
            this._codeFile.Add(string.Format("\t\t\t\t\t{0} ent = new {0}();", _entityName));
            this._codeFile.Add("\t\t\t\t\tReflectionUtility.CopyByProperty(retEnt, ent);");
            this._codeFile.Add("\t\t\t\t\tentityList.Add(ent);");
            this._codeFile.Add("\t\t\t\t}");

            // Try end
            this._codeFile.Add("\t\t\t}");

            // Catch
            this._codeFile.Add("\t\t\tcatch(Exception ex)");
            this._codeFile.Add("\t\t\t{");
            this._codeFile.Add("\t\t\t\tUtilities utl = new Utilities();");
            this._codeFile.Add("\t\t\t\tutl.HandleException(ex);");

            // Catch end
            this._codeFile.Add("\t\t\t}");            

            // Return
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\treturn entityList;");

            // Method end
            this._codeFile.Add("\t\t}");
        }

        // Create ReadItem Method
        private void CreateReadItemMethod()
        {
            // Method start
            this._codeFile.Add(string.Format("\t\tpublic static {0} ReadItem({1})", this._entityName, this.GetIdentifierParams()));
            this._codeFile.Add("\t\t{");

            // Return item
            this._codeFile.Add("\t\t\t// Return Item");
            this._codeFile.Add(string.Format("\t\t\t{0} entity = new {0}();", this._entityName));
            this._codeFile.Add("");

            // Try start
            this._codeFile.Add("\t\t\ttry");
            this._codeFile.Add("\t\t\t{");

            this._codeFile.Add("\t\t\t\t// Instances");
            this._codeFile.Add(this.GetRootInstance(this._rootInstance));
            if (this._instances == null)
            {
                this._instances = new List<string>();

                foreach (InstanceNode property in this._rootInstance.NestedInstances[0].NestedInstances)
                {
                    this.GetInstanceProperties(this._instances, property, string.Format("_{0}", property.InstanceTypeName));
                }
            }

            foreach (string s in _instances)
            {
                this._codeFile.Add(s);
            }

            // Method parameters
            if (this._methods.ReadItemMethod.MethodParams.NestedInstances.Count != 0)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Method parameters");

                List<string> _methodParams = new List<string>();
                this.GetInstanceProperties(_methodParams, this._methods.ReadItemMethod.MethodParams, "");

                foreach (string s in _methodParams)
                {
                    this._codeFile.Add(s);
                }
            }

            string paramList = "";
            foreach (InstanceNode node in this._methods.ReadItemMethod.MethodParams.NestedInstances)
            {
                if (paramList != "") paramList += ", ";
                paramList += "_" + node.Name;
            }

            // Map parameters
            for (int i = 0; i < this._identifierNames.Count; i++)
            {
                InstancePath _param = null;
                if (this._methods.ReadItemMethod.MethodParams.InstancePaths
                     .Count(ip => ip.ParamNumber == i + 1) > 0)
                {
                    _param = this._methods.ReadItemMethod.MethodParams.InstancePaths
                         .Single(ip => ip.ParamNumber == i + 1);
                }

                // If there is no parameter, the method may return multiple items, so return object must be searched
                if (_param != null)
                {
                    this._codeFile.Add("");
                    this._codeFile.Add("\t\t\t\t// Map Id");

                    if (!_param.Node.IsArray && _param.IsEntity) // Entity
                    {
                        this._codeFile.Add(string.Format("\t\t\t\t_{0} = new {1}();", _param.Node.Name, _param.Node.InstanceTypeName));
                        this._codeFile.Add(string.Format("\t\t\t\t_{0}.{1} = param{2};", _param.Node.Name, _identifierNames[i], i + 1));
                    }
                    else if (_param.Node.IsArray && _param.IsEntity) // Entity array
                    {
                        this._codeFile.Add(string.Format("\t\t\t_{0}[0] = new {1}();", _param.Node.Name, _param.Node.ElementTypeName));
                        this._codeFile.Add(string.Format("\t\t\t_{0}[0].{1} = param{2};", _param.Node.Name, _identifierNames[i], i + 1));
                    }
                    else // Object
                    {
                        this._codeFile.Add(string.Format("\t\t\t\t_{0} = param{1};", _param.Path, i + 1));
                    }
                }
            }

            // Call method
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Call method");
            this._codeFile.Add(string.Format("\t\t\t\t{0} retObj = _{1}.{2}({3});",
                this._methods.ReadItemMethod.ReturnObject.NestedInstances[0].InstanceTypeName,
                this._rootInstance.NestedInstances[0].InstanceTypeName,
                this._methods.ReadItemMethod.MethodName,
                paramList));

            // If return item is a collection, search for entity
            if (this._methods.ReadItemMethod.IsArray)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Return item is a collection, so search for entity");

                string retEnt = this._methods.ReadItemMethod.ReturnObject.InstancePaths[0].Path;
                retEnt = retEnt.Replace(retEnt.Split('.')[0], "retObj");

                this._codeFile.Add(string.Format("\t\t\t\tforeach({0} e in {1})", 
                    this._methods.ReadItemMethod.ElementTypeName,
                    retEnt));
                this._codeFile.Add("\t\t\t\t{");
                this._codeFile.Add(string.Format("\t\t\t\t\tif (e.{0} == param1)", _identifierNames));
                this._codeFile.Add("\t\t\t\t\t{");
                this._codeFile.Add("\t\t\t\t\t\t// Parse entity");
                this._codeFile.Add("\t\t\t\t\t\tReflectionUtility.CopyByProperty(e, entity);");
                this._codeFile.Add("\t\t\t\t\t\tbreak;");
                this._codeFile.Add("\t\t\t\t\t}");
                this._codeFile.Add("\t\t\t\t}");
            }
            else
            {
                // Parse ret object
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Parse retObj");

                string retEnt = this._methods.ReadItemMethod.ReturnObject.InstancePaths[0].Path;
                retEnt = retEnt.Replace(retEnt.Split('.')[0], "retObj");

                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty({0}, entity);",
                    retEnt));
            }

            // Fix Dates
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t//Fix dates");
            this._codeFile.Add("\t\t\t\tReflectionUtility.FixDatesForSharePoint(entity);");

            // Try end
            this._codeFile.Add("\t\t\t}");

            // Catch
            this._codeFile.Add("\t\t\tcatch(Exception ex)");
            this._codeFile.Add("\t\t\t{");
            this._codeFile.Add("\t\t\t\tUtilities utl = new Utilities();");
            this._codeFile.Add("\t\t\t\tutl.HandleException(ex);");

            // Catch end
            this._codeFile.Add("\t\t\t}");        

            // Return
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\treturn entity;");

            // Method end
            this._codeFile.Add("\t\t}");
        }

        // Create Create Method
        private void CreateCreateMethod()
        {
            // Method start
            this._codeFile.Add(string.Format("\t\tpublic static {0} Create({0} param1)", this._entityName));
            this._codeFile.Add("\t\t{");
            this._codeFile.Add("");

            // Return item
            this._codeFile.Add("\t\t\t// Return Item");
            this._codeFile.Add(string.Format("\t\t\t{0} entity = new {0}();", this._entityName));

            // Try start
            this._codeFile.Add("\t\t\ttry");
            this._codeFile.Add("\t\t\t{");

            this._codeFile.Add("\t\t\t\t// Instances");
            this._codeFile.Add(this.GetRootInstance(this._rootInstance));
            if (this._instances == null)
            {
                this._instances = new List<string>();

                foreach (InstanceNode property in this._rootInstance.NestedInstances[0].NestedInstances)
                {
                    this.GetInstanceProperties(this._instances, property, string.Format("_{0}", property.InstanceTypeName));
                }
            }

            foreach (string s in _instances)
            {
                this._codeFile.Add(s);
            }

            // Method parameters
            if (this._methods.CreateMethod.MethodParams.NestedInstances.Count != 0)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Method parameters");

                List<string> _methodParams = new List<string>();
                this.GetInstanceProperties(_methodParams, this._methods.CreateMethod.MethodParams, "");

                foreach (string s in _methodParams)
                {
                    this._codeFile.Add(s);
                }
            }

            string paramList = "";
            foreach (InstanceNode node in this._methods.CreateMethod.MethodParams.NestedInstances)
            {
                if (paramList != "") paramList += ", ";
                paramList += "_" + node.Name;
            }
                        
            // Find param 1 and map entity
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Map Entity");
            InstancePath param1 = null;
            foreach (InstancePath p in this._methods.CreateMethod.MethodParams.InstancePaths)
            {
                if (p.ParamNumber == 1)
                {
                    param1 = p;
                    break;
                }
            }
            
            if(!param1.Node.IsArray)
            {
                this._codeFile.Add(string.Format("\t\t\t\t_{0} = new {1}();", param1.Path, param1.Node.InstanceTypeName));
                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty(param1, _{0});", param1.Path));
            }
            else
            {
                this._codeFile.Add(string.Format("\t\t\t\t_{0}[0] = new {1}();", param1.Path, param1.Node.ElementTypeName));
                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty(param1, _{0}[0]);", param1.Path));
            }

            // Call method
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Call method");
            this._codeFile.Add(string.Format("\t\t\t\t{0} retObj = _{1}.{2}({3});",
                this._methods.CreateMethod.ReturnObject.NestedInstances[0].InstanceTypeName,
                this._rootInstance.NestedInstances[0].InstanceTypeName,
                this._methods.CreateMethod.MethodName,
                paramList));

            // If return item is a collection, search for entity
            if (this._methods.CreateMethod.IsArray)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Return item is a collection, so search for entity");

                string retEnt = this._methods.ReadListMethod.ReturnObject.InstancePaths[0].Path;
                retEnt = retEnt.Replace(retEnt.Split('.')[0], "retObj");

                this._codeFile.Add(string.Format("\t\t\t\tforeach({0} e in {1})",
                    this._methods.CreateMethod.ElementTypeName,
                    retEnt));
                this._codeFile.Add("\t\t\t\t{");
                this._codeFile.Add(string.Format("\t\t\t\t\tif (e.{0} == param1.{0})", _identifierNames));
                this._codeFile.Add("\t\t\t\t\t{");
                this._codeFile.Add("\t\t\t\t\t\t// Parse entity");
                this._codeFile.Add("\t\t\t\t\t\tReflectionUtility.CopyByProperty(e, entity);");
                this._codeFile.Add("\t\t\t\t\t\tbreak;");
                this._codeFile.Add("\t\t\t\t\t}");
                this._codeFile.Add("\t\t\t\t}");
            }
            else
            {
                // Parse ret object
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Parse retObj");

                string retEnt = this._methods.CreateMethod.ReturnObject.InstancePaths[0].Path;
                retEnt = retEnt.Replace(retEnt.Split('.')[0], "retObj");

                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty({0}, entity);",
                    retEnt));
            }

            // Try end
            this._codeFile.Add("\t\t\t}");

            // Catch
            this._codeFile.Add("\t\t\tcatch(Exception ex)");
            this._codeFile.Add("\t\t\t{");
            this._codeFile.Add("\t\t\t\tUtilities utl = new Utilities();");
            this._codeFile.Add("\t\t\t\tutl.HandleException(ex);");

            // Catch end
            this._codeFile.Add("\t\t\t}");        

            // Return
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\treturn entity;");

            // Method end
            this._codeFile.Add("\t\t}");
        }

        // Create Delete Method
        private void CreateDeleteMethod()
        {
            // Method start
            this._codeFile.Add(string.Format("\t\tpublic static void Delete({0})", this.GetIdentifierParams()));
            this._codeFile.Add("\t\t{");

            // Try start
            this._codeFile.Add("\t\t\ttry");
            this._codeFile.Add("\t\t\t{");

            this._codeFile.Add("\t\t\t\t// Instances");
            this._codeFile.Add(this.GetRootInstance(this._rootInstance));
            if (this._instances == null)
            {
                this._instances = new List<string>();

                foreach (InstanceNode property in this._rootInstance.NestedInstances[0].NestedInstances)
                {
                    this.GetInstanceProperties(this._instances, property, string.Format("_{0}", property.InstanceTypeName));
                }
            }

            foreach (string s in _instances)
            {
                this._codeFile.Add(s);
            }

            // Method parameters
            if (this._methods.DeleteMethod.MethodParams.NestedInstances.Count != 0)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Method parameters");

                List<string> _methodParams = new List<string>();
                this.GetInstanceProperties(_methodParams, this._methods.DeleteMethod.MethodParams, "");

                foreach (string s in _methodParams)
                {
                    this._codeFile.Add(s);
                }
            }

            string paramList = "";
            foreach (InstanceNode node in this._methods.DeleteMethod.MethodParams.NestedInstances)
            {
                if (paramList != "") paramList += ", ";
                paramList += "_" + node.Name;
            }

            // Map parameters
            for (int i = 0; i < this._identifierNames.Count; i++)
            {
                InstancePath _param = null;
                if (this._methods.ReadItemMethod.MethodParams.InstancePaths
                     .Count(ip => ip.ParamNumber == i + 1) > 0)
                {
                    _param = this._methods.ReadItemMethod.MethodParams.InstancePaths
                         .Single(ip => ip.ParamNumber == i + 1);
                }

                // If there is no parameter, the method may return multiple items, so return object must be searched
                if (_param != null)
                {
                    this._codeFile.Add("");
                    this._codeFile.Add("\t\t\t\t// Map Id");

                    if (!_param.Node.IsArray && _param.IsEntity) // Entity
                    {
                        this._codeFile.Add(string.Format("\t\t\t\t_{0} = new {1}();", _param.Node.Name, _param.Node.InstanceTypeName));
                        this._codeFile.Add(string.Format("\t\t\t\t_{0}.{1} = param{2};", _param.Node.Name, _identifierNames[i], i + 1));
                    }
                    else if (_param.Node.IsArray && _param.IsEntity) // Entity array
                    {
                        this._codeFile.Add(string.Format("\t\t\t_{0}[0] = new {1}();", _param.Node.Name, _param.Node.ElementTypeName));
                        this._codeFile.Add(string.Format("\t\t\t_{0}[0].{1} = param{2};", _param.Node.Name, _identifierNames[i], i + 1));
                    }
                    else // Object
                    {
                        this._codeFile.Add(string.Format("\t\t\t\t_{0} = param{1};", _param.Path, i + 1));
                    }
                }
            }

            // Call method
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Call method");
            this._codeFile.Add(string.Format("\t\t\t\t_{0}.{1}({2});",
                this._rootInstance.NestedInstances[0].InstanceTypeName,
                this._methods.DeleteMethod.MethodName,
                paramList));

            // Try end
            this._codeFile.Add("\t\t\t}");

            // Catch
            this._codeFile.Add("\t\t\tcatch(Exception ex)");
            this._codeFile.Add("\t\t\t{");
            this._codeFile.Add("\t\t\t\tUtilities utl = new Utilities();");
            this._codeFile.Add("\t\t\t\tutl.HandleException(ex);");

            // Catch end
            this._codeFile.Add("\t\t\t}");        

            // Method end
            this._codeFile.Add("\t\t}");
        }

        // Create Update Method
        private void CreateUpdateMethod()
        {
            // Method start
            this._codeFile.Add(string.Format("\t\tpublic static void Update({0} param1)", this._entityName));
            this._codeFile.Add("\t\t{");

            // Try start
            this._codeFile.Add("\t\t\ttry");
            this._codeFile.Add("\t\t\t{");

            // Instances
            this._codeFile.Add("\t\t\t\t// Instances");
            this._codeFile.Add(this.GetRootInstance(this._rootInstance));
            if (this._instances == null)
            {
                this._instances = new List<string>();

                foreach (InstanceNode property in this._rootInstance.NestedInstances[0].NestedInstances)
                {
                    this.GetInstanceProperties(this._instances, property, string.Format("_{0}", property.InstanceTypeName));
                }
            }

            foreach (string s in _instances)
            {
                this._codeFile.Add(s);
            }

            // Method parameters
            if (this._methods.UpdateMethod.MethodParams.NestedInstances.Count != 0)
            {
                this._codeFile.Add("");
                this._codeFile.Add("\t\t\t\t// Method parameters");

                List<string> _methodParams = new List<string>();
                this.GetInstanceProperties(_methodParams, this._methods.UpdateMethod.MethodParams, "");

                foreach (string s in _methodParams)
                {
                    this._codeFile.Add(s);
                }
            }

            string paramList = "";
            foreach (InstanceNode node in this._methods.UpdateMethod.MethodParams.NestedInstances)
            {
                if (paramList != "") paramList += ", ";
                paramList += "_" + node.Name;
            }

            // Find param 1 and map entity
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Map Entity");
            InstancePath param1 = null;
            foreach (InstancePath p in this._methods.UpdateMethod.MethodParams.InstancePaths)
            {
                if (p.ParamNumber == 1)
                {
                    param1 = p;
                    break;
                }
            }

            if (!param1.Node.IsArray)
            {
                this._codeFile.Add(string.Format("\t\t\t\t_{0} = new {1}();", param1.Path, param1.Node.InstanceTypeName));
                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty(param1, _{0});", param1.Path));
            }
            else
            {
                this._codeFile.Add(string.Format("\t\t\t\t_{0}[0] = new {1}();", param1.Path, param1.Node.ElementTypeName));
                this._codeFile.Add(string.Format("\t\t\t\tReflectionUtility.CopyByProperty(param1, _{0}[0]);", param1.Path));
            }

            // Call method
            this._codeFile.Add("");
            this._codeFile.Add("\t\t\t\t// Call method");
            this._codeFile.Add(string.Format("\t\t\t\t_{0}.{1}({2});",
                this._rootInstance.NestedInstances[0].InstanceTypeName,
                this._methods.UpdateMethod.MethodName,
                paramList));

            // Try end
            this._codeFile.Add("\t\t\t}");

            // Catch
            this._codeFile.Add("\t\t\tcatch(Exception ex)");
            this._codeFile.Add("\t\t\t{");
            this._codeFile.Add("\t\t\t\tUtilities utl = new Utilities();");
            this._codeFile.Add("\t\t\t\tutl.HandleException(ex);");

            // Catch end
            this._codeFile.Add("\t\t\t}");        

            // Method end
            this._codeFile.Add("\t\t}");
        }

        private string GetRootInstance(InstanceNode node)
        {
            string instance = string.Format("\t\t\t\t{0} _{0} = new {0}(", node.NestedInstances[0].InstanceTypeName);
            for (int i = 0; i < node.ConstructorParameters.Count; i++ )
            {
                if (i > 0) instance += ",";

                string value = node.ConstructorParameters[i].Value;
                if (node.ConstructorParameters[i].CfgKey != "")
                {
                    value = value.Replace("@param", string.Format("ConfigurationManager.AppSettings[\"{0}\"]", node.ConstructorParameters[i].CfgKey));
                }

                instance += value;
            }

            instance += ");";

            return instance;
        }

        private void GetInstanceProperties(List<string> list, InstanceNode node, string previous)
        {
            // Node has no value so may require instantiating
            if (node.Value == "" && node.CfgKey == "")
            {
                
                if (node.Name != "Root") // && (node.ParamNumber == 0 || node.IsEntity))
                {
                    string instance = "";

                    // Complex types need instantiating and assigning
                    if (!node.InstanceTypeFullName.StartsWith("System.") && (node.ParamNumber == 0 || node.IsEntity))
                    {
                        // Instantiate object
                        if (!node.IsArray)
                            instance = string.Format("\t\t\t\t{0} _{1} = new {0}();", node.InstanceTypeName, node.Name);
                        else
                            instance = string.Format("\t\t\t\t{0} _{1} = new {2}[1];", node.InstanceTypeName, node.Name, node.ElementTypeName);

                        list.Add(instance);

                        // Map value
                        if (previous != "")
                            list.Add(string.Format("\t\t\t\t{0}.{1} = _{1};", previous, node.Name));

                        previous = "_" + node.Name;
                    }
                    else if (node.InstanceTypeFullName.StartsWith("System.") && previous == "") // Simple types do not need instantiating
                    {
                        // 
                        list.Add(string.Format("\t\t\t\t{0} _{1};", node.InstanceTypeName, node.Name));
                    }
                }

                // Recursive call
                foreach (InstanceNode n in node.NestedInstances)
                {
                    this.GetInstanceProperties(list, n, previous);
                }
            }
                // Node has a value
            else if (node.Name != "Root" && previous != "")
            {
                string value = node.Value;
                if (node.CfgKey != "")
                {
                    value = value.Replace("@param", string.Format("ConfigurationManager.AppSettings[\"{0}\"]", node.CfgKey));
                }

                list.Add(string.Format("\t\t\t\t{0}.{1} = {2};", previous, node.Name, value));
            }            
        }

        public void WriteEntity()
        {
            // Create directory
            if (!Directory.Exists(_opDir))
                Directory.CreateDirectory(_opDir);

            //if(File.Exists(_fileName))
            //{
            //    string warning = "Overwrite existing file:\n" + _fileName;
            //    if(MessageBox.Show(warning, "Webber-Cross.RuleGen", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            // Write code file
            StreamWriter stw = new StreamWriter(_fileName, false);
            foreach (string s in _codeFile)
            {
                stw.WriteLine(s);
            }
            stw.Close();

            // Clear up resources
            stw.Dispose();
            stw = null;
        }

        private string GetIdentifierParams()
        {
            string _params = "";
            for(int i = 0; i < this._identifierTypes.Count; i++)
            {
                if (_params != "")
                {
                    _params += ", ";
                }
                
                _params += string.Format("{0} param{1}", this._identifierTypes[i], i + 1); 
            }

            return _params;
        }
    }
}
