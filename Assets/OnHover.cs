using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnHover : MonoBehaviour {
    private Color EnoughMoney;

	void Start () {
        for (int i = 0; i < transform.parent.childCount; i++) 
        {
            transform.parent.GetChild(i).FindChild("HoverUI/Content/Name").GetComponent<Text>().text = TowerVars.TowerName[i];

            transform.parent.GetChild(i).FindChild("HoverUI/Content/Damage").GetComponent<Text>().text = "Damage: " + TowerVars.Dmg[i].ToString();

            transform.parent.GetChild(i).FindChild("HoverUI/Content/Speed").GetComponent<Text>().text = "Speed: " + TowerVars.AttackSpeed[i].ToString();

            transform.parent.GetChild(i).FindChild("HoverUI/Content/Range").GetComponent<Text>().text = "Range: " + TowerVars.Range[i].ToString();

            transform.parent.GetChild(i).FindChild("HoverUI/Content/Price").GetComponent<Text>().text = "Price: " + TowerVars.Cost[i].ToString();

        }
        EnoughMoney = transform.FindChild("HoverUI/Content/Price").GetComponent<Text>().color;

    }
    void Update()
    {
        if (TowerVars.Cost[transform.GetSiblingIndex()] > Game.Money)
        {
            transform.FindChild("HoverUI/Content/Price").GetComponent<Text>().color = new Color(178f / 256, 22f / 256, 22f / 256);
        }
        else
        {
            transform.FindChild("HoverUI/Content/Price").GetComponent<Text>().color = EnoughMoney;
        }

    }
   



    public void TowerSelectionOnHover()
    {
        
        transform.FindChild("HoverUI").GetComponent<Canvas>().enabled = !transform.FindChild("HoverUI").GetComponent<Canvas>().enabled;

    }
}
