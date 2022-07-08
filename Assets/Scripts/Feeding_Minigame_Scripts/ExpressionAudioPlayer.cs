using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip happySound;
    [SerializeField] private AudioClip madSound;
    [SerializeField] private AudioSource source;


    //////////////////////////////////////////
    ///
    ///
    public void PlayHappySound()
    {
        source.PlayOneShot(happySound);
    }


    //////////////////////////////////////////
    ///
    ///
    public void PlayMadSound()
    {
        source.PlayOneShot(madSound);
    }
    
}
