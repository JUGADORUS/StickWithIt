using System;
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
    [SerializeField] public AudioClip[] _musicClips;

    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    public float minPitch = 0.8f;
    public float maxPitch = 1.5f;

    private AudioClip[] _nextMusicClips; // ADDED: следующий массив музыки, который включитс€ после конца текущего трека
    private bool _playFirstTrackFromNewPlaylist; // ADDED: нужно ли после переключени€ включить именно первый трек нового массива

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
        PlayFirstSong();
    }

    void Update()
    {
        if (!_musicSource.isPlaying && Application.isFocused)
        {
            if (_nextMusicClips != null && _nextMusicClips.Length > 0) // ADDED: если запрошен новый плейлист Ч переключаемс€ только после конца текущего трека
            {
                _musicClips = _nextMusicClips; // ADDED: теперь текущий массив музыки становитс€ новым
                _nextMusicClips = null; // ADDED: очищаем ожидание переключени€

                if (_playFirstTrackFromNewPlaylist) // ADDED: если надо начать именно с первого трека нового массива
                {
                    _playFirstTrackFromNewPlaylist = false; // ADDED: сбрасываем флаг после использовани€
                    PlayFirstSong(); // ADDED: запускаем первый трек нового массива
                    return; // ADDED: выходим, чтобы ниже не запустилс€ ещЄ и случайный трек
                }
            }

            if (_musicClips != null && _musicClips.Length > 0) // ADDED: защита от пустого массива
            {
                PlayRandomTrack();
            }
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
        _sfxSource.pitch = UnityEngine.Random.Range(minPitch, maxPitch);
        _sfxSource.PlayOneShot(_jumpSounds[UnityEngine.Random.Range(0,_jumpSounds.Length)]);
    }

    void PlayRandomTrack()
    {
        int index = UnityEngine.Random.Range(0, _musicClips.Length);
        _musicSource.clip = _musicClips[index];
        _musicSource.Play();
    }

    void PlayFirstSong()
    {
        int index = 0;
        _musicSource.clip = _musicClips[index];
        _musicSource.Play();
    }

    public void SwitchPlaylistAfterCurrentTrack(AudioClip[] newPlaylist, bool playFirstTrack = true) // ADDED: метод дл€ отложенного переключени€ плейлиста
    {
        _nextMusicClips = newPlaylist; // ADDED: сохран€ем новый массив, но не включаем его сразу
        _playFirstTrackFromNewPlaylist = playFirstTrack; // ADDED: запоминаем, запускать первый трек или случайный
    }
}
