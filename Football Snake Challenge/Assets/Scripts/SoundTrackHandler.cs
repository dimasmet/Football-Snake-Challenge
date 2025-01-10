using UnityEngine;
using UnityEngine.UI;

public class SoundTrackHandler : MonoBehaviour
{
    public static SoundTrackHandler sound;

    public Button _buttonActive;
    public Sprite _onSoundSprite;
    public Sprite _offSoundSprite;

    public AudioSource audioSourceMain;
    public AudioSource audioSourceSounds;

    public AudioClip soundGameOver;
    public AudioClip soundBonusTake;

    private bool musicOn = true;

    private void Awake()
    {
        if (sound == null) sound = this;
    }

    private void Start()
    {
        _buttonActive.onClick.AddListener(() =>
        {
            GlobalActiveSounds();
        });
    }

    public void PlaySoundGameOver()
    {
        if (musicOn)
        {
            audioSourceSounds.PlayOneShot(soundGameOver);
        }
    }

    public void GlobalActiveSounds()
    {
        musicOn = !musicOn;

        if (musicOn)
        {
            _buttonActive.GetComponent<Image>().sprite = _onSoundSprite;
            audioSourceMain.Play();
        }
        else
        {
            _buttonActive.GetComponent<Image>().sprite = _offSoundSprite;
            audioSourceMain.Stop();
        }
    }

    public void PlaySoundBonus()
    {
        if (musicOn)
        {
            audioSourceSounds.PlayOneShot(soundBonusTake);
        }
    }
}
