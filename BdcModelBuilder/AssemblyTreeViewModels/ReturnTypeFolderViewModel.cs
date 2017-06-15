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
    public class ReturnTypeFolderViewModel : TreeViewItemViewModel
    {
        readonly MethodInfo _mi;

        public ReturnTypeFolderViewModel(MethodInfo mi, MethodViewModel parent)
            : base(parent, true)
        {
            _mi = mi;
            base._text = "Return Type";
            base._name = "Return Type";
        }

        protected override void LoadChildren()
        {
            // Load return type
            base.Children.Add(new TypeViewModel(_mi.ReturnType, this));
        }
    }
}
