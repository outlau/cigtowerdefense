using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpdateTexts : MonoBehaviour {

    private string EggString = "ANDYSUX";
    private string BreakString = "NAH";
    private int BreakStringProgress = 0;
    private int EggStringProgress = 0;
    public GameObject EasterEggText;
    private float EggTimer;
    private Transform[] Texts;

    private bool Lolling = false;

    void Update()
    {
        transform.FindChild("Money").GetComponent<Text>().text = "Money: " + Game.Money.ToString();


        transform.FindChild("Lives").GetComponentInChildren<Text>().text = "Lives: " + Game.Lives.ToString();


        transform.FindChild("Wave").GetComponentInChildren<Text>().text = "Wave: " + (Game.Wave+1).ToString();
        detectPressedKeyOrButton();
        
    }


    public void detectPressedKeyOrButton()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            //Debug.Log(Input.GetKeyDown(kcode));
            if (Input.GetKeyDown(kcode))
            {
                //Debug.Log(EggString[progress].ToString());


                if (!Lolling)
                {
                    
                    if (kcode.ToString() == EggString[EggStringProgress].ToString() )
                    {
                        EggStringProgress++;
                    }
                        else if(EggStringProgress == 1 && kcode.ToString() == EggString[0].ToString())
                    {
                        EggStringProgress = 1;
                    }
                    else
                    {
                        EggStringProgress = 0;
                    }
                    
                }
                if (Lolling)
                {
                    
                    if (kcode.ToString() == BreakString[BreakStringProgress].ToString())
                    {
                        BreakStringProgress++;
                    }
                    else if (BreakStringProgress == 1 && kcode.ToString() == BreakString[0].ToString())
                    {
                        BreakStringProgress = 1;
                    }
                    else
                    {
                        BreakStringProgress = 0;
                    }
                    
                }


            }
        }
        if (EggStringProgress == EggString.Length)
        {
            Lolling = true;
            GameObject.Find("EasterEgg").GetComponent<Canvas>().enabled = true;

            EggTimer += Time.deltaTime;
            float lolLength = UnityEngine.Random.Range(3f, 4f);
            if (BreakStringProgress != BreakString.Length)
                {
                for (float i = 0; i < lolLength; i++)
                    { 
                    Instantiate(EasterEggText, new Vector3(UnityEngine.Random.Range(-80f, 80f), 0f, UnityEngine.Random.Range(-70f, 70f)), transform.rotation, transform.FindChild("EasterEgg/Panel"));
                
                    }
                }
            else if(BreakStringProgress == BreakString.Length && EggTimer <= 4.5f)
            {
                for (int i = 1; i < (transform.Find("EasterEgg/Panel").childCount); i++)
                {
                    Destroy(transform.Find("EasterEgg/Panel").GetChild(i).gameObject);
                }
                transform.Find("EasterEgg/Panel").GetChild(0).GetComponent<Text>().text = "k";
                transform.Find("EasterEgg/Panel").GetChild(0).GetComponent<Transform>().position = new Vector3(0f,10f);// 0f,0f,0f)
                EggTimer = 4.5f;
            }

            if (EggTimer >= 5)
            {

                //GameObject.Find("EasterEgg").GetComponent<Canvas>().enabled = false;
                for (int i = 0; i < (transform.Find("EasterEgg/Panel").childCount); i++)
                {
                    //Debug.Log(transform.Find("EasterEgg/Panel").GetChild(0).GetComponent<Text>());
                    //(transform.Find("EasterEgg/Panel").GetChild(i).GetComponent<Text>().text = "JK";
                    Destroy(transform.Find("EasterEgg/Panel").GetChild(i).gameObject);
                }
                EggTimer = 0;
                EggStringProgress = 0;
                BreakStringProgress = 0;
                

                GameObject.Find("EasterEgg").GetComponent<Canvas>().enabled = false;
                Lolling = false;

            }
        }
    }
}
