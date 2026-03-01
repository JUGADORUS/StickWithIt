using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _jumpSounds;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip[] _musicClips;

    public float minPitch = 0.8f;         // Низкий тон
    public float maxPitch = 1.5f;
    public float sfxVolume = 0.7f;
    public float musicVolume = 0.7f;

    void Start()
    {
        _sfxSource.volume = sfxVolume;
        _musicSource.volume = musicVolume;
        PlayRandomTrack();
    }

    void Update()
    {
        if (!_musicSource.isPlaying && _musicClips.Length > 0)
        {
            PlayRandomTrack();
        }
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
