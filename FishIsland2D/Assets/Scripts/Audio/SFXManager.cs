using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("Audio References")]
    public AudioSource[] sources;
    public AudioSource selectedSFXSource;
    private int nextIndex = 0;

    [Header("Fishing SFX")]
    [SerializeField] private AudioClip splashSound;
    [SerializeField] private AudioClip caughtSound;
    [SerializeField] private AudioClip newFishCaughtSound;
    [SerializeField] private AudioClip failedCatchSound;

    [Header("Inventory Buttons SFX")]
    [SerializeField] private AudioClip soldFishSound;
    [SerializeField] private AudioClip showFishSound;

    [Header("UI SFX")]
    [SerializeField] private AudioClip warningSound;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip slideSound;

    public void PlaySFX(AudioClip soundEffect)
    {
        //Get one of the audio sources for SFX
        selectedSFXSource = sources[nextIndex];

        //Assign clip to audio source
        selectedSFXSource.clip = soundEffect;

        if (!selectedSFXSource.isPlaying)
        {
            //Play audio
            selectedSFXSource.Play();
        }

        //Move on to next audio source
        nextIndex++;

        if (nextIndex >= sources.Length)
        {
            nextIndex = 0;
        }
    }

    public void PlayCaughtSound()
    {
        PlaySFX(caughtSound);
    }

    public void PlaySoldSound()
    {
        PlaySFX(soldFishSound);
    }

    public void PlayShowFishSound()
    {
        PlaySFX(showFishSound);
    }

    public void PlayWarningSound()
    {
        PlaySFX(warningSound);
    }

    public void PlaySplashSound()
    {
        PlaySFX(splashSound);
    }

    public void PlayFailedCatchSound()
    {
        PlaySFX(failedCatchSound);
    }

    public void PlayHoverSound()
    {
        PlaySFX(hoverSound);
    }

    public void PlayClickSound()
    {
        PlaySFX(clickSound);
    }

    public void PlaySlideSound()
    {
        PlaySFX(slideSound);
    }
}