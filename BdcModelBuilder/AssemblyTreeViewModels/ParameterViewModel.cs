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
    public class ParameterViewModel : TreeViewItemViewModel
    {
        readonly ParameterInfo _pi;

        public ParameterViewModel(ParameterInfo pi, TreeViewItemViewModel parent)
            : base(parent, true)
        {
            _pi = pi;
            base._nodeType = pi.ParameterType;
            base._text = string.Format("[{0}] {1}", pi.ParameterType.Name, _pi.Name);
            base._name = _pi.Name;
        }

        protected override void LoadChildren()
        {
            foreach (PropertyInfo pi in _pi.ParameterType.GetProperties().Where(p => p.CanRead))
                base.Children.Add(new PropertyViewModel(pi, this));
        }

    }
}
