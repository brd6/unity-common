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
        XML,
        JSON
    }

    public class DataSerializer
    {
        /// <summary>
        /// Save data to a file format
        /// </summary>
        /// <typeparam name="T">Data template type</typeparam>
        /// <param name="type">Data type</param>
        /// <param name="data">Data</param>
        /// <param name="filename">File path to save the data. e.g : Application.persistentDataPath + "/myfile"</param>
        public static void SaveData<T>(DataSerializeType type, T data, string filename)
        {
            switch (type)
            {
                case DataSerializeType.BINARY:
                    SaveBinary<T>(data, filename);
                    break;
                case DataSerializeType.XML:
                    SaveXML<T>(data, filename);
                    break;
                case DataSerializeType.JSON:
                    SaveJson<T>(data, filename);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Load a data from file format
        /// </summary>
        /// <typeparam name="T">Data template type</typeparam>
        /// <param name="type">Data type</param>
        /// <param name="filename">File path to load the data, e.g : Application.persistentDataPath + "/myfile"</param>
        /// <returns></returns>
        public static T LoadData<T>(DataSerializeType type, string filename)
        {
            switch (type)
            {
                case DataSerializeType.BINARY:
                    return LoadBinary<T>(filename);
                case DataSerializeType.XML:
                    return LoadXML<T>(filename);
                case DataSerializeType.JSON:
                    return LoadJson<T>(filename);
                default:
                    break;
            }

            return default(T);
        }

        public static void SaveJson<T>(T data, string filename)
        {
            string jsonData = JsonUtility.ToJson(data);

            File.WriteAllText(filename, jsonData);
        }

        public static T LoadJson<T>(string filename)
        {
            string jsonData = File.ReadAllText(filename);

            T data = JsonUtility.FromJson<T>(jsonData);

            return data;
        }

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

        /// <summary>
        /// Save data to binary file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="filename"></param>
        public static void SaveBinary<T>(T data, string filename)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Create(filename);

            binaryFormatter.Serialize(fileStream, data);
            fileStream.Close();
        }

        /// <summary>
        /// Load data from binary file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
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