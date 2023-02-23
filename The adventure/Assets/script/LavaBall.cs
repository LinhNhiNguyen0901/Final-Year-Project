using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity = 40;
    public float tempo ;
    public bool isTouching;

    public Collider2D OnCollider, OffCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (tempo <= 0)
        {
            lavaBall();
        }
        else
            tempo -= Time.deltaTime;
    }

    void lavaBall()
    {
        tempo = 5f;
        rb.velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTouching)
        {
            if (collision.tag == "Player")
            {  
                isTouching = true;
                collision.GetComponent<LifeCount>().LoseLife();
            }
        }

    }
}
