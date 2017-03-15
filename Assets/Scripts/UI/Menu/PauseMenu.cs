using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    void Start()
    {

    }
    
    void Update()
    {

    }

    public void ResumeGame()
    {
        GameObject.Find("GameController").GetComponent<UserInput>().UI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SettingsMenu()
    {
        Debug.Log("Not made yet..");
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
