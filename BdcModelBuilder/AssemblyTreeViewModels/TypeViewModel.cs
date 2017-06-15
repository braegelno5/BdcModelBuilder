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
    public class TypeViewModel : TreeViewItemViewModel
    {
        public TypeViewModel(Type t, TreeViewItemViewModel parent)
            : base(parent, true)
        {
            base._nodeType = t;
            base._text = t.Name;
            base._name = t.Name;
        }

        protected override void LoadChildren()
        {
            foreach (PropertyInfo pi in base._nodeType.GetProperties().Where(p => p.CanRead))
                base.Children.Add(new PropertyViewModel(pi, this));
        }

    }
}
