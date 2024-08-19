using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace LinscEditor.Utilities
{
    internal static class DCSerializer
    {
        public static void ToFile<T>(T instance, string path)
        {
            try
            {
                using FileStream fs = new FileStream(path, FileMode.Create);
                DataContractSerializer dcSerializer = new DataContractSerializer(typeof(T));

                dcSerializer.WriteObject(fs, instance);
            }
            catch (Exception ex)
            {
                //TODO: Show a proper error dialog 
                Debug.WriteLine(ex.Message);
            }
        }

        internal static T FromFile<T>(string path)
        {
            try
            {
                using FileStream fs = new FileStream(path, FileMode.Open);
                DataContractSerializer dcSerializer = new DataContractSerializer(typeof(T));

                return (T) dcSerializer.ReadObject(fs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return default;
            }
        }
    }
}
