using UnityEngine;
using UnityEngine.UI;

public class SoundTrackHandler : MonoBehaviour
{
    public static SoundTrackHandler sound;

    public Button _buttonActive;

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
            _buttonActive.transform.GetChild(0).GetComponent<Text>().text = "ON";
            audioSourceMain.Play();
        }
        else
        {
            _buttonActive.transform.GetChild(0).GetComponent<Text>().text = "OFF";
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
