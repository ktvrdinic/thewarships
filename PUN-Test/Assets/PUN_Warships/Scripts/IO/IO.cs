using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class IO
{
    public static void Save<T>(T class_to_save, string file_name) where T : class, new()
    {
        BinaryFormatter bin_for = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath/*"C:/Users/Marin/Documents/manana de luca/Input"*/ + "/" + file_name);
        bin_for.Serialize(file, class_to_save);
        file.Close();
    }

    public static T Load<T>(string file_name) where T : class, new()
    {
        BinaryFormatter bin_for = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath/*"C:/Users/Marin/Documents/manana de luca/Input"*/ + "/" + file_name, FileMode.Open);
        T loaded_class = bin_for.Deserialize(file) as T;
        file.Close();
        return loaded_class;
    }

    public static bool File_exist(string file_name)
    {
        return File.Exists(Application.persistentDataPath/*"C:/Users/Marin/Documents/manana de luca/Input"*/ + "/" + file_name);
    }
}
