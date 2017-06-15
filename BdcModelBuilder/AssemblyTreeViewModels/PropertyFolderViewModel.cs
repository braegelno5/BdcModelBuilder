// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public class PropertyFolderViewModel : TreeViewItemViewModel
    {
        readonly Type _type;

        public PropertyFolderViewModel(Type type, ClassViewModel parent)
            : base(parent, true)
        {
            _type = type;
            base._text = "Properties";
            base._name = "Properties";
        }

        protected override void LoadChildren()
        {
            // Load properties
            foreach (PropertyInfo pi in _type.GetProperties().Where(p => p.CanRead))
                base.Children.Add(new PropertyViewModel(pi, this));
        }
    }
}
