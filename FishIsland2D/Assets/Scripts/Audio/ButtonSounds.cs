using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    public void Click()
    {
        AudioManager.Instance.sfxManager.PlayClickSound();
    }

    public void Hover()
    {
        AudioManager.Instance.sfxManager.PlayHoverSound();
    }

    public void Slide()
    {
        AudioManager.Instance.sfxManager.PlaySlideSound();
    }
}