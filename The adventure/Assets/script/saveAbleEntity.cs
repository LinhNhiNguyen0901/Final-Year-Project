using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class saveAbleEntity : MonoBehaviour
{
    [SerializeField] private string id;
    public string ID => id;

    [ContextMenu ("GenerateID")]
    private void GenerateID()
    {
        id = Guid.NewGuid().ToString();
    }

    public object SaveState()
    {
        var state = new Dictionary<string, object>();
        foreach (var saveAble in GetComponents<saveAble>())
        {
            state[saveAble.GetType().ToString()] = saveAble.SaveState();
        }
        return state;
    }

    public void LoadState(object state)
    {
        var stateDictionary = (Dictionary<string, object>)state;
         foreach (var saveAble in GetComponents<saveAble>())
        {
            string typeName = saveAble.GetType().ToString();
            if (stateDictionary.TryGetValue(typeName, out object saveState))
            {
                saveAble.LoadState(saveState);
            }
        }
    }
}
