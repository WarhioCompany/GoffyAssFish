using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // ~ Buttons
    [Header("Buttons")]
    [SerializeField] private Button StartButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button CreditsButton;
    [SerializeField] private Button ExitButton;

    // ~ Scene Loader

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    // ~ For Exit Button (UnityEvent)

    public void ExitGame() {
        //try
        //{
        //    EditorApplication.ExitPlaymode();
        //}
        //catch { }

        Application.Quit();
    }
}
