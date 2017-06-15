// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

// *** Do not change this namespace
namespace WebberCross.BdcModelBuilder.Common
{
    /// <summary>
    /// Summary description for ReflectionUtility.
    /// </summary>
    public class ReflectionUtility
    {
        private static CopyByPropertyAttribute FindAttr(CopyByPropertyAttribute[] attrs, string name)
        {
            CopyByPropertyAttribute bestMatch = null;

            foreach (CopyByPropertyAttribute attr in attrs)
            {
                string currentClass = name.Substring(name.Replace("+", ".").LastIndexOf(".") + 1);
                if (attr.Class != null)
                {
                    int idx = attr.Class.LastIndexOf(".");
                    string destClass = (idx > 0) ? attr.Class.Substring(idx + 1) : attr.Class;

                    if (destClass == currentClass) // Exact match so return
                        return attr;

                    else if (attr.Name == null) // No name so potential default
                        bestMatch = attr;
                }
            }
            return bestMatch;
        }

        private static bool IsUserControl(System.Type baseType)
        {
            if (baseType == null)
                return false;

            if (baseType.Name == "UserControl")
                return true;

            return IsUserControl(baseType.BaseType);
        }

        private static void CopyByProperty(object source, object destination, bool copyNullsInSource)
        {
            Type destinationType = destination.GetType();
            Type sourceType = source.GetType();

            BindingFlags destFlags;

            if (IsUserControl(destinationType))
                destFlags = BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;
            else
                destFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

            PropertyInfo[] destinationProperties = destinationType.GetProperties(destFlags);

            foreach (PropertyInfo destinationProperty in destinationProperties)
            {
                bool copyNulls = copyNullsInSource;
                string pName = destinationProperty.Name;

                // See if we have any attributes against the destination property
                CopyByPropertyAttribute[] destAttributes = (CopyByPropertyAttribute[])(destinationProperty.GetCustomAttributes(typeof(CopyByPropertyAttribute), true));
                if (destAttributes.Length > 0)
                {
                    // Find the best match
                    CopyByPropertyAttribute attr = FindAttr(destAttributes, sourceType.FullName);

                    // We have a match so extract the populated fields in the attribute
                    if (attr != null)
                    {
                        if (attr.Copy == false) continue;

                        copyNulls = attr.AllowNull;

                        if (attr.Name != null)
                            pName = attr.Name;
                    }
                }

                PropertyInfo sourceProperty = sourceType.GetProperty(pName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (sourceProperty != null)
                {
                    try
                    {
                        bool destIsNullable = destinationProperty.PropertyType.GetInterface("INullable") != null;
                        bool srcIsNullable = sourceProperty.PropertyType.GetInterface("INullable") != null;

                        object sourceClass = source;
                        PropertyInfo sourceIsNullProperty = null;

                        // If the source is a Nullable type and the destination is a simple
                        // type drill into the "simple" type inside the Nullable type
                        if (srcIsNullable && !destIsNullable)
                        {
                            sourceClass = sourceProperty.GetValue(source, null);
                            sourceIsNullProperty = sourceProperty.PropertyType.GetProperty("IsNull");
                            sourceProperty = sourceProperty.PropertyType.GetProperty("Value");
                        }

                        // Check we have a source and the types match
                        if (sourceProperty != null && NullableTypeMatch(sourceProperty.PropertyType, destinationProperty.PropertyType))
                        {
                            object sourceValue = null;

                            if (sourceIsNullProperty == null || (bool)(sourceIsNullProperty.GetValue(sourceClass, null)) == false)
                                sourceValue = sourceProperty.GetValue(sourceClass, null);

                            if (copyNulls || sourceValue != null)
                            {
                                if (IsNullable(sourceProperty.PropertyType) && !IsNullable(destinationProperty.PropertyType))
                                    sourceValue = Convert.ChangeType(sourceValue, sourceProperty.PropertyType.GetGenericArguments()[0]);

                                destinationProperty.SetValue(destination, sourceValue, null);
                            }
                        }
                        // Do byte[] to string swop
                        else if (sourceProperty != null 
                            && sourceProperty.PropertyType.Equals(typeof(byte[])) 
                            && destinationProperty.PropertyType.Equals(typeof(string)))
                        {
                            // Convert bytes to string
                            string s = Convert.ToBase64String((byte[])sourceProperty.GetValue(sourceClass, null));

                            // Set value
                            destinationProperty.SetValue(destination, s, null);
                        }
                        // Do string to byte[] swop
                        else if (sourceProperty != null
                            && sourceProperty.PropertyType.Equals(typeof(string))
                            && destinationProperty.PropertyType.Equals(typeof(byte[])))
                        {
                            // Convert bytes to string
                            byte[] b = Convert.FromBase64String((string)sourceProperty.GetValue(sourceClass, null));

                            // Set value
                            destinationProperty.SetValue(destination, b, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        //throw new System.Exception( "CopyByProperty : " + pName + " : " + ex.Message, ex );
                    }
                }
            }
        }

        /// <summary>
        /// Copy values (matching property by property) from source to destination. Assumes
        /// that source and destination properties have matching CASE and NULL Source
        /// values are skipped.
        /// </summary>
        /// <param name="source">Source object with properties to copy</param>
        /// <param name="destination">Destination object with matching properties</param>
        public static void CopyByProperty(object source, object destination)
        {
            CopyByProperty(source, destination, false);
        }

        /// <summary>
        /// Copy values (matching property by property) from source to destination. Assumes
        /// that source and destination properties have matching CASE and NULL Source
        /// values are assigned to the destination.
        /// </summary>
        /// <param name="source">Source object with properties to copy</param>
        /// <param name="destination">Destination object with matching properties</param>
        public static void CopyByPropertyIncludingNulls(object source, object destination)
        {
            CopyByProperty(source, destination, true);
        }

        public static bool IsNullable(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }

            return false;
        }

        public static bool NullableTypeMatch(Type a, Type b)
        {
            Type aBase = a;
            Type bBase = b;

            if (IsNullable(a))
                aBase = a.GetGenericArguments()[0];

            if (IsNullable(b))
                bBase = b.GetGenericArguments()[0];

            if (aBase.Equals(bBase))
                return true;

            return false;
        }

        public static Type GetUnderlyingType(Type t)
        {
            if (IsNullable(t))
                return t.GetGenericArguments()[0];

            return t;
        }

        /// <summary>
        /// Fixes dates to over 01/01/1900
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static void FixDatesForSharePoint(object o)
        {
            var props = o.GetType().GetProperties().Where(p => p.PropertyType.Equals(typeof(DateTime)));
            foreach (PropertyInfo e in props)
            {
                DateTime dt = (DateTime)e.GetValue(o, null);

                if (dt < new DateTime(1900, 1, 1))
                {
                    e.SetValue(o, new DateTime(1900, 1, 1), null);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CopyByPropertyAttribute : System.Attribute
    {
        private bool _allowNull = true;
        private string _Name;
        private string _Class;

        /// <summary></summary>
        public readonly bool Copy = true;

        /// <summary>
        /// 
        /// </summary>
        public bool AllowNull
        {
            get { return _allowNull; }
            set { _allowNull = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Class
        {
            get { return _Class; }
            set { _Class = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copy"></param>
        public CopyByPropertyAttribute(bool copy)
        {
            Copy = copy;
        }        
    }
}
