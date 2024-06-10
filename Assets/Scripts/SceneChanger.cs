using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private const string MainMenuScene = nameof(MainMenuScene);
    private const string FirstScene = nameof(FirstScene);

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }    

    public void LoadFirstScene()
    {
        SceneManager.LoadScene(FirstScene);
    }    
}