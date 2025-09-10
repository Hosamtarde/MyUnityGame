using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource loopSource; 

    void Start()
    {
        if (loopSource != null)
        {
            loopSource.loop = true;  
            loopSource.Play();       
        }
        else
        {
            Debug.LogWarning("loopSource ·„ Ì „ —»ÿÂ ›Ì Inspector!");
        }
    }
}
