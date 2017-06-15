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
using System.Xml;

using WebberCross.BdcModelBuilder.AppModels;

namespace WebberCross.BdcModelBuilder.Builders
{
    public enum EntityCollectionType
    {
        IEnumerableCollection
    }

    public class BDCMBuilder
    {
        private string _fileName = "";
        private string _fileName2 = "";
        private string _assemblyNS = "";
        private string _assemblyName = "";
        private string _modelName = "";
        private string _entityName = "";
        private string _entityType = "";
        private List<string> _identifierNames = null;
        private List<string> _identifierTypes = null;
        private string _opDir = "";
        private EntityCollectionType _entCollectionType = EntityCollectionType.IEnumerableCollection;
        private bool _includeUpdate = false;
        private bool _includeCreate = false;
        private bool _includeDelete = false;
        private List<EntityProperties> _entProperties = null;
        private List<string> _codeFile = null;
        private XmlDocument _xmlDoc = null;
        private string _bdcSchema = "http://schemas.microsoft.com/windows/2007/BusinessDataCatalog";
        private MethodSet _methods = null;

        public string ModelNS
        {
            get { return _assemblyNS; }
            set { _assemblyNS = value; }
        }
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }
        public string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }
        public string EntityName
        {
            get { return _entityName; }
            set { _entityName = value; }
        }
        public string EntityType
        {
            get { return _entityType; }
            set { _entityType = value; }
        }
        public List<string> IdentifierName
        {
            get { return _identifierNames; }
            set { _identifierNames = value; }
        }
        public List<string> IdentifierType
        {
            get { return _identifierTypes; }
            set { _identifierTypes = value; }
        }
        public EntityCollectionType EntCollectionType
        {
            get { return _entCollectionType; }
            set { _entCollectionType = value; }
        }
        public string OpDir
        {
            get { return _opDir; }
            set { _opDir = value; }
        }
        public bool IncludeUpdate
        {
            get { return _includeUpdate; }
            set { _includeUpdate = value; }
        }
        public bool IncludeCreate
        {
            get { return _includeCreate; }
            set { _includeCreate = value; }
        }
        public bool IncludeDelete
        {
            get { return _includeDelete; }
            set { _includeDelete = value; }
        }
        public List<EntityProperties> EntityProperties
        {
            get { return _entProperties; }
            set { _entProperties = value; }
        }
        public List<string> CodeFile
        {
            get { return this.GetCodeFile(); }
        }
        public MethodSet Methods
        {
            get { return _methods; }
            set { _methods = value; }
        }

        // Constructors
        public BDCMBuilder()
        {
            
        }
        public BDCMBuilder(string opDir)
            : this()
        {
            this._opDir = opDir;
        }
    
        // Builds entity
        public void BuildBDCM()
        {
            // Create file name
            _fileName = string.Format("{0}\\{1}.bdcm", _opDir, _modelName);
            _fileName2 = string.Format("{0}\\{1}\\{1}.bdcm", _opDir, _modelName);

            this._codeFile = new List<string>();

            // Write header
            this.CreateBDCM();        
        }

        /// <summary>
        /// Writes header section
        /// </summary>
        /// <param name="stw"></param>
        private void CreateBDCM()
        {
            // Create document
            _xmlDoc = new XmlDocument();          
            
            // Declaration
            XmlDeclaration xmlDeclaration = _xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null); 

            // CurrentModel Element
            XmlElement model = _xmlDoc.CreateElement("Model", _bdcSchema);
            model.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            model.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            model.SetAttribute("Name", _modelName);
            _xmlDoc.InsertBefore(xmlDeclaration, _xmlDoc.DocumentElement);
            _xmlDoc.AppendChild(model);

            // LobSystems Element
            XmlElement lobSystems = _xmlDoc.CreateElement("LobSystems", _bdcSchema);
            model.AppendChild(lobSystems);

            // LobSystem Element
            XmlElement lobSystem = _xmlDoc.CreateElement("LobSystem", _bdcSchema);
            lobSystem.SetAttribute("Name", _modelName);
            lobSystem.SetAttribute("Type", "DotNetAssembly");
            lobSystems.AppendChild(lobSystem);

            // LobSystemInstances Element
            XmlElement lobSystemInstances = _xmlDoc.CreateElement("LobSystemInstances", _bdcSchema);
            lobSystem.AppendChild(lobSystemInstances);

            // LobSystemInstance Element
            XmlElement lobSystemInstance = _xmlDoc.CreateElement("LobSystemInstance", _bdcSchema);
            lobSystemInstance.SetAttribute("Name", _modelName);
            lobSystemInstances.AppendChild(lobSystemInstance);

            // Entities Element
            XmlElement entities = _xmlDoc.CreateElement("Entities", _bdcSchema);
            lobSystem.AppendChild(entities);

            // Entity Element
            XmlElement entity = _xmlDoc.CreateElement("Entity", _bdcSchema);
            entity.SetAttribute("Name", _entityName);
            entity.SetAttribute("Namespace", _assemblyNS);
            entity.SetAttribute("DefaultDisplayName", _entityName);
            entity.SetAttribute("EstimatedInstanceCount", "1000");
            entity.SetAttribute("Version", "1.0.0.0");
            entities.AppendChild(entity);

            // EntityProperties Element
            XmlElement properties = _xmlDoc.CreateElement("Properties", _bdcSchema);
            entity.AppendChild(properties);

            // Property Element
            XmlElement property = _xmlDoc.CreateElement("Property", _bdcSchema);
            property.SetAttribute("Name", "Class");
            property.SetAttribute("Type", "System.String");
            property.InnerText = string.Format("{0}.{1}Service, {2}", _assemblyNS, _entityName, _modelName);
            properties.AppendChild(property);

            // Identifiers Element
            XmlElement identifiers = _xmlDoc.CreateElement("Identifiers", _bdcSchema);
            entity.AppendChild(identifiers);

            // Identifier Elements
            for (int i = 0; i < this._identifierNames.Count; i++)
            {
                XmlElement identifier = _xmlDoc.CreateElement("Identifier", _bdcSchema);
                identifier.SetAttribute("Name", _identifierNames[i]);
                identifier.SetAttribute("TypeName", _identifierTypes[i]);
                identifiers.AppendChild(identifier);
            }

            // Methods Element
            XmlElement methods = _xmlDoc.CreateElement("Methods", _bdcSchema);
            entity.AppendChild(methods);

            // Create Read Method
            this.CreateReadMethod(methods);

            // Create ReadItem Method
            this.CreateReadItemMethod(methods);

            // Create Create Method
            if (_methods.CreateMethod.Include)
                this.CreateCreateMethod(methods);

            // Create Delete Method
            if (_methods.DeleteMethod.Include)
                this.CreateDeleteMethod(methods);

            // Create Update Method
            if (_methods.UpdateMethod.Include)
                this.CreateUpdateMethod(methods);
        }

        private void CreateReadMethod(XmlElement methods)
        {
            // Comment
            XmlComment comment = _xmlDoc.CreateComment("ReadList Method");
            methods.AppendChild(comment);

            // Identifier Element
            XmlElement method = _xmlDoc.CreateElement("Method", _bdcSchema);
            method.SetAttribute("Name", "ReadList");
            methods.AppendChild(method);

            // Parameters Element
            XmlElement parameters = _xmlDoc.CreateElement("Parameters", _bdcSchema);
            method.AppendChild(parameters);

            // Return Parameter Element
            XmlElement retParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
            retParameter.SetAttribute("Direction", "Return");
            retParameter.SetAttribute("Name", "returnParameter");
            parameters.AppendChild(retParameter);

            // Collection
            // TypeDescriptor Element
            string collectionType = "";

            // Collection options
            switch (_entCollectionType)
            {
                case EntityCollectionType.IEnumerableCollection:
                    collectionType = string.Format("System.Collections.Generic.IEnumerable`1[[{0}, {1}]]", this._entityType, _modelName);
                    break;
            }

            XmlElement typeDescriptor = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            typeDescriptor.SetAttribute("TypeName", collectionType);
            typeDescriptor.SetAttribute("IsCollection", "true");
            typeDescriptor.SetAttribute("Name", _entityName + "List");
            retParameter.AppendChild(typeDescriptor);

            // TypeDescriptors Element
            XmlElement typeDescriptors = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            typeDescriptor.AppendChild(typeDescriptors);

            // Entity
            // TypeDescriptor Element
            XmlElement typeDescriptorEnt = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            typeDescriptorEnt.SetAttribute("TypeName", string.Format("{0}, {1}", this._entityType, _modelName));
            typeDescriptorEnt.SetAttribute("Name", _entityName);
            typeDescriptors.AppendChild(typeDescriptorEnt);

            // TypeDescriptors Element
            XmlElement typeDescriptorsEnt = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            typeDescriptorEnt.AppendChild(typeDescriptorsEnt);

            // Entity EntityProperties
            foreach (EntityProperties ep in this._entProperties)
            {
                if (!ep.Read) continue;

                // TypeDescriptor Element
                XmlElement typeDescriptorProp = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                typeDescriptorProp.SetAttribute("TypeName", ep.PropTypeFullName);
                typeDescriptorProp.SetAttribute("Name", ep.Name);

                if (ep.ReadOnly) // || this._identifierNames.Contains(ep.Name))
                {
                    typeDescriptorProp.SetAttribute("ReadOnly", "true");
                }
                if (this._identifierNames.Contains(ep.Name))
                {
                    typeDescriptorProp.SetAttribute("IdentifierName", ep.Name);
                }
                if (ep.PropTypeName == "DateTime")
                {
                    // Interpretation Element
                    XmlElement interpretation = _xmlDoc.CreateElement("Interpretation", _bdcSchema);
                    typeDescriptorProp.AppendChild(interpretation);

                    // NormalizeDateTime Element
                    XmlElement normalizeDateTime = _xmlDoc.CreateElement("NormalizeDateTime", _bdcSchema);
                    normalizeDateTime.SetAttribute("LobDateTimeMode", "UTC");
                    interpretation.AppendChild(normalizeDateTime);
                    
                }

                typeDescriptorsEnt.AppendChild(typeDescriptorProp);
            }

            // MethodInstances Element
            XmlElement methodInstances = _xmlDoc.CreateElement("MethodInstances", _bdcSchema);
            method.AppendChild(methodInstances);

            // MethodInstance Element
            XmlElement methodInstance = _xmlDoc.CreateElement("MethodInstance", _bdcSchema);
            methodInstance.SetAttribute("Type", "Finder");
            methodInstance.SetAttribute("ReturnParameterName", "returnParameter");
            methodInstance.SetAttribute("Default", "true");
            methodInstance.SetAttribute("Name", "ReadList");
            methodInstance.SetAttribute("DefaultDisplayName", _entityName + " List");
            methodInstances.AppendChild(methodInstance);
        }

        private void CreateReadItemMethod(XmlElement methods)
        {
            // Comment
            XmlComment comment = _xmlDoc.CreateComment("ReadItem Method");
            methods.AppendChild(comment);

            // Identifier Element
            XmlElement method = _xmlDoc.CreateElement("Method", _bdcSchema);
            method.SetAttribute("Name", "ReadItem");
            methods.AppendChild(method);

            // Parameters Element
            XmlElement parameters = _xmlDoc.CreateElement("Parameters", _bdcSchema);
            method.AppendChild(parameters);

            // Input Parameter Elements
            for (int i = 0; i < this._identifierNames.Count; i++)
            {
                XmlElement inParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
                inParameter.SetAttribute("Direction", "In");
                inParameter.SetAttribute("Name", string.Format("id{0}", i + 1));
                parameters.AppendChild(inParameter);

                XmlElement typeDescriptorIn = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                typeDescriptorIn.SetAttribute("TypeName", _identifierTypes[i]);
                typeDescriptorIn.SetAttribute("IdentifierName", _identifierNames[i]);
                typeDescriptorIn.SetAttribute("Name", string.Format("param{0}", i + 1));
                inParameter.AppendChild(typeDescriptorIn);
            }

            // Return Parameter Element
            XmlElement retParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
            retParameter.SetAttribute("Direction", "Return");
            retParameter.SetAttribute("Name", "returnParameter");
            parameters.AppendChild(retParameter);

            // Entity
            // TypeDescriptor Element
            XmlElement typeDescriptorEnt = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            typeDescriptorEnt.SetAttribute("TypeName", string.Format("{0}, {1}", this._entityType, _modelName));
            typeDescriptorEnt.SetAttribute("Name", _entityName);
            retParameter.AppendChild(typeDescriptorEnt);

            // TypeDescriptors Element
            XmlElement typeDescriptorsEnt = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            typeDescriptorEnt.AppendChild(typeDescriptorsEnt);

            // Entity EntityProperties
            foreach (EntityProperties ep in this._entProperties)
            {
                if (!ep.Read) continue;

                // TypeDescriptor Element
                XmlElement typeDescriptorProp = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                typeDescriptorProp.SetAttribute("TypeName", ep.PropTypeFullName);
                typeDescriptorProp.SetAttribute("Name", ep.Name);

                if (ep.ReadOnly) // || this._identifierNames.Contains(ep.Name))
                {
                    typeDescriptorProp.SetAttribute("ReadOnly", "true");
                }
                if (this._identifierNames.Contains(ep.Name))
                {
                    typeDescriptorProp.SetAttribute("IdentifierName", ep.Name);
                }
                if (ep.PropTypeName == "DateTime")
                {
                    // Interpretation Element
                    XmlElement interpretation = _xmlDoc.CreateElement("Interpretation", _bdcSchema);
                    typeDescriptorProp.AppendChild(interpretation);

                    // NormalizeDateTime Element
                    XmlElement normalizeDateTime = _xmlDoc.CreateElement("NormalizeDateTime", _bdcSchema);
                    normalizeDateTime.SetAttribute("LobDateTimeMode", "UTC");
                    interpretation.AppendChild(normalizeDateTime);

                }

                typeDescriptorsEnt.AppendChild(typeDescriptorProp);
            }

            // MethodInstances Element
            XmlElement methodInstances = _xmlDoc.CreateElement("MethodInstances", _bdcSchema);
            method.AppendChild(methodInstances);

            // MethodInstance Element
            XmlElement methodInstance = _xmlDoc.CreateElement("MethodInstance", _bdcSchema);
            methodInstance.SetAttribute("Type", "SpecificFinder");
            methodInstance.SetAttribute("ReturnParameterName", "returnParameter");
            methodInstance.SetAttribute("Default", "true");
            methodInstance.SetAttribute("Name", "ReadItem");
            methodInstance.SetAttribute("DefaultDisplayName", "Read " + _entityName);
            methodInstances.AppendChild(methodInstance);
        }

        private void CreateCreateMethod(XmlElement methods)
        {
            // Comment
            XmlComment comment = _xmlDoc.CreateComment("Create Method");
            methods.AppendChild(comment);

            // Identifier Element
            XmlElement method = _xmlDoc.CreateElement("Method", _bdcSchema);
            method.SetAttribute("Name", "Create");
            methods.AppendChild(method);

            // Parameters Element
            XmlElement parameters = _xmlDoc.CreateElement("Parameters", _bdcSchema);
            method.AppendChild(parameters);

            // Return Parameter Element
            XmlElement retParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
            retParameter.SetAttribute("Direction", "Return");
            retParameter.SetAttribute("Name", "returnParameter");
            parameters.AppendChild(retParameter);

            // Entity
            // TypeDescriptor Element
            XmlElement retTypeDescriptorEnt = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            retTypeDescriptorEnt.SetAttribute("TypeName", string.Format("{0}, {1}", this._entityType, _modelName));
            retTypeDescriptorEnt.SetAttribute("Name", _entityName);
            retParameter.AppendChild(retTypeDescriptorEnt);

            // TypeDescriptors Element
            XmlElement retTypeDescriptorsEnt = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            retTypeDescriptorEnt.AppendChild(retTypeDescriptorsEnt);

            // Entity EntityProperties
            foreach (EntityProperties ep in this._entProperties)
            {
                if (!ep.Read) continue;

                // TypeDescriptor Element
                XmlElement retTypeDescriptorProp = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                retTypeDescriptorProp.SetAttribute("TypeName", ep.PropTypeFullName);
                retTypeDescriptorProp.SetAttribute("Name", ep.Name);

                if (ep.ReadOnly) // || this._identifierNames.Contains(ep.Name))
                {
                    retTypeDescriptorProp.SetAttribute("ReadOnly", "true");
                }
                if (this._identifierNames.Contains(ep.Name))
                {
                    retTypeDescriptorProp.SetAttribute("IdentifierName", ep.Name);
                }
                if (ep.PropTypeName == "DateTime")
                {
                    // Interpretation Element
                    XmlElement interpretation = _xmlDoc.CreateElement("Interpretation", _bdcSchema);
                    retTypeDescriptorProp.AppendChild(interpretation);

                    // NormalizeDateTime Element
                    XmlElement normalizeDateTime = _xmlDoc.CreateElement("NormalizeDateTime", _bdcSchema);
                    normalizeDateTime.SetAttribute("LobDateTimeMode", "UTC");
                    interpretation.AppendChild(normalizeDateTime);

                }

                retTypeDescriptorsEnt.AppendChild(retTypeDescriptorProp);
            }

            // Input Parameter Element
            XmlElement inParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
            inParameter.SetAttribute("Direction", "In");
            inParameter.SetAttribute("Name", "param1");
            parameters.AppendChild(inParameter);

            // Entity
            // TypeDescriptor Element
            XmlElement inTypeDescriptorEnt = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            inTypeDescriptorEnt.SetAttribute("TypeName", string.Format("{0}, {1}", this._entityType, _modelName));
            inTypeDescriptorEnt.SetAttribute("Name", _entityName);
            inParameter.AppendChild(inTypeDescriptorEnt);

            // TypeDescriptors Element
            XmlElement inTypeDescriptorsEnt = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            inTypeDescriptorEnt.AppendChild(inTypeDescriptorsEnt);

            // Entity EntityProperties
            foreach (EntityProperties ep in this._entProperties)
            {
                if (!ep.Read) continue;
                if (this._identifierNames.Contains(ep.Name)) continue; // Skip ID

                // TypeDescriptor Element
                XmlElement inTypeDescriptorProp = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                inTypeDescriptorProp.SetAttribute("TypeName", ep.PropTypeFullName);                
                inTypeDescriptorProp.SetAttribute("Name", ep.Name);

                if (!ep.ReadOnly)
                {
                    inTypeDescriptorProp.SetAttribute("CreatorField", "true");
                }
                if (ep.PropTypeName == "DateTime")
                {
                    // Interpretation Element
                    XmlElement interpretation = _xmlDoc.CreateElement("Interpretation", _bdcSchema);
                    inTypeDescriptorProp.AppendChild(interpretation);

                    // NormalizeDateTime Element
                    XmlElement normalizeDateTime = _xmlDoc.CreateElement("NormalizeDateTime", _bdcSchema);
                    normalizeDateTime.SetAttribute("LobDateTimeMode", "UTC");
                    interpretation.AppendChild(normalizeDateTime);
                }

                inTypeDescriptorsEnt.AppendChild(inTypeDescriptorProp);
            }

            // MethodInstances Element
            XmlElement methodInstances = _xmlDoc.CreateElement("MethodInstances", _bdcSchema);
            method.AppendChild(methodInstances);

            // MethodInstance Element
            XmlElement methodInstance = _xmlDoc.CreateElement("MethodInstance", _bdcSchema);
            methodInstance.SetAttribute("Type", "Creator");
            methodInstance.SetAttribute("ReturnParameterName", "returnParameter");
            methodInstance.SetAttribute("Default", "true");
            methodInstance.SetAttribute("Name", "Create");
            methodInstance.SetAttribute("DefaultDisplayName", "Create " + _entityName);
            methodInstances.AppendChild(methodInstance);
        }

        private void CreateDeleteMethod(XmlElement methods)
        {
            // Comment
            XmlComment comment = _xmlDoc.CreateComment("Delete Method");
            methods.AppendChild(comment);

            // Identifier Element
            XmlElement method = _xmlDoc.CreateElement("Method", _bdcSchema);
            method.SetAttribute("Name", "Delete");
            methods.AppendChild(method);

            // Parameters Element
            XmlElement parameters = _xmlDoc.CreateElement("Parameters", _bdcSchema);
            method.AppendChild(parameters);

            // Input Parameter Elements
            for (int i = 0; i < this._identifierNames.Count; i++)
            {
                XmlElement inParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
                inParameter.SetAttribute("Direction", "In");
                inParameter.SetAttribute("Name", string.Format("param{0}", i + 1));
                parameters.AppendChild(inParameter);

                XmlElement typeDescriptorIn = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                typeDescriptorIn.SetAttribute("TypeName", _identifierTypes[i]);
                typeDescriptorIn.SetAttribute("IdentifierName", _identifierNames[i]);
                typeDescriptorIn.SetAttribute("Name", string.Format("id{0}", i + 1));
                inParameter.AppendChild(typeDescriptorIn);
            }

            // MethodInstances Element
            XmlElement methodInstances = _xmlDoc.CreateElement("MethodInstances", _bdcSchema);
            method.AppendChild(methodInstances);

            // MethodInstance Element
            XmlElement methodInstance = _xmlDoc.CreateElement("MethodInstance", _bdcSchema);
            methodInstance.SetAttribute("Type", "Deleter");
            methodInstance.SetAttribute("Name", "Delete");
            methodInstances.AppendChild(methodInstance);
        }

        private void CreateUpdateMethod(XmlElement methods)
        {
            // Comment
            XmlComment comment = _xmlDoc.CreateComment("Update Method");
            methods.AppendChild(comment);

            // Identifier Element
            XmlElement method = _xmlDoc.CreateElement("Method", _bdcSchema);
            method.SetAttribute("Name", "Update");
            methods.AppendChild(method);

            // Parameters Element
            XmlElement parameters = _xmlDoc.CreateElement("Parameters", _bdcSchema);
            method.AppendChild(parameters);
                       
            // Input Parameter Element
            XmlElement inParameter = _xmlDoc.CreateElement("Parameter", _bdcSchema);
            inParameter.SetAttribute("Direction", "In");
            inParameter.SetAttribute("Name", "param1");
            parameters.AppendChild(inParameter);

            // Entity
            // TypeDescriptor Element
            XmlElement inTypeDescriptorEnt = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
            inTypeDescriptorEnt.SetAttribute("TypeName", string.Format("{0}, {1}", this._entityType, _modelName));
            inTypeDescriptorEnt.SetAttribute("Name", _entityName);
            inParameter.AppendChild(inTypeDescriptorEnt);

            // TypeDescriptors Element
            XmlElement inTypeDescriptorsEnt = _xmlDoc.CreateElement("TypeDescriptors", _bdcSchema);
            inTypeDescriptorEnt.AppendChild(inTypeDescriptorsEnt);

            // Entity EntityProperties
            foreach (EntityProperties ep in this._entProperties)
            {
                if (!ep.Read) continue;
                
                // TypeDescriptor Element
                XmlElement inTypeDescriptorProp = _xmlDoc.CreateElement("TypeDescriptor", _bdcSchema);
                inTypeDescriptorProp.SetAttribute("TypeName", ep.PropTypeFullName);
                inTypeDescriptorProp.SetAttribute("UpdaterField", "true");
                inTypeDescriptorProp.SetAttribute("Name", ep.Name);

                if (this._identifierNames.Contains(ep.Name))
                {
                    inTypeDescriptorProp.SetAttribute("IdentifierName", ep.Name);
                    inTypeDescriptorProp.SetAttribute("PreUpdaterField", "true");
                }
                if (ep.PropTypeName == "DateTime")
                {
                    // Interpretation Element
                    XmlElement interpretation = _xmlDoc.CreateElement("Interpretation", _bdcSchema);
                    inTypeDescriptorProp.AppendChild(interpretation);

                    // NormalizeDateTime Element
                    XmlElement normalizeDateTime = _xmlDoc.CreateElement("NormalizeDateTime", _bdcSchema);
                    normalizeDateTime.SetAttribute("LobDateTimeMode", "UTC");
                    interpretation.AppendChild(normalizeDateTime);
                }

                inTypeDescriptorsEnt.AppendChild(inTypeDescriptorProp);
            }

            // MethodInstances Element
            XmlElement methodInstances = _xmlDoc.CreateElement("MethodInstances", _bdcSchema);
            method.AppendChild(methodInstances);

            // MethodInstance Element
            XmlElement methodInstance = _xmlDoc.CreateElement("MethodInstance", _bdcSchema);
            methodInstance.SetAttribute("Type", "Updater");
            methodInstance.SetAttribute("Name", "Update");
            methodInstances.AppendChild(methodInstance);
        }

        public void WriteBDCM()
        {
            // Create directory
            if (!Directory.Exists(_opDir))
                Directory.CreateDirectory(_opDir);

            string opFile = _fileName;

            if(File.Exists(_fileName2))
            {
                opFile = _fileName2;
            }

            // Write document into stream with formatting
            XmlTextWriter xtw = new XmlTextWriter(opFile, Encoding.Default);
            xtw.Formatting = Formatting.Indented;            
            _xmlDoc.WriteTo(xtw);
            xtw.Flush();
        }

        private List<string> GetCodeFile()
        {
            this._codeFile = new List<string>();

            // Write document into stream with formatting
            MemoryStream ms = new MemoryStream(1000000);
            XmlTextWriter xtw = new XmlTextWriter(ms, Encoding.Default);
            xtw.Formatting = Formatting.Indented;
            _xmlDoc.WriteTo(xtw);
            xtw.Flush();

            // Read stram back
            StreamReader sr = new StreamReader(ms);
            sr.BaseStream.Position = 0;
            while (!sr.EndOfStream)
            {
                this._codeFile.Add(sr.ReadLine());
            }
            sr.Close();
            sr.Dispose();

            return this._codeFile;
        }
    }
}
