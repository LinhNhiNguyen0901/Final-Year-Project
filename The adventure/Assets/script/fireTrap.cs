using System.Collections;
using UnityEngine;

public class fireTrap : MonoBehaviour
{
    [SerializeField] private float activeDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());
            if (active)
                collision.GetComponent<LifeCount>().LoseLife();
        }
    }

    private IEnumerator ActivateFiretrap()
    {
        triggered = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(activeDelay);
        sprite.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
