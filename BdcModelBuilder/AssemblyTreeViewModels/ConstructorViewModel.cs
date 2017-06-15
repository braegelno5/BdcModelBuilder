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
    public class ConstructorViewModel : TreeViewItemViewModel
    {
        readonly ConstructorInfo _ci;

        public ConstructorViewModel(int index, ConstructorInfo ci, ConstructorFolderViewModel parent)
            : base(parent, true)
        {
            _ci = ci;
            base._nodeType = ci.DeclaringType;
            base._text = string.Format("Constructor {0}", index.ToString());
            base._name = string.Format("Constructor {0}", index.ToString());
        }

        protected override void LoadChildren()
        {
            // Load parameters
            foreach (ParameterInfo pi in _ci.GetParameters()
                .OrderBy(m => m.Name))
            {
                base.Children.Add(new ParameterViewModel(pi, this));
            }
        }
    }
}
