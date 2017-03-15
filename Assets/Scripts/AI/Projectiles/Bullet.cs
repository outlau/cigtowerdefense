using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float BulletVelocity;
    public Rigidbody rb;

    private float dmg;

    private int ParentTowerIndex;
    private Vector3 InitPosition;


    void Start()
    {
        ParentTowerIndex = transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().ThisTower;

        rb = GetComponent<Rigidbody>();       
        rb.velocity = transform.TransformDirection(Vector3.forward * BulletVelocity);

        dmg = transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().Damage;
        InitPosition = transform.position;
    }

    
    void Update()
    {

        float distance = (InitPosition - transform.position).magnitude;

        if (distance >= TowerVars.Range[ParentTowerIndex])
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        GameObject contact = collision.contacts[0].otherCollider.gameObject;
       
        if (ParentTowerIndex == TowerVars.SplashTower)
        {            
            if (contact.GetComponent<MobAI>())
            {
                Debug.Log(contact);
                foreach (GameObject e in GameObject.FindGameObjectsWithTag("Mob"))
                {
                    if((e.transform.position - contact.transform.position).magnitude < transform.parent.FindChild("TowerBody").GetComponent<TowerAI>().SplashRange){
                        e.GetComponent<MobAI>().Health -= dmg;
                    }
                }
            }
        }

        if(contact.tag != "Tower")
        {
            Destroy(gameObject);
        }
        if(contact.GetComponent<MobAI>()){
            contact.GetComponent<MobAI>().Health -= dmg;
        }     
    }
}