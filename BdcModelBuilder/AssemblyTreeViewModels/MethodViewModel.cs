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
    public class MethodViewModel : TreeViewItemViewModel
    {
        readonly MethodInfo _mi;

        public MethodViewModel(MethodInfo mi, MethodFolderViewModel parent)
            : base(parent, true)
        {
            _mi = mi;
            base._nodeType = mi.ReturnType;
            base._text = _mi.Name;
            base._name = _mi.Name;
        }

        protected override void LoadChildren()
        {
            // Add return type folder
            base.Children.Add(new ReturnTypeFolderViewModel(_mi, this));

            // Add parameters folder
            base.Children.Add(new ParameterFolderViewModel(_mi, this));
        }
    }
}
