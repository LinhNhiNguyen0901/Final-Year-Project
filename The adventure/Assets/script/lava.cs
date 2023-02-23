using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour
{
    public float time = 2;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if( time <= 0)
            {
                collision.GetComponent<LifeCount>().LoseLife();
                time = 2;
            }
            else
                time -= Time.deltaTime;
        }
    }

}
