using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;

namespace Angel
{
    static class DataSaverExtension
    {
        /// <summary>
        /// Writes the given object instance to a binary file.
        /// <para>Object type (and all child types) must be decorated with the [Serializable] attribute.</para>
        /// <para>To prevent a variable from being serialized, decorate it with the [NonSerialized] attribute; cannot be applied to properties.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the binary file.</typeparam>
        /// <param name="fileName">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the binary file.</param>        
        public static void WriteToBinaryFile<T>(string fileName, T objectToWrite)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, objectToWrite);
            }
        }
        /// <summary>
        /// Reads an object instance from a binary file.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="fileName">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the binary file.</returns>
        public static T ReadFromBinaryFile<T>(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (Stream fStream = File.OpenRead(fileName))
            {
                return (T)formatter.Deserialize(fStream);
            }
        }
        /// <summary>
        /// Deletes a file from rootfolder.
        /// </summary>
        /// <typeparam name="T">The type of object to read from the binary file.</typeparam>
        /// <param name="fileName">The file path to read the object instance from.</param>
        public static void DeleteBinaryFile<T>(string filePath)
        {
            string rootFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";            
            var fullPath = rootFolder + filePath;
            File.Delete(fullPath);            
        }
    }
}
