using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    private int VersionState;
	// Use this for initialization
	void Start () {
        if (int.Parse(Core.VersionNumber[0].ToString()) >= 1)
            transform.Find("Background/Container/Version").GetComponent<Text>().text = "Version: " + Core.VersionNumber;
        else if (int.Parse(Core.VersionNumber[2].ToString()) >= 5)
            transform.Find("Background/Container/Version").GetComponent<Text>().text = "Version: " + Core.VersionNumber + " Beta";
        else
            transform.Find("Background/Container/Version").GetComponent<Text>().text = "Version: " + Core.VersionNumber + " Alpha";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void StartGame()
    {
        SceneManager.LoadScene("TestLevel");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
