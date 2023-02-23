using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventorySlot : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    public Image bg;

    public void ClearSlot()
    {
        bg.enabled = true;
        icon.enabled = false;

    }

    public void DrawSlot(inventoryItems item)
    { 
       if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        icon.sprite = item.itemData.icon;
    }
}
