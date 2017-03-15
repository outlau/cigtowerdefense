using UnityEngine;
using System.Collections;

public class MobUI : MonoBehaviour {
    private float Health;
    private float InitHealth;
	// Use this for initialization
	void Start () {
        Health = transform.parent.GetComponent<MobAI>().Health;
        InitHealth = Health;
	}
	
	// Update is called once per frame
	void Update () {
        Health = transform.parent.GetComponent<MobAI>().Health;
        transform.rotation = new Quaternion(0f, 90f, 0f, 0f);
        transform.FindChild("HealthLeft").GetComponent<RectTransform>().offsetMin = new Vector3(-Health/InitHealth + 1, 0f);        //transform.FindChild("HealthLeft").transform.right = new Vector3(-1f,0f);
    }
}
