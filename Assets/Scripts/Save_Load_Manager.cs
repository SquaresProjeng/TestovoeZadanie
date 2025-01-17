using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;


[System.Serializable]
public class SaveData
{
    public int Score; 
}

public class Save_Load_Manager : MonoBehaviour
{
    private string filePath;

    public void Save(int value)
    {
        SaveData data = new SaveData();
        data.Score = value;

        if (!Directory.Exists(Application.dataPath + "/saves"))
            Directory.CreateDirectory(Application.dataPath + "/saves");

        FileStream fs = new FileStream(Application.dataPath + "/saves/savefile.json", FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fs, data);
        fs.Close();
    }

    public int Load()
    {
        int loadint = 0;
        if (File.Exists(Application.dataPath + "/saves/savefile.json"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/savefile.json", FileMode.OpenOrCreate);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                SaveData data = (SaveData)formatter.Deserialize(fs);
                loadint = data.Score;
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        return loadint;
    }
}

