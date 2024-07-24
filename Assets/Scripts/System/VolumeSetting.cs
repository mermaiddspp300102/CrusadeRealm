using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider sfxSlider;
    private void Start()
    {
        if (PlayerPrefs.HasKey("soundVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSoundVolume();
            SetSfxVolume();
        }
    }
    public void SetSoundVolume()
    {
        float volume = soundSlider.value;
        myMixer.SetFloat("sound", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }
    public void SetSfxVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    private void LoadVolume()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        SetSoundVolume();
        SetSfxVolume();
    }
}
