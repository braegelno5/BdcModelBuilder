// (c) Copyright Webber-Cross Software Ltd.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

// *** Do not change this namespace
namespace WebberCross.BdcModelBuilder.Common
{
    /// <summary>
    /// Provides a method for performing a deep copy of an object.
    /// Binary Serialization is used to perform the copy.
    /// </summary>
    public static class DataContractCopy
    {
        /// <summary>
        /// Perform a deep Copy of the object.
        /// </summary>
        /// <typeparam name="T">The type of object being copied.</typeparam>
        /// <param name="source">The object instance to copy.</param>
        /// <returns>The copied object.</returns>
        public static T Clone<T>(T source)
        {
            // Don't copy a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            MemoryStream stream = new MemoryStream();

            // Serialise
            NetDataContractSerializer ser = new NetDataContractSerializer();
            ser.WriteObject(stream, source);

            // Deserialise
            stream.Position = 0;
            T retVal = (T)ser.ReadObject(stream);

            stream.Close();
            stream.Dispose();

            return retVal;
        }

        public static byte[] GetBytes<T>(T source)
        {
            // Don't copy a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return null;
            }

            MemoryStream stream = new MemoryStream();

            // Serialise
            NetDataContractSerializer ser = new NetDataContractSerializer();
            ser.WriteObject(stream, source);

            return stream.ToArray();
        }

        public static T Deserialise<T>(byte[] source)
        {
            // Don't copy a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            MemoryStream stream = new MemoryStream(source);

            // Serialise
            NetDataContractSerializer ser = new NetDataContractSerializer();

            // Deserialise
            stream.Position = 0;
            T retVal = (T)ser.ReadObject(stream);

            stream.Close();
            stream.Dispose();

            return retVal;
        }

        public static string SerialiseToString<T>(T source)
        {
            // Don't serialise a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return null;
            }

            // Serialise to string
            StringBuilder serialXML = new StringBuilder(); 
            DataContractSerializer dcSerializer = new DataContractSerializer(typeof(T)); 
            using (XmlWriter xWriter = XmlWriter.Create(serialXML)) 
            {
                dcSerializer.WriteObject(xWriter, source); 
                xWriter.Flush(); 
                return serialXML.ToString(); 
            }
        }
    }
}
