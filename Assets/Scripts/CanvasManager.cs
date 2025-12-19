using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("buttons")]
    public Button playButton;
    public Button menuButton;
    public Button settingButton;
    public Button backButton;
    public Button QuitButton;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject pauseMenuPanel;

    private void Start()
    {
        if (playButton) playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
        if (menuButton) playButton.onClick.AddListener(() => SceneManager.LoadScene(0));

        if (settingButton) settingButton.onClick.AddListener(() => SetMenus(settingsPanel, mainMenuPanel)); 
        if (backButton) backButton.onClick.AddListener(() => SetMenus(mainMenuPanel, settingsPanel));

        if (QuitButton) QuitButton.onClick.AddListener(QuitGame);
    }
    void SetMenus(GameObject menuToActivate, GameObject menuToDeactivate)
    {
        if (menuToActivate) menuToActivate.SetActive(true);
        if (menuToDeactivate) menuToDeactivate.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}