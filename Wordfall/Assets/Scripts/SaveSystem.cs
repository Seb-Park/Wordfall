using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SaveBoolArray (bool[] boolArray, string filename){//array you want to save + the name of the file
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, boolArray);
        stream.Close();
    }

    public static bool[] LoadBoolArray(string filename)
    {//array you want to save + the name of the file
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            bool[] returnableBool = formatter.Deserialize(stream) as bool[];

            stream.Close();

            return returnableBool;
        }
        else{
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveObject(Object obj, string filename){
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, obj);
        stream.Close();
    }

    public static Object LoadObject(string filename)
    {
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Object returnableObject = formatter.Deserialize(stream) as Object;

            stream.Close();

            return returnableObject;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void SaveInt(int number, string filename)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + filename;
        FileStream stream = new FileStream(path, FileMode.Create);
        bf.Serialize(stream, number);
        stream.Close();
    }

    public static int? LoadInt(string filename){
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int? returnableInt = formatter.Deserialize(stream) as int?;

            stream.Close();

            return returnableInt;
        }
        else
        {
            Debug.Log("Save file not found in " + path + "!");
            return null;
        }
    }

    public static int? LoadInt(string filename, int? def)
    {
        string path = Application.persistentDataPath + "/" + filename;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int? returnableInt = formatter.Deserialize(stream) as int?;

            stream.Close();

            return returnableInt;
        }
        else
        {
            Debug.Log("Save file not found in " + path + "!");
            return def;
        }
    }
}
