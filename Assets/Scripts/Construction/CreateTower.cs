using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateTower : MonoBehaviour
{
    Vector3 mousePosition, targetPosition;
    //To Instantiate TargetObject at mouse position
    public Transform[] targetObjectList = new Transform[2];
    public Transform[] realTargetObjectList = new Transform[2];
    private Transform targetObject;
    private Transform realTargetObject;

    public bool PreventFirst = true;
    private bool TowerSet = false;
    private GameObject Ghost;
    public static List<Vector3> OccupiedSnaps = new List<Vector3>();
    private bool Occupied;

    public static bool CreatingTower = false;
    private GameObject[] Towers;
    private GameObject Button;
    private ColorBlock CreateColor;
    private ColorBlock CreatingColor;

    private bool FirstTower = true;
    public int ButtonIndex;



    void Start()
    {
        Button = GameObject.Find("TowerCreate");
        CreateColor = Button.GetComponent<Button>().colors;
        CreatingColor = CreateColor;
        CreatingColor.normalColor = new Color(0.235f, 1, 0.627f, 0.5f);
        
       

    }
    

    void Update()
    {

        if (CreatingTower)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            targetObject = targetObjectList[GetComponent<CreateTower>().ButtonIndex];
            realTargetObject = realTargetObjectList[GetComponent<CreateTower>().ButtonIndex];
                    
            // Ghost it
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.transform.name == "Base")
            {
                if (!TowerSet)
                {
                    Ghost = GameObject.Find(Instantiate(targetObject, Snap(hit.point), new Quaternion(0, 0, 0, 0), GameObject.Find("Towers").transform).name);
                    TowerSet = true;
                }
                Ghost.transform.position = Snap(hit.point);
            }
            else
            {
                //error
            }

            // Place it
            if (Input.GetMouseButtonUp(0) && !PreventFirst)
            {
                if (hit.collider)
                {
                    if (hit.collider.gameObject.transform.name == "Base")
                    {
                        Occupied = OccupiedSnaps.Exists(s => s == Snap(hit.point));
                        if (!Occupied)
                        {
                            if (Game.TakeMoney(TowerVars.Cost[ButtonIndex]))
                            {
                                if (FirstTower)
                                    FirstTower = false;
                                
                                //Tower UI
                                GetComponent<SelectObject>().TurnOffAllTowerUI();
                                
                                Transform instance = Instantiate(realTargetObject, Snap(hit.point), new Quaternion(0, 0, 0, 0), GameObject.Find("Towers").transform) as Transform;
                                OccupiedSnaps.Add(Snap(hit.point));
                           
                                //TowerUI
                                GetComponent<SelectObject>().LastTowerClicked = instance.gameObject;


                            }
                        }
                        else
                        {
                            Debug.Log("Occupied!");
                            Debug.Log(Occupied);
                        }
                    }
                    else
                    {
                        Debug.Log("Not a valid spawnpoint!");
                    }
                }
                else
                {
                    Debug.Log("Not inside map!");
                }
            }
            else if (PreventFirst)
            {
                PreventFirst = false;
            }
            if (!Input.GetKey(KeyCode.LeftShift) && !FirstTower)
            {
                CreationMode();
            }
        }
        else if(!PreventFirst)
        {
            PreventFirst = true;
        }



        
    }

    public static Vector3 Snap(Vector3 a)
    {
        Vector3 snappedPoint = new Vector3(Mathf.Floor(a.x/10)*10 +5, a.y, Mathf.Ceil(a.z/10) * 10 - 5);
        return snappedPoint;
    }

    public void CreationMode()
    {
        //Turn off selection UI when tower has been selected
        transform.Find("TowerSelectionCanvas").GetComponent<Canvas>().enabled = false;

        CreatingTower = !CreatingTower;

        if (TowerVars.Cost[ButtonIndex] > Game.Money)
        {

            CreatingTower = false;
            Debug.Log("Not enough money!");
        }


        if (CreatingTower)
        {
            //Turn off range UI's
            transform.GetComponent<SelectObject>().TurnOffAllTowerUI();

            GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
            Button.GetComponentInChildren<Text>().text = "Creating";
            Button.GetComponent<Button>().colors = CreatingColor;
        }
        else
        {
            FirstTower = true;
            TowerSet = false;
            Destroy(Ghost);
            Button.GetComponentInChildren<Text>().text = "Create";
            Button.GetComponent<Button>().colors = CreateColor;
        }
            


    }

    //For Use With "Create" button
    public void OpenSelectTowerMenu()
    {
        transform.Find("TowerSelectionCanvas").GetComponent<Canvas>().enabled = !transform.Find("TowerSelectionCanvas").GetComponent<Canvas>().enabled;
    }

  

    //Which tower is selected
    public void TowerSelected(Button button)
    {
        for (int i = 0; i < button.transform.parent.childCount; i++)
        {
            if (button.transform.parent.GetChild(i).name == button.name)
                ButtonIndex = i;
        }
    }
}