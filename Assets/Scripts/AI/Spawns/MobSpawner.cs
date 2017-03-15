using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class MobSpawner : MonoBehaviour
{
    private Transform Mobs;
    public GameObject[] Mob;
    public int MobCount;
    private float timer;
    private float spawn_time = 1f;

    private float TimerBetweenWaves = 5f;
    private bool WaitingForNextWave = false;
    private bool StartWave = false;
    GameObject StartWaveButtonObject;



    void Start()
    {
        timer = spawn_time;
        StartWaveButtonObject = GameObject.Find("StartWave").gameObject;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawn_time && !Game.GameOver && StartWave)
        {   
            if (MobCount <= Game.WaveLength[Game.Wave]-1)
            {
                Instantiate(Mob[Game.Wave], transform.position, transform.rotation, transform);
                timer = 0;
                MobCount++;
                
            }
            if (transform.childCount <= 0 && !WaitingForNextWave)
            {
                WaitingForNextWave = true;
                timer = 0;
                Debug.Log("Waiting for next wave");
                StartWaveButtonObject.SetActive(true);
                StartWave = false;
            }
        }
        

        if (timer >= TimerBetweenWaves && WaitingForNextWave)
        {
            Debug.Log("Start Wave");
            MobCount = 0;
            WaitingForNextWave = false;
            Game.Wave++;
            Game.Wave %= 5;
            timer = spawn_time;
            StartWaveButtonObject.SetActive(false);
            StartWave = true;
        }


    }
    public void StartWaveButton()
    {
        StartWave = true;
       
       
            StartWaveButtonObject.SetActive(false);
            timer = TimerBetweenWaves;
        //StartWave = false;
        
    }
}





