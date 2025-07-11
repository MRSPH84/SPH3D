using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [Header("UI Components")]
    public Slider volumeSlider;
    public Dropdown qualityDropdown;

    void Start()
    {
        // مقدار اولیه اسلایدر صدا
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.75f);
        AudioListener.volume = volumeSlider.value;

        // مقدار کیفیت گرافیک
        int qualityIndex = PlayerPrefs.GetInt("quality", 2);
        qualityDropdown.value = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void SetGraphicsQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("quality", index);
    }
}
