using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    public AudioMixer mixer;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        LoadFullscreenSettings();

        LoadLevel("MasterVol", masterSlider);
        LoadLevel("MusicVol", musicSlider);
        LoadLevel("SfxVol", sfxSlider);

        int savedQuality = PlayerPrefs.GetInt("QualityLevel", 2);
        _qualityDropdown.value = savedQuality;
        SetQuality(savedQuality);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        Debug.Log("Полноэкранный режим: " + isFullscreen);
    }

    private void LoadFullscreenSettings()
    {
        bool savedFull = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        Screen.fullScreen = savedFull;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        Debug.Log("Уровень качества изменен на: " + QualitySettings.names[qualityIndex]);
    }

    private void LoadLevel(string parameter, Slider slider)
    {
        float val = PlayerPrefs.GetFloat(parameter, 0.75f);
        slider.value = val;
        SetGroupVolume(parameter, val);
    }

    // Эти методы привяжи к OnValueChanged каждого слайдера в UI
    public void SetMaster(float v) => SetGroupVolume("MasterVol", v);
    public void SetMusic(float v) => SetGroupVolume("MusicVol", v);
    public void SetSfx(float v) => SetGroupVolume("SfxVol", v);

    private void SetGroupVolume(string name, float value)
    {
        float dbValue;

        if (value > 0.001f) // Если слайдер не в нуле
        {
            dbValue = Mathf.Log10(value) * 20;
        }
        else // Если слайдер в самом начале
        {
            dbValue = -80f; // Принудительный "Mute"
        }

        mixer.SetFloat(name, dbValue);
        PlayerPrefs.SetFloat(name, value);
    }
}
