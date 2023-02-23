using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemCollect : MonoBehaviour
{
    // It's just a delegate name so it doesn't matter what it is called
    public delegate void HandleGemCollected(itemData itemData);
    public static event HandleGemCollected OnGemCollected;
    public itemData gemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        OnGemCollected?.Invoke(gemData);
    }
}
