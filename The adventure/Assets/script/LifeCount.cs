using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour, saveAble
{
    public Image[] lives;
    private int livesRemaining = 4;
    Animator animator;
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
        {
            animator.GetBool("death");
            GetComponent<mainCharMovement>().enabled = false;
            FindObjectOfType<mainCharMovement>().Die();
            return;
        }

        //Decrease the value of livesRemaining
        else
        {
            livesRemaining--;
            //Hide one of the life images
            lives[livesRemaining].gameObject.SetActive(false);
        }

    }

    public void addLife()
    {
        if (livesRemaining < 4)
        {
            lives[livesRemaining].gameObject.SetActive(true);
            livesRemaining++;
        }
    }

    public object SaveState()
    {
        return new SaveData()
        {
            lives = this.livesRemaining
        };
    }

    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        livesRemaining = saveData.lives;
    }

    [Serializable]
    private struct SaveData
    {
        public int lives;
    }
}