using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGuy : MonoBehaviour, IKillable, IDamageable<int> {


    public int iHealth;
    float fSpeed;
    Rigidbody rbBody;
    GameObject goPlayer;
    public GameObject goGunAxis;
    public Vector3 v3LastKnownPosition;
    public float fAimSpeed;
    Quaternion qLookAtRotation;
    public Transform tGunTip;
    public float fFireRate;
    public float fFireTimer;
    private bool bFireing;

  

    // Use this for initialization
    void Start () {
        rbBody = GetComponent<Rigidbody>();
        goPlayer = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreLayerCollision(12, 13, true);

    }
	
	// Update is called once per frame
	void Update () {
		if (iHealth <= 0)
        {
            Kill();
        }

        v3LastKnownPosition = goPlayer.transform.position;
        qLookAtRotation = Quaternion.LookRotation(v3LastKnownPosition - goGunAxis.transform.position,Vector3.forward);

        goGunAxis.transform.rotation = Quaternion.RotateTowards(goGunAxis.transform.rotation, qLookAtRotation, fAimSpeed * Time.deltaTime);

        fFireTimer += Time.deltaTime;

        if (fFireTimer >= fFireRate)
        {
            GameObject m_goBullet = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            
            if (m_goBullet != null)
            {
                m_goBullet.GetComponent<Bullet>().iaHitLayers.Add(9);
                m_goBullet.GetComponent<Bullet>().iaHitLayers.Add(11);

                m_goBullet.transform.position = tGunTip.transform.position;
                m_goBullet.transform.rotation = tGunTip.transform.rotation;
                m_goBullet.SetActive(true);
                m_goBullet.GetComponent<Bullet>().Velocity = transform.TransformDirection(new Vector3(0, 0, 1));
            }
            fFireTimer = 0;

        }


        



    }


    public void Kill()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damageTaken)
    {
        iHealth -= damageTaken;

    }






}
