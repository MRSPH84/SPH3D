using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Scene Settings")]
    public string sceneToLoad = "SampleScene"; // یا اسم صحنه اصلی‌ات

    // شروع بازی
    public void StartGame()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // خروج از بازی
    public void ExitGame()
    {
        Debug.Log("Exiting Game...");
        Application.Quit();
    }

    // باز کردن تنظیمات
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // برگشت به منو
    public void BackToMainMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
}
