using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Create variables for the sounds needed during game play
    public static AudioClip collectSound;
    public static AudioClip jumpSound;

    static AudioSource audioSrc;
    
    // Start is called before the first frame update
    void Start()
    {

        collectSound = Resources.Load<AudioClip>("CollectCoin");
        jumpSound = Resources.Load<AudioClip>("Jump");

        audioSrc = GetComponent<AudioSource>();
    }


    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "CollectCoin":
                audioSrc.PlayOneShot(collectSound);
                break;
            case "Jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
        }
    }
}
