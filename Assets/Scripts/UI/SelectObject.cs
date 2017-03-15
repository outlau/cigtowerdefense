using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectObject : MonoBehaviour
{
    Vector3 mousePosition, targetPosition;
    //To Instantiate TargetObject at mouse position
    public Transform MouseTargetObject;
    GameObject[] Towers;
    public GameObject LastTowerClicked;
    GameObject ObjectHit;

    void Start()
    {
    }


    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0) && transform.GetComponent<CreateTower>().PreventFirst) //&& hit.transform.gameObject.tag != "Untagged") 
        {
            Towers = GameObject.FindGameObjectsWithTag("Tower");

            ObjectHit = hit.transform.gameObject;

            if (ObjectHit.tag == "Tower")
            {
                if (LastTowerClicked != ObjectHit)
                {
                    TurnOffAllTowerUI();
                    ObjectHit.GetComponentInChildren<Canvas>().enabled = true;
                    ObjectHit.GetComponentInChildren<LineRenderer>().enabled = true;

                }
                else
                {
                    ObjectHit.GetComponentInChildren<Canvas>().enabled = !ObjectHit.GetComponentInChildren<Canvas>().enabled;
                    ObjectHit.GetComponentInChildren<LineRenderer>().enabled = !ObjectHit.GetComponentInChildren<LineRenderer>().enabled;
                }
                LastTowerClicked = ObjectHit;
                

            }
            else if(ObjectHit.tag == "Mob")
            {
                
                Debug.Log(ObjectHit.GetComponent<MobAI>().Health);

                //Turn off tower UIs
                TurnOffAllTowerUI();
            }
            else
            {

                // Trying to close all Tower UIs when you click on something else 
                // buggy
                /*
                if (Towers.Length != 0) // Equals(0))
                {
                    foreach (GameObject t in Towers)
                        t.transform.GetComponentInChildren<Canvas>().enabled = false;
                }
                */
            }


        }
        else
        {
            //error
        }

    }
    public void TurnOffAllTowerUI()
    {
        Towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject t in Towers)
        {
            t.transform.GetComponentInChildren<Canvas>().enabled = false;
            t.transform.GetComponentInChildren<LineRenderer>().enabled = false;
        }
    }
}

