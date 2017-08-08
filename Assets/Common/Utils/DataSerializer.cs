using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using UnityEngine;

namespace Common
{
    public enum DataSerializeType
    {
        BINARY,
        XML
    }

    public class DataSerializer
    {
        /// <summary>
        /// Save data to XML file
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="filename"></param>
        public static void SaveXML<T>(T data, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            FileStream fileStream = new FileStream(filename, FileMode.Create);

            xmlSerializer.Serialize(fileStream, data);
            fileStream.Close();
        }

        /// <summary>
        /// Load data from XML file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T LoadXML<T>(string filename)
        {
            if (!File.Exists(filename))
                return default(T);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            FileStream fileStream = new FileStream(filename, FileMode.Open);

            T data = (T)xmlSerializer.Deserialize(fileStream);
            fileStream.Close();

            return data;
        }

        public static void SaveBinary<T>(T data, string filename)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(filename);

            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        public static T LoadBinary<T>(string filename)
        {
            if (!File.Exists(filename))
                return default(T);

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(filename, FileMode.Open);

            T data = (T)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();

            return data;
        }

    }
}