using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerManager : MonoBehaviour
{
    [SerializeField] private GameObject EndOfFirstLevel;
    [SerializeField] private GameObject EndOfSecondLevel;
    [SerializeField] private GameObject EndOfThirdLevel;
    [SerializeField] private GameObject End;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private AudioClip[] _firstLevelMusic;
    [SerializeField] private AudioClip[] _secondLevelMusic;
    [SerializeField] private AudioClip[] _thirdLevelMusic;
    [SerializeField] private AudioClip[] _fourthLevelMusic;

    [SerializeField] private GameObject DirectionalLight;

    public Material[] skyboxMaterials;

    private const string SAVE_FIRST = "FirstLevelBool";
    private const string SAVE_SECOND = "SecondLevelBool";
    private const string SAVE_THIRD = "ThirdLevelBool";

    public bool IsFirstLevel { get; private set; }
    public bool IsSecondLevel { get; private set; }
    public bool IsThirdLevel { get; private set; }

    public void Start()
    {
        IsFirstLevel = PlayerPrefs.GetInt(SAVE_FIRST, 1) == 1; // По умолчанию true
        IsSecondLevel = PlayerPrefs.GetInt(SAVE_SECOND, 0) == 1;
        IsThirdLevel = PlayerPrefs.GetInt(SAVE_THIRD, 0) == 1;
        audioManager.SwitchPlaylistAfterCurrentTrack(_firstLevelMusic);
        RenderSettings.skybox = skyboxMaterials[0];
        DirectionalLight.SetActive(false);

        if (IsThirdLevel)
        {
            audioManager.SwitchPlaylistAfterCurrentTrack(_fourthLevelMusic);
            RenderSettings.skybox = skyboxMaterials[3];
            DirectionalLight.SetActive(true);
        }
        else if (IsSecondLevel)
        {
            audioManager.SwitchPlaylistAfterCurrentTrack(_thirdLevelMusic);
            RenderSettings.skybox = skyboxMaterials[2];
            DirectionalLight.SetActive(false);
        }
        else if (IsFirstLevel)
        {
            audioManager.SwitchPlaylistAfterCurrentTrack(_firstLevelMusic);
            RenderSettings.skybox = skyboxMaterials[0];
            DirectionalLight.SetActive(false);
        }
    }

    private void SaveAll()
    {
        PlayerPrefs.SetInt(SAVE_FIRST, IsFirstLevel ? 1 : 0);
        PlayerPrefs.SetInt(SAVE_SECOND, IsSecondLevel ? 1 : 0);
        PlayerPrefs.SetInt(SAVE_THIRD, IsThirdLevel ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EndOfFirstLevel)
        {
            Debug.Log("First level end");
            audioManager.SwitchPlaylistAfterCurrentTrack(_secondLevelMusic);
            RenderSettings.skybox = skyboxMaterials[1];
            DirectionalLight.SetActive(true);

            SaveAll();
        }
        else if (other.gameObject == EndOfSecondLevel)
        {
            Debug.Log("Second level end");
            audioManager.SwitchPlaylistAfterCurrentTrack(_thirdLevelMusic);
            RenderSettings.skybox = skyboxMaterials[2];
            DirectionalLight.SetActive(false);

            SaveAll();
        }
        else if (other.gameObject == EndOfThirdLevel)
        {
            Debug.Log("Third level end");
            audioManager.SwitchPlaylistAfterCurrentTrack(_fourthLevelMusic);
            RenderSettings.skybox = skyboxMaterials[3];
            DirectionalLight.SetActive(true);

            SaveAll();
        }
        else if (other.gameObject == End)
        {
            Debug.Log("Game end");
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
