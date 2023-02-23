using UnityEngine;

public class dontDestroyOnLoad : MonoBehaviour
{
    public inventory currentInven; 
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }
}