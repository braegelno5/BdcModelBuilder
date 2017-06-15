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
    public class ParameterFolderViewModel : TreeViewItemViewModel
    {
        readonly MethodInfo _mi;

        public ParameterFolderViewModel(MethodInfo mi, MethodViewModel parent)
            : base(parent, true)
        {
            _mi = mi;
            base._text = "Parameter";
            base._name = "Parameter";
        }

        protected override void LoadChildren()
        {
            // Load parameters
            foreach (ParameterInfo pi in _mi.GetParameters()
                .OrderBy(m => m.Name))
            {
                base.Children.Add(new ParameterViewModel(pi, this));
            }
        }
    }
}
