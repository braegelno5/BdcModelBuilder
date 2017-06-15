// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WebberCross.BdcModelBuilder.AppModels;

namespace WebberCross.BdcModelBuilder.Builders
{
    public class Builder
    {
        private ModelContainer _mContainer = null;        

        public Builder(ModelContainer mContainer)
        {
            this._mContainer = mContainer;
        }

        /// <summary>
        /// Builds Entity for Curent Model
        /// </summary>
        /// <param name="write"></param>
        /// <returns></returns>
        public List<string> BuildEntity(bool write)
        {
            if (this._mContainer == null) return null;

            return this.BuildEntity(this._mContainer.CurrentModel, write);
        }

        public List<string> BuildEntity(Model model, bool write)
        {
            if (model == null) return null;            

            EntityBuilder eb = new EntityBuilder(model);
            eb.ModelNS = model.ModelNS;
            eb.ClassName = model.EntityDetails.EntityName;
            eb.EntityProperties = model.EntityDetails.EntityProps.Where(p => p.Read == true).ToList();
            eb.OpDir = string.Format("{0}\\{1}", model.OpPath, model.Name);
            eb.BuildEntity();

            // Write
            if (write) eb.WriteEntity();

            return eb.CodeFile;
        }

        /// <summary>
        /// Builds BDCM for Current Model
        /// </summary>
        /// <param name="write"></param>
        /// <returns></returns>
        public List<string> BuildBDCM(bool write)
        {
            if (this._mContainer == null) return null;

            return this.BuildBDCM(this._mContainer.CurrentModel, write);
        }

        public List<string> BuildBDCM(Model model, bool write)
        {
            if (model == null) return null;

            BDCMBuilder bb = new BDCMBuilder();
            bb.ModelNS = model.ModelNS;
            bb.ModelName = model.Name;
            bb.EntityProperties = model.EntityDetails.EntityProps.Where(p => p.Read == true).ToList();
            bb.IdentifierName = model.EntityDetails.GetIdentifierNames();
            bb.IdentifierType = model.EntityDetails.GetIdentifierTypes();
            bb.EntityName = model.EntityDetails.EntityName;
            bb.EntityType = string.Format("{0}.{1}", model.ModelNS, model.EntityDetails.EntityName);
            bb.OpDir = string.Format("{0}\\{1}", model.OpPath, model.Name);
            bb.Methods = model.Methods;

            bb.BuildBDCM();

            // Write
            if (write) bb.WriteBDCM();

            return bb.CodeFile;
        }

        /// <summary>
        /// Builds Entity Service for Current Model
        /// </summary>
        /// <param name="write"></param>
        /// <returns></returns>
        public List<string> BuildEntityService(bool write)
        {
            if (this._mContainer == null) return null;

            return this.BuildEntityService(this._mContainer.CurrentModel, write);
        }

        public List<string> BuildEntityService(Model model, bool write)
        {
            if (model == null) return null;
            if (model.DataSourceInstance.NestedInstances.Count == 0) return null;

            EntityServiceBuilder esb = new EntityServiceBuilder(model);
            esb.ModelNS = model.ModelNS;
            esb.ClassName = model.EntityDetails.EntityName + "Service";
            esb.EntityProperties = model.EntityDetails.EntityProps.Where(p => p.Read == true).ToList();
            esb.IdentifierNames = model.EntityDetails.GetIdentifierNames();
            esb.IdentifierTypes = model.EntityDetails.GetIdentifierTypes();
            esb.EntityName = model.EntityDetails.EntityName;
            esb.TargetNameSpace = model.TargetNameSpace;
            esb.OpDir = string.Format("{0}\\{1}", model.OpPath, model.Name);
            esb.RootInstance = model.DataSourceInstance;
            esb.Methods = model.Methods;

            esb.BuildEntityService();

            // Write
            if (write) esb.WriteEntity();

            return esb.CodeFile;
        }
        
        /// <summary>
        /// Write All CurrentModel Files
        /// </summary>
        public void WriteAll()
        {
            foreach (Model m in this._mContainer.Models)
            {
                this.BuildEntity(m, true);
                this.BuildBDCM(m, true);
                this.BuildEntityService(m, true);
            }
        }
    }
}
