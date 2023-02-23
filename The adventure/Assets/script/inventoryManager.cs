using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryManager : MonoBehaviour
{
    public GameObject slotPrefabs;
    public List<inventorySlot> inventorySlot = new List<inventorySlot>(8);

    private void OnEnable()
    {
        inventory.OnInventoryChange += DrawInventory;
    }

    private void OnDisable()
    {
        inventory.OnInventoryChange -= DrawInventory;
    }
    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
            Destroy(childTransform.gameObject);
        inventorySlot = new List<inventorySlot>(8);
    }

    void DrawInventory(List<inventoryItems> inventory)
    {
        ResetInventory();
        for (int i = 0; i < inventorySlot.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for(int i = 0; i < inventory.Count; i++)
        {
            inventorySlot[i].DrawSlot(inventory[i]);
        }
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefabs);
        slotPrefabs = newSlot;
        
        newSlot.transform.SetParent(transform, false);

        inventorySlot newSlotComponent = newSlot.GetComponent<inventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlot.Add(newSlotComponent);
    }
}
