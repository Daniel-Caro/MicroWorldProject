using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    private static string dataPath = Application.persistentDataPath + "/game.save";
    private static BinaryFormatter binaryFormatter = new BinaryFormatter();
    public static void SaveGameData()
    {
        Debug.Log("Datos guardados");
        SavedData savedData = new SavedData();
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        binaryFormatter.Serialize(fileStream, savedData);
        fileStream.Close();
    }

    public static SavedData LoadGameData()
    {
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            SavedData savedData = (SavedData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return savedData;
        }
        else
        {
            Debug.Log("No se encontro archivo de guardado");
            return null;
        }
    }
}
