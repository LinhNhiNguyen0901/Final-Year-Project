using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {

            Debug.Log("quit");
            Application.Quit();
            Debug.Break();
        }
    }
}
