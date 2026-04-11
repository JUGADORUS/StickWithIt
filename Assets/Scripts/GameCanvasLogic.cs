using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCanvasLogic : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _developers;
    [SerializeField] private GameObject _player;

    private void Start()
    {
        Time.timeScale = 1f;

        if (PlayerPrefs.GetInt("HasSave", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            Vector3 targetPos = new Vector3(x, y, z);

            if (_player != null)
            {
                Rigidbody rb = _player.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    rb.position = targetPos;
                }

                _player.transform.position = targetPos;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            OpenPause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && _settings.activeSelf)
        {
            CloseSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0 && _settings.activeSelf == false)
        {
            ClosePause();
        }
    }

    public void OpenSettings()
    {
        _pauseMenu.SetActive(false);
        _settings.SetActive(true);
    }

    public void CloseSettings()
    {
        _settings.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void ClosePause()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenPause()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowDevelopers()
    {
        _pauseMenu.SetActive(false);
        _developers.SetActive(true);
    }

    public void CloseDevelopers()
    {
        _developers.SetActive(false);
        _pauseMenu.SetActive(true);
    }

    public void ExitToMainMenu()
    {
        SavePlayerPosition(_player.transform.position);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void SavePlayerPosition(Vector3 position)
    {
        PlayerPrefs.SetFloat("PlayerX", position.x);
        PlayerPrefs.SetFloat("PlayerY", position.y);
        PlayerPrefs.SetFloat("PlayerZ", position.z);
        PlayerPrefs.SetInt("HasSave", 1); // �������, ��� ���������� ����������
        PlayerPrefs.Save();
        Debug.Log("���������");
    }

    public void NewGame()
    {
        // ���������� ���� ����������
        PlayerPrefs.SetInt("HasSave", 0);
        // ��������� ������ �������
        SceneManager.LoadScene(1);
    }
}
