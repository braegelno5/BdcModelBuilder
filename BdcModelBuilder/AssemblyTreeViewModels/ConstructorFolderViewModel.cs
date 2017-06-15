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
    public class ConstructorFolderViewModel : TreeViewItemViewModel
    {
        readonly Type _type;

        public ConstructorFolderViewModel(Type type, ClassViewModel parent)
            : base(parent, true)
        {
            _type = type;
            base._text = "Constructors";
            base._name = "Constructors";
        }

        protected override void LoadChildren()
        {
            int index = 0;

            // Load methods
            foreach (ConstructorInfo ci in _type.GetConstructors()
                .OrderBy(c => c.Name)
                .Where(c => c.IsPublic)
                .Where(c => !c.IsStatic)
                .Where(c => c.GetParameters().Length > 0))
            {
                base.Children.Add(new ConstructorViewModel(index, ci, this));

                index++;
            }
        }
    }
}
