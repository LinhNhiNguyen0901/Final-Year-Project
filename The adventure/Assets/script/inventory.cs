using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    public static event Action<List<inventoryItems>> OnInventoryChange;
    public List<inventoryItems> inven = new List<inventoryItems>();
    private Dictionary<itemData, inventoryItems> itemDictionary = new Dictionary<itemData, inventoryItems>();

    private void OnEnable()
    {
        gemCollect.OnGemCollected += Add;

    }

    private void OnDisable()
    {
        gemCollect.OnGemCollected -= Add;
    }

    public void Add(itemData itemData)
    {
        inventoryItems newItem = new inventoryItems(itemData);
        inven.Add(newItem);
        itemDictionary.Add(itemData, newItem);
        //Debug.Log($"Added {itemData.displayName} to the inventory.");
        OnInventoryChange?.Invoke(inven);
    }

    public void Remove (itemData itemData)
    {
        if(itemDictionary.TryGetValue (itemData, out inventoryItems item))
        {
            inven.Remove(item);
            itemDictionary.Remove(itemData);
            OnInventoryChange?.Invoke(inven);

        }
    }

    public void LoadData(gameData data)
    {
        this.inven = data.inven;
    }

    public void SaveData(ref gameData data)
    {
        data.inven = this.inven;
    }
}
