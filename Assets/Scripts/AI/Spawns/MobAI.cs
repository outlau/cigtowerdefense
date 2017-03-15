using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobAI : MonoBehaviour
{
    // Mob AI values
    private float MovementSpeed;
    public float Health;
    private int MobMoney = 50;

    private float[] MovementSpeedList = new float[] { 10f, 10f, 10f, 10f, 3f};
    private float[] HealthList = new float[] {50f, 85f, 100f, 160f, 10000f};


    // Rotation values
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public float RotationSpeed;



    private int target = 1;
    private Transform[] targets;


    void Start()
    {
        targets = GameObject.Find("Points").GetComponentsInChildren<Transform>();
        MovementSpeed = MovementSpeedList[Game.Wave];
        Health = HealthList[Game.Wave];

    }

    void Update()
    {
        // Has it reached a new point/the end?
        if (gameObject.transform.position == targets[target].position)
        {
            if (target == targets.Length - 1)
            {
                // End
                Game.Lives--;
                if(Game.Lives <= 0){
                    //GAME OVER
                    /*
                    Debug.Log("Game Over");
                    //Destroy Mobs
                    
                    Game.GameOver = true;
                    GameObject[] MobsOnMap;
                    GameObject[] TowersOnMap;

                    TowersOnMap = GameObject.FindGameObjectsWithTag("Tower");
                    MobsOnMap = GameObject.FindGameObjectsWithTag("Mob");
                    foreach (GameObject t in TowersOnMap)
                    {
                        Destroy(t);
                    }

                    foreach (GameObject e in MobsOnMap)
                    {
                        Destroy(e);
                    }
                    */
                    
                }
                Destroy(gameObject);
            }
            else
            {
                // Next point
                target++;
            }
        }

        // Move
        float step = MovementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targets[target].position, step);
        // Rotate
        _direction = (targets[target].position - transform.position).normalized;
        if (_direction != Vector3.zero)
        {
            _lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        }
        if (Health <= 0)
        {
            Destroy(gameObject);
            Game.GiveMoney(MobMoney);

        }
    }
}