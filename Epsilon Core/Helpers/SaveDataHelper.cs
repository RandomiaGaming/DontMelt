using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Epsilon
{
    internal static class SaveDataHelper
    {
        internal static SettingsData settings = new SettingsData();

        internal static string GetJson(object target)
        {
            MemoryStream memoryStream = new MemoryStream();
            DataContractJsonSerializer JsonSerializer = new DataContractJsonSerializer(target.GetType());
            JsonSerializer.WriteObject(memoryStream, target);
            return Encoding.ASCII.GetString(memoryStream.ToArray());
        }
        internal static object GetObject(string target, Type targetType)
        {
            try
            {
                byte[] JsonToBytes = Encoding.ASCII.GetBytes(target);
                MemoryStream memoryStream = new MemoryStream(JsonToBytes);
                DataContractJsonSerializer ser1 = new DataContractJsonSerializer(targetType);
                object Output = ser1.ReadObject(memoryStream);
                return Output;
            }
            catch
            {
                return null;
            }
        }
        internal static void WriteToDisk(object target, string path)
        {
            if (path != "")
            {
                FileStream stream = new FileStream(path, FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write(GetJson(target));
                streamWriter.Dispose();
                stream.Close();
            }
        }
        internal static object ReadFromDisk(string path, Type targetType)
        {
            if (File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                StreamReader streamReader = new StreamReader(stream);
                string fileJson = streamReader.ReadToEnd();
                streamReader.Dispose();
                stream.Close();
                return GetObject(fileJson, targetType);
            }
            else
            {
                return null;
            }
        }
        internal static void DeleteFromDisk(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}