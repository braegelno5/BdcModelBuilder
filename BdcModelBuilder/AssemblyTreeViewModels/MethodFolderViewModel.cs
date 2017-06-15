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
    public class MethodFolderViewModel : TreeViewItemViewModel
    {
        readonly Type _type;

        public MethodFolderViewModel(Type type, ClassViewModel parent)
            : base(parent, true)
        {
            _type = type;
            base._text = "Methods";
            base._name = "Methods";
        }

        protected override void LoadChildren()
        {
            // Load methods
            foreach (MethodInfo mi in _type.GetMethods()
                .OrderBy(m => m.Name)
                .Where(m => m.IsPublic)
                .Where(m => !m.IsStatic)
                .Where(m => !m.Name.StartsWith("get_"))
                .Where(m => !m.Name.StartsWith("set_"))
                .Where(m => !m.Name.StartsWith("add_"))
                .Where(m => !m.Name.StartsWith("remove_")))
            {
                base.Children.Add(new MethodViewModel(mi, this));
            }
        }
    }
}
