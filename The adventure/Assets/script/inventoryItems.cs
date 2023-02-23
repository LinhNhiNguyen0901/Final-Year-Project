using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class inventoryItems 
{
    public itemData itemData;
    
    public inventoryItems(itemData item)
    {
        itemData = item;
    }
}
