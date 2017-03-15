using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAI : MonoBehaviour
{

    // Objects 
    public Rigidbody Bullet;
    public GameObject Target;
    private GameObject[] Mobs;
    //List<GameObject> ClosestMobs = new List<GameObject>();

    // Rotation values
    public float RotationSpeed;
    private Quaternion _lookRotation;
    public static Quaternion initRotation;
    private Vector3 _direction;

    private float LastRotation = 1;
    private float deltaRotation;

    // Attack vars

    private float AttackTimer;

    public float AttackSpeed;
    public float Damage;
    public int Range;
    public int MultishotNumber;
    public float SplashRange;

    public int ThisTower;

    public List<GameObject> MultishotProjectiles;
    

    void Start()
    {

        ThisTower = GameObject.Find("UI").GetComponent<CreateTower>().ButtonIndex;

        AttackSpeed = TowerVars.AttackSpeed[ThisTower];
        Damage = TowerVars.Dmg[ThisTower];
        Range = TowerVars.Range[ThisTower];

        //Special Abilities
        SplashRange = TowerVars.SplashRange;
        MultishotNumber = TowerVars.MultishotProjectileNumber;


        AttackTimer = 1 / AttackSpeed;
        initRotation = transform.rotation;
        transform.parent.FindChild("TowerUI").GetComponent<TowerUI>().UpdateTexts();
    }

    void Update()
    {
        // Find all mobs currently in the game
        Mobs = GameObject.FindGameObjectsWithTag("Mob");

        if (Mobs.Length > 0)
        {
            // Find the closest one
            Target = FindClosestMob(Mobs);
            
            // Look in that direction
            if (Target)
            {
                _direction = (Target.transform.position - transform.position);
                _direction.y = 0;
                _lookRotation = Quaternion.LookRotation(_direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initRotation, Time.deltaTime * RotationSpeed);
            }

            // To make sure tower isn't rotating too fast while attacking
            deltaRotation = Mathf.Abs(transform.rotation.y - LastRotation);     
            LastRotation = transform.rotation.y;
        }

    }

    void LateUpdate()
    {
        Attack();
        
    }

    private GameObject FindClosestMob(GameObject[] Mobs)
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        
        foreach (GameObject Mob in Mobs)
        {
            Vector3 diff = (Mob.transform.position - position);
            float curDistance = diff.magnitude;
            if (curDistance < distance && curDistance < Range)
            {
                closest = Mob;
                distance = curDistance;
            }

        }
        return closest;
    }

    private void Attack()
    {
        //if tower has special ability
        bool PerformMultishot;

        if (ThisTower == TowerVars.Mulitshot)
            PerformMultishot = true;
        else
            PerformMultishot = false;

        if (AttackTimer >= 1 / AttackSpeed)
        {
            if (Target && deltaRotation <= 0.01)
            {
                if (!PerformMultishot)
                {
                    Instantiate(Bullet, transform.position + 5.0f * transform.forward, _lookRotation, transform.parent);
                    AttackTimer = 0;
                }
                else
                {
                    for (int i = 0; i < MultishotNumber; i++)
                    {
                        GameObject Projectile;
                        Projectile = (Instantiate(Bullet, transform.position - MultishotNumber*transform.right + MultishotNumber*i*transform.right + 5.0f * transform.forward, _lookRotation, transform.parent) as GameObject);
                        MultishotProjectiles.Add(Projectile);
                        AttackTimer = 0;
                    }

                }
            }
            
        }
        else
        {
            AttackTimer += Time.deltaTime;
        }
    } 

    //private void Multishot()
    //{

    //    int MultiShotTargetNumber = 3;

    //    List<GameObject> AllMobs = new List<GameObject>(Mobs);
       

    //    for (int i = 0; i < MultiShotTargetNumber; i++)
    //    {
    //        ClosestMobs.Add(FindClosestMob(Mobs));
    //        AllMobs.Remove(FindClosestMob(Mobs));

    //    }
    //    ClosestMobs.ToArray();
    //    Debug.Log(ClosestMobs);
    //}
}
