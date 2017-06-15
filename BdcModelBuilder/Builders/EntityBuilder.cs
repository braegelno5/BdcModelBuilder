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
    public class EntityBuilder
    {
        private Model _model;

        private string fileName = "";
        private string assemblyNS = "";
        private string className = "";
        private string opDir = "";
        private List<EntityProperties> properties = null;
        private List<string> codeFile = null;

        public string ModelNS
        {
            get { return assemblyNS; }
            set { assemblyNS = value; }
        }
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        public string OpDir
        {
            get { return opDir; }
            set { opDir = value; }
        }
        public List<EntityProperties> EntityProperties
        {
            get { return properties; }
            set { properties = value; }
        }
        public List<string> CodeFile
        {
            get { return codeFile; }
            set { codeFile = value; }
        }

        public EntityBuilder(Model model)
        {
            this._model = model;
        }
        public EntityBuilder(string opDir, Model model)
            : this(model)
        {
            this.opDir = opDir;
        }
    
        // Builds entity
        public void BuildEntity()
        {
            // Create file name
            fileName = string.Format("{0}\\{1}Entity.cs", opDir, className);

            this.codeFile = new List<string>();

            // Write header
            this.CreateEntity();        
        }

        /// <summary>
        /// Writes header section
        /// </summary>
        /// <param name="stw"></param>
        private void CreateEntity()
        {
            // Write header
            this.codeFile.Add("// Webber-Cross SharePoint Entity");
            this.codeFile.Add("// Automatically built by WebberCross.BDCModel");
            this.codeFile.Add("");
            this.codeFile.Add("using System;");
            this.codeFile.Add("using System.Collections.Generic;");
            this.codeFile.Add("using System.Linq;");
            this.codeFile.Add("using System.Text;");         
            
            this.codeFile.Add("");
            this.codeFile.Add("namespace " +  assemblyNS);
            this.codeFile.Add("{");          
            this.codeFile.Add("\t[Serializable]");
            this.codeFile.Add("\tpublic class " + className);
            this.codeFile.Add("\t{");           
            
            // Type private variables
            foreach (EntityProperties prop in this.properties.OrderBy(p => p.Name))
            {
                // Convert byte arrays to strings (these are used for SQL Timestamps)
                string propType =prop.PropTypeName;
                if (prop.PropTypeName == "Byte[]")
                    propType = "String";

                string objString = string.Format("\t\tprivate {0} _{1};", propType, prop.Name);
                this.codeFile.Add(objString);
            }

            this.codeFile.Add("");

            // EntityProperties
            foreach (EntityProperties prop in this.properties.OrderBy(p => p.Name))
            {
                // Convert byte arrays to strings (these are used for SQL Timestamps)
                string propType = prop.PropTypeName;
                if (prop.PropTypeName == "Byte[]")
                    propType = "String";

                string objString = string.Format("\t\tpublic {0} {1}", propType, prop.Name);
                this.codeFile.Add(objString);
                this.codeFile.Add("\t\t{");
                objString = string.Format("\t\t\tget {{ return this._{0}; }}", prop.Name);
                this.codeFile.Add(objString);
                objString = string.Format("\t\t\tset {{ this._{0} = value; }}", prop.Name);
                this.codeFile.Add(objString);
                this.codeFile.Add("\t\t}");
            }

            this.codeFile.Add("");

            this.codeFile.Add("");

            // Constructor
            this.codeFile.Add("\t\tpublic " + className + "()");
            this.codeFile.Add("\t\t{");
            this.codeFile.Add("");          
            this.codeFile.Add("\t\t}");
            this.codeFile.Add("");

            // Write footer
            this.codeFile.Add("\t}");
            this.codeFile.Add("}");
        }


        public void WriteEntity()
        {
            // Create directory
            if (!Directory.Exists(opDir))
                Directory.CreateDirectory(opDir);

            //if(File.Exists(_fileName))
            //{
            //    string warning = "Overwrite existing file:\n" + _fileName;
            //    if(MessageBox.Show(warning, "Webber-Cross.RuleGen", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            // Write code file
            StreamWriter stw = new StreamWriter(fileName, false);
            foreach (string s in codeFile)
            {
                stw.WriteLine(s);
            }
            stw.Close();

            // Clear up resources
            stw.Dispose();
            stw = null;
        }
    }
}
