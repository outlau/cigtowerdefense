using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour
{

    private GameObject PauseMenu;
    public GameObject UI;
    private bool Paused = false;

    void Start ()
    {
        PauseMenu = GameObject.Find("PauseMenu");
        PauseMenu.SetActive(false);
        UI = GameObject.Find("UI");
        
    }
	
	void Update ()
    {
        // Pause game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

	}

    void TogglePause()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        UI.SetActive(!UI.activeSelf);
        if (Paused) Time.timeScale = 1;
        else Time.timeScale = 0;
        Paused = !Paused;
    }

}
