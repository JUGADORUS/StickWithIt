using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer; // 2. —сылка на микшер
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _jumpSounds;
    [SerializeField] private AudioClip[] _musicClips;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;

    void Awake()
    {
        float mVal = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        float musVal = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        float sfxVal = PlayerPrefs.GetFloat("SfxVol", 0.75f);

        SetGroupVolume("MasterVol", mVal);
        SetGroupVolume("MusicVol", musVal);
        SetGroupVolume("SfxVol", sfxVal);

        if (_masterSlider != null) _masterSlider.value = mVal;
        if (_musicSlider != null) _musicSlider.value = musVal;
        if (_sfxSlider != null) _sfxSlider.value = sfxVal;
    }

    void Start()
    {
        PlayRandomTrack();
    }


    void Update()
    {
        if (!_musicSource.isPlaying && _musicClips.Length > 0)
        {
            PlayRandomTrack();
        }
    }

    // ћетод, который мы будем вызывать из —лайдеров в меню
    public void SetGroupVolume(string parameter, float value)
    {
        // ѕереводим 0..1 в децибелы
        _mainMixer.SetFloat(parameter, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(parameter, value);
    }

    private void LoadVolume(string parameter)
    {
        float val = PlayerPrefs.GetFloat(parameter, 0.75f);
        _mainMixer.SetFloat(parameter, Mathf.Log10(val) * 20);
    }

    public void PlayJumpSound()
    {
        Debug.Log("JUMPSOUND");
        _sfxSource.pitch = Random.Range(minPitch, maxPitch);
        _sfxSource.PlayOneShot(_jumpSounds[Random.Range(0,_jumpSounds.Length)]);
    }

    void PlayRandomTrack()
    {
        int index = Random.Range(0, _musicClips.Length);
        _musicSource.clip = _musicClips[index];
        _musicSource.Play();
    }
}
