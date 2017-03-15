using UnityEngine;
using System.Collections;

public class VaporShot : Bullet
{


	void Start () {
	
	}
	
	void Update () {
	
	}

    GameObject FindSplashHits()
    {
        GameObject[] Mobs = GameObject.FindGameObjectsWithTag("Mob");
        Vector3 diff;
        foreach (GameObject Mob in Mobs)
        {
            diff = Mob.transform.position - transform.position;
            if (diff.magnitude <= transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().SplashRange)
            {
                Debug.Log("Splash hit!");
            }
        }
        return gameObject;
    }
}
