using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{

    // Range
    private int Range;
    public float ThetaScale = 0.01f;
    private int Size;
    private LineRenderer LineDrawer;
    private float Theta = 0f;

    private int UpgradeCount = 1;

    private int ParentTowerIndex;

    void Start()
    {
        //ParentTowerIndex = transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().ThisTower;
        ParentTowerIndex = GameObject.Find("UI").GetComponent<CreateTower>().ButtonIndex;
        Range = TowerVars.Range[ParentTowerIndex];
        LineDrawer = transform.parent.FindChild("TowerBody").GetComponent<LineRenderer>();
        DrawRange();
    }

    void Update()
    {
        
    }

    void DrawRange()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.SetVertexCount(Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = Range * Mathf.Cos(Theta);
            float z = Range * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3((x) / 10 , GetComponent<Transform>().localScale.y / -2f +0.05f , (z) / 10 ));
        }
    }

    public void Sell()
    {
        CreateTower.OccupiedSnaps.Remove(CreateTower.OccupiedSnaps.Find(s => s == CreateTower.Snap(gameObject.transform.parent.position)));
        
        int Sum = 0;
            for(int i = 1; i < UpgradeCount; i++)
        {
            Sum += UpgradeCost(i);
            
        }
        Game.GiveMoney((TowerVars.Cost[ParentTowerIndex] + Sum)/2);
        /// 2); // How much should towers sell for? + UPGRADE PRICE
        Destroy(gameObject.transform.parent.gameObject);
        
    }

    public void Upgrade()
    {               
        // Formula for upgrade cost?  exponential 15% per upgrade for testing 
        if (Game.TakeMoney(UpgradeCost(UpgradeCount)))
        {
            

            UpgradeCount++;
       
            transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().Damage = (int)Mathf.Round((transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().Damage * (Mathf.Pow(1.08f, UpgradeCount - 1))));   // Formula for upgrade dmg?  exponential 15% per upgrade for testing 

            transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().AttackSpeed = transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().AttackSpeed *= 1.1f;

            UpdateTexts();
        }
        else
        {
            Debug.Log("Can't Upgrade");

        }


    }
    public void UpdateTexts()
    {
       
        if (UpgradeCount == 9)
        {
            transform.FindChild("Content/Name").GetComponent<Text>().text = "OP TOWER";
        }
        transform.FindChild("Content/Damage").GetComponent<Text>().text = "Dmg: " + transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().Damage.ToString();
        transform.FindChild("Content/Upgrade Cost").GetComponent<Text>().text = "Upgrade Cost: " + UpgradeCost(UpgradeCount).ToString();
        transform.FindChild("Content/Speed").GetComponent<Text>().text = "Speed: " + (Mathf.Round(transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().AttackSpeed*10)/10).ToString();
        transform.FindChild("Content/Range").GetComponent<Text>().text = "Range: " + transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().Range.ToString();

    }
    
    // Upgrade Cost formula
    public int UpgradeCost(int Exponent)
    {
        return TowerVars.UpgradeCost[ParentTowerIndex] + (int)Mathf.Round(Mathf.Pow(2, Exponent));
    }

   
}

