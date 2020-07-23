/*
    SCRIPT TO ADD SOUND AND AUDIO FX TO GAME
    Tutorial: https://www.youtube.com/watch?v=8pFlnyfRfRc by Alexander Zotov
*/

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
        jumpSound = Resources.Load<AudioClip>("Bounce");

        audioSrc = GetComponent<AudioSource>();
    }


    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "CollectCoin":
                audioSrc.PlayOneShot(collectSound);
                break;
            case "Bounce":
                audioSrc.PlayOneShot(jumpSound);
                break;
        }
    }
}
