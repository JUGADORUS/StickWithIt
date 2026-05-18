using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndCutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        PlayerPrefs.SetInt("HasSave", 0);
        SceneManager.LoadScene(0);
    }
}
