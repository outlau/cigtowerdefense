using UnityEngine;
using System.Collections;

public class TowerVars : MonoBehaviour
{
    public static string[] TowerName = new string[] {"Cube Tower","Sphere Tower","Vapor Tower","Multishot Tower"};
    public static int[] Cost = new int[] { 50, 75, 125, 300 };
    public static int[] UpgradeCost = new int[] { 30, 40, 80,120 };
    public static int[] Dmg = new int[] { 10, 22,15,25 };
    public static float[] AttackSpeed = new float[]{ 1, 0.5f,0.75f, 1.2f };
    public static int[] Range = new int[]{ 10, 30, 50, 80 };

    //Special abilites
    public static int Mulitshot = 3; //Multishot tower index
    public static int MultishotProjectileNumber = 3;
    public static int SplashTower = 2;

    public static float SplashRange = 20; //Range of splash damage

    void Start () {
	
	}
}
