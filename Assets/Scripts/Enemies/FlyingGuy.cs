using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGuy : MonoBehaviour, IKillable, IDamageable<int> {


    public int Health;
    float Speed;
    Rigidbody rb;
    GameObject player;
    public GameObject gunAxis;
    public Vector3 lastKnownPosition;
    public float aimSpeed;
    Quaternion lookAtRotation;
    public Transform GunTip;
    public GameObject bullet;
    //public Bullet bulletLogic;
    public float fireRate;
    public float fireTimer;
    private bool fireing;
    public Rigidbody projectile;
  

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreLayerCollision(12, 13, true);

    }
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0)
        {
            Kill();
        }

        lastKnownPosition = player.transform.position;
        lookAtRotation = Quaternion.LookRotation(lastKnownPosition - gunAxis.transform.position,Vector3.forward);

        gunAxis.transform.rotation = Quaternion.RotateTowards(gunAxis.transform.rotation, lookAtRotation, aimSpeed * Time.deltaTime);

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            
            if (bullet != null)
            {
                bullet.GetComponent<Bullet>().ignoreLayers = 12;
                bullet.transform.position = GunTip.transform.position;
                bullet.transform.rotation = GunTip.transform.rotation;
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().Velocity = transform.TransformDirection(new Vector3(0, 0, 1));
            }
            fireTimer = 0;
       
           

        }


        



    }


    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damageTaken)
    {
        Health -= damageTaken;

    }






}
