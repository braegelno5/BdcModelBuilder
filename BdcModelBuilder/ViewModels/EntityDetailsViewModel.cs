// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebberCross.BdcModelBuilder.AppModels;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public delegate void EntityDetailsChangedHandler(EntityDetails details);

    public class EntityDetailsViewModel
    {
        private RelayCommand _getEntityDetailsCommand = null;
        public event EntityDetailsChangedHandler EntityDetailsChanged;
        private string _assemblyPath = "";

        public string AssemblyPath
        {
            set { this._assemblyPath = value; }
        }

        public EntityDetailsViewModel()
        {

        }

        #region Commands

        public ICommand GetEntityDetailsCommand
        {
            get
            {
                if (_getEntityDetailsCommand == null)
                {
                    _getEntityDetailsCommand = new RelayCommand(
                        param => this.GetEntityDetails(),
                        param => true
                        );
                }
                return _getEntityDetailsCommand;
            }
        }


        #endregion

        private void GetEntityDetails()
        {
            AssemblyTree tree = new AssemblyTree(this._assemblyPath);

            tree.ShowDialog();

            if (tree.DialogResult.HasValue && tree.DialogResult.Value)
            {
                // Load text
                Type t = tree.ViewModel.GetSelectedType();

                if (t != null && this.EntityDetailsChanged != null)
                {
                    EntityDetails ed = new EntityDetails();

                    ed.EntitySource = t.FullName;
                    ed.EntityName = t.Name + "Entity";
                    ed.DisplayName = t.Name + "Entity";


                    // Load Properties
                    PropertyInfo[] pi = t.GetProperties();
                    ed.EntityProps = new ObservableCollection<EntityProperties>(EntityProperties.GetEntityPropertiesList(pi)
                        .Where(p => p.PropTypeFullName.StartsWith("System") == true)
                        .OrderBy(p => p.Name));

                    this.EntityDetailsChanged(ed);
                }
            }
        }
    }
}
