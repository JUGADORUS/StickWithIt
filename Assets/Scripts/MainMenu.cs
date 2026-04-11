using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _developers;

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void OpenSettings()
    {
        _settings.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ShowDevelopers()
    {
        _mainMenu.SetActive(false);
        _developers.SetActive(true);
    }

    public void CloseDevelopers()
    {
        _developers.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        // Если сохранения нет, можно либо заблокировать кнопку, 
        // либо просто начать новую игру
        if (PlayerPrefs.GetInt("HasSave", 0) == 1)
        {
            SceneManager.LoadScene(1);
            Debug.Log("CONTINUINGSS");
        }
        else
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        // Сбрасываем флаг сохранения
        PlayerPrefs.SetInt("HasSave", 0);
        // Загружаем первый уровень
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Debug.Log("Выход из игры...");

        // Для того, чтобы выйти из запущенной игры (Билд)
        Application.Quit();

        // Для того, чтобы остановить игру прямо в редакторе Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
