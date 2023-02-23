using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class saveLoadSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu ("Save")]
    public void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }

    [ContextMenu("Load")]
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
    }
    public void SaveFile (object state)
    {
        using (FileStream stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
            stream.Close();
        }
    }

    Dictionary<string, object > LoadFile()
    {
        if(!File.Exists(SavePath))
        {
            Debug.Log("No save file");
            return new Dictionary<string, object>();
        }

        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            Debug.Log("load");
            BinaryFormatter formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
            
        }
    }
    void SaveState (Dictionary<string, object >state)
    {
        foreach (var saveAble in FindObjectsOfType<saveAbleEntity>())
        {
            state[saveAble.ID] = saveAble.SaveState();
        }
    }

    void LoadState (Dictionary<string, object >state)
    {
        foreach (var saveAble in FindObjectsOfType<saveAbleEntity>())
        {
            if (state.TryGetValue(saveAble.ID, out object saveState))
            {
                saveAble.LoadState(saveState);
            }
        }
    }
        
}
