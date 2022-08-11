using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip openClip;
    [SerializeField] private AudioClip closeClip;
    [SerializeField] private AudioClip selectClip;
    
    [Header("Side Tray")]
    [SerializeField] private AudioClip discoverClip;
    [SerializeField] private AudioClip homeClip;
    [SerializeField] private AudioClip rehomeClip;
    [SerializeField] private AudioClip settingsClip;
    [SerializeField] private AudioClip storageClip;

    [Header("Cat Sounds")]
    [SerializeField] private AudioClip purrClip;
    [SerializeField] private AudioClip catMeowClip;
    [SerializeField] private AudioClip catMeow2Clip;
    [SerializeField] private AudioClip komiMeowClip;

    private static SFXPlayer _instance;
    public static SFXPlayer Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayOpenSound()
    {
        source.PlayOneShot(openClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayCloseSound()
    {
        source.PlayOneShot(closeClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlaySelectSound()
    {
        source.PlayOneShot(selectClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayDiscoverSound()
    {
        source.PlayOneShot(discoverClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayHomeSound()
    {
        source.PlayOneShot(homeClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayRehomeSound()
    {
        source.PlayOneShot(rehomeClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlaySettingsSound()
    {
        source.PlayOneShot(settingsClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayStorageSound()
    {
        source.PlayOneShot(storageClip);
    }
    
    //////////////////////////////////////////
    ///
    ///
    public void PlayPurrSound()
    {
        source.PlayOneShot(purrClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayMeowSound()
    {
        source.PlayOneShot(catMeowClip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayMeow2Sound()
    {
        source.PlayOneShot(catMeow2Clip);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayKomiMeowSound()
    {
        source.PlayOneShot(komiMeowClip);
    }    
    

    //////////////////////////////////////////
    /// Determines which cat sound to play
    ///
    public void PlayCatSound(Cat selectedCat)
    {
        if (selectedCat == null) return;

        if (selectedCat.Relationship == selectedCat.MaxRelationship)
        {
            PlayPurrSound();
            return;
        }

        PlayMeowSound(selectedCat.CatType);
    }

    //////////////////////////////////////////
    ///
    ///
    public void PlayMeowSound(CatType catType)
    {
        if (catType == CatType.komiCat)
        {
            PlayKomiMeowSound();
        }
        else
        {
            int randVal = Random.Range(0, 2);

            if (randVal == 0)
            {
                PlayMeowSound();
            }
            else
            {
                PlayMeow2Sound();
            }
        }
    }
}
