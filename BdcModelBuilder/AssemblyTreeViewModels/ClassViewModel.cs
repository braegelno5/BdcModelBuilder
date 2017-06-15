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
    public class ClassViewModel : TreeViewItemViewModel
    {
        public ClassViewModel(Type type)
            : base(null, true)
        {
            base._nodeType = type;
            base._text = base._nodeType.Name;
            base._name = base._nodeType.Name;
        }

        protected override void LoadChildren()
        {
            // Load constructors
            base.Children.Add(new ConstructorFolderViewModel(base._nodeType, this));

            // Load properties
            base.Children.Add(new PropertyFolderViewModel(base._nodeType, this));

            // Load methods
            base.Children.Add(new MethodFolderViewModel(base._nodeType, this));
        }
    }
}
