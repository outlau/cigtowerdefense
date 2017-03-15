using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {

    public static string VersionNumber
    {
        get; set;
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    static Core()
    {
        VersionNumber = "0.2";
    }
}
