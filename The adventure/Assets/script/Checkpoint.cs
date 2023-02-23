using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private LifeCount playerHealth;
    Animator animator;

    private void Awake()
    {
        playerHealth = GetComponent<LifeCount>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("Appear", true);
            mainCharMovement.currCheckpoint = transform.position;
            animator.SetBool("flowing", true);
        }
    }
}
