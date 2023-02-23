using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpingEnemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity = 40;
    public float tempo;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (tempo <= 0)
        {
            JumpEnemy();
        }
        else
            tempo -= Time.deltaTime;
    }

    void JumpEnemy()
    {
        tempo = 2f;
        transform.position = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GetComponent<LifeCount>().LoseLife();
    }
}
