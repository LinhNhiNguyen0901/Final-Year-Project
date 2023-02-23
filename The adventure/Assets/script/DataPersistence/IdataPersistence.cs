using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IdataPersistence 
{
    void LoadData(gameData data);
    void SaveData(gameData data);
}
