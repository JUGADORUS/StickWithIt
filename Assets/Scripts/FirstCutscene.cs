using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FirstCutscene : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        _videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
