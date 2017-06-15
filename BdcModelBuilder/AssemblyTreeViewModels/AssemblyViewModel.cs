// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Reflection;

using WebberCross.BdcModelBuilder.AppModels;

namespace WebberCross.BdcModelBuilder.ViewModels
{
    public class AssemblyViewModel
    {
        readonly ReadOnlyCollection<ClassViewModel> _classes;

        public AssemblyViewModel(string assemblyPath)
        {
            // Load types from assembly
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += new ResolveEventHandler(this.AppDomain_ResolveEventHandler);
            Assembly a = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                        
            _classes = new ReadOnlyCollection<ClassViewModel>(
                a.GetTypes()
                .Where(t => t.IsClass)
                .OrderBy(t => t.Name)
                .Select(t => new ClassViewModel(t))
                .ToList());
        }

        private Assembly AppDomain_ResolveEventHandler(object sender, ResolveEventArgs e)
        {
            return Assembly.ReflectionOnlyLoad(e.Name);
        }

        public ReadOnlyCollection<ClassViewModel> Classes
        {
            get { return _classes; }
        }

        // Business methods
        public Type GetSelectedType()
        {
            // Loop through top level
            foreach (ClassViewModel cvm in _classes)
            {
                if (cvm.IsChecked)
                {
                    return cvm.NodeType;
                }
            }

            return null;
        }

        public InstanceNode GetInstanceRoot()
        {
            InstanceNode rootInstance = new InstanceNode("Root", null);
            rootInstance.InstancePaths = new ObservableCollection<InstancePath>();
            rootInstance.ConstructorParameters = new ObservableCollection<InstanceNode>();

            string path = "";

            // Loop through top level
            foreach (ClassViewModel cvm in _classes)
            {
                if (cvm.IsChecked)
                {
                    // Add instance to tree
                    InstanceNode nextINode = new InstanceNode(cvm.Name, cvm.NodeType);
                    rootInstance.NestedInstances.Add(nextINode);

                    // Add to path
                    if (path != "")
                        path += ".";

                    path += cvm.Text;

                     // Loop through folder level
                    foreach (TreeViewItemViewModel folder in cvm.Children)
                    {
                        if (folder.IsChecked)
                        {
                            if (folder is PropertyFolderViewModel)
                            {
                                // Recursive call on properties
                                this.LoadInstancePaths(folder, nextINode, rootInstance.InstancePaths, path);
                            }
                            else if (folder is ConstructorFolderViewModel)
                            {
                                // Loop through folder level
                                foreach (TreeViewItemViewModel constructor in folder.Children)
                                {
                                    if (constructor.IsChecked)
                                    {
                                        // Loop through parameter level
                                        foreach (TreeViewItemViewModel parameter in constructor.Children)
                                        {
                                            // Add instance to tree
                                            InstanceNode paramNode = new InstanceNode(parameter.Name, parameter.NodeType);
                                            rootInstance.ConstructorParameters.Add(paramNode);
                                        }

                                    }
                                }
                            }

                            break;
                        }
                    }
                }
            }

            return rootInstance;
        }

        private void LoadInstancePaths(TreeViewItemViewModel node, InstanceNode iNode, ObservableCollection<InstancePath> flatPath, string path)
        {
            InstanceNode nextINode = iNode;

            if (node.IsChecked && node.NodeType != null)
            {
                // Add instance to tree
                nextINode = new InstanceNode(node.Name, node.NodeType);
                iNode.NestedInstances.Add(nextINode);

                // Add to path
                if (path != "")
                    path += ".";

                path += node.Name;

                // Add flat path
                if (this.LastCheckedNode(node))
                {
                    flatPath.Add(new InstancePath(path, nextINode));
                }
            }

            if (node.Children == null) return;
            foreach (TreeViewItemViewModel tn in node.Children)
            {
                // Recursive call
                this.LoadInstancePaths(tn, nextINode, flatPath, path);
            }
        }

        private bool LastCheckedNode(TreeViewItemViewModel node)
        {
            foreach (TreeViewItemViewModel tn in node.Children)
            {
                if (tn.IsChecked)
                {
                    return false;
                }
            }

            return true;
        }

        public MethodDetails GetMethodDetails(MethodDetails mDetails)
        {
            // Loop through types
            foreach (TreeViewItemViewModel tnType in _classes)
            {
                if (tnType.IsChecked)
                {
                    // Loop through type folders
                    foreach (TreeViewItemViewModel tnTypeSub in tnType.Children)
                    {
                        if (tnTypeSub.IsChecked && tnTypeSub is MethodFolderViewModel)
                        {
                            // Loop through methods
                            foreach (TreeViewItemViewModel tnMethod in tnTypeSub.Children)
                            {
                                if (tnMethod.IsChecked)
                                {
                                    mDetails.MethodName = tnMethod.Text;
                                    mDetails.Include = true;

                                    // Loop through method folders
                                    foreach (TreeViewItemViewModel tnMethodSub in tnMethod.Children)
                                    {
                                        if (tnMethodSub.IsChecked && tnMethodSub is ReturnTypeFolderViewModel)
                                        {
                                            // Look for return type
                                            mDetails.ReturnObject = new InstanceNode("Root", null);
                                            mDetails.ReturnObject.InstancePaths = new ObservableCollection<InstancePath>();
                                            this.GetMethodReturnParameter(tnMethodSub, mDetails);
                                            this.LoadInstancePaths(tnMethodSub, mDetails.ReturnObject, mDetails.ReturnObject.InstancePaths, "");
                                        }
                                        else if (tnMethodSub.IsChecked && tnMethodSub is ParameterFolderViewModel)
                                        {
                                            mDetails.MethodParams = new InstanceNode("Root", null);
                                            mDetails.MethodParams.InstancePaths = new ObservableCollection<InstancePath>();

                                            // Loop through parameters
                                            foreach (TreeViewItemViewModel tnParam in tnMethodSub.Children)
                                            {
                                                if (tnParam.IsChecked)
                                                {
                                                    // Recursive call
                                                    this.LoadInstancePaths(tnParam, mDetails.MethodParams, mDetails.MethodParams.InstancePaths, "");
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            return mDetails;
        }

        private void GetMethodReturnParameter(TreeViewItemViewModel node, MethodDetails mDetails)
        {
            // If it's the last node, use it's type
            if (node.IsChecked)
            {
                if (this.LastCheckedNode(node))
                {
                    mDetails.ReturnTypeFullName = node.NodeType.FullName;
                    mDetails.ReturnTypeName = node.NodeType.Name;
                    mDetails.IsArray = node.NodeType.IsArray;

                    if (node.NodeType.IsArray)
                        mDetails.ElementTypeName = node.NodeType.GetElementType().Name;

                    return;
                }

                // Recursive call
                if (node.Children == null) return;
                foreach (TreeViewItemViewModel tn in node.Children)
                {
                    this.GetMethodReturnParameter(tn, mDetails);
                }
            }
        }

        
    }
}
