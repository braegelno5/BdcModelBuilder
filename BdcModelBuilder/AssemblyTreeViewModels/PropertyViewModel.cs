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
    public class PropertyViewModel : TreeViewItemViewModel
    {
        readonly PropertyInfo _pi;

        public PropertyViewModel(PropertyInfo pi, TreeViewItemViewModel parent)
            : base(parent, true)
        {
            _pi = pi;
            base._nodeType = pi.PropertyType;
            base._text = string.Format("[{0}] {1}", pi.PropertyType.Name, _pi.Name);
            base._name = _pi.Name;
        }

        protected override void LoadChildren()
        {
            foreach (PropertyInfo pi in _pi.PropertyType.GetProperties().Where(p => p.CanRead))
                base.Children.Add(new PropertyViewModel(pi, this));
        }

    }
}
