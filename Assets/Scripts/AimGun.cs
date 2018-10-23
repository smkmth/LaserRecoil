using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimGun : MonoBehaviour {

    public Vector3 lookDirection;
    public float deadZone;
    public Transform gunTip;
    public LineRenderer laser;
    public Rigidbody rb;
    public Rigidbody enemeyrb;
    public Ray ray;
    public RaycastHit hit;
    public float recoil;
    public float recoilStart;
    public float dropoff;
    public float recoilLowestValue;
    public float range;
    public float maxRecoil;
    public int damage;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Mathf.Abs(Input.GetAxis("RightVert")) > deadZone || Mathf.Abs(Input.GetAxis("RightHoriz")) > deadZone)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-Input.GetAxis("RightVert"), Input.GetAxis("RightHoriz")) * 180 / Mathf.PI);

        }

        if (Input.GetAxis("FireLaser") != 0)
        {
            //Debug.Log(Input.GetAxis("FireLaser"));
            laser.enabled = true;
            ray = new Ray(gunTip.transform.position, gunTip.transform.right);
            

            laser.SetPosition(0, gunTip.transform.position);
            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.transform.tag == "Enemey")
                {
                    enemeyrb = hit.transform.gameObject.GetComponent<Rigidbody>();
                    enemeyrb.AddForce(gunTip.transform.right * recoil, ForceMode.Impulse);
                    if (hit.transform.gameObject.GetComponent<IDamageable<int>>() != null)
                    {
                        hit.transform.gameObject.GetComponent<IDamageable<int>>().TakeDamage(damage);
                    }
   
                }
                laser.SetPosition(1, hit.point);
                if (rb.velocity.magnitude < maxRecoil)
                {

                    rb.AddForce(-gunTip.transform.right * recoil, ForceMode.Impulse);
                }
                if (recoil > recoilLowestValue)
                {
                    recoil -= dropoff;
                }
            }
            else
            {

                laser.SetPosition(1, ray.GetPoint(100));
            }




        }
        else if(Input.GetAxis("FireLaser2") != 0)
        {
            laser.enabled = true;
            Ray ray = new Ray(gunTip.transform.position, gunTip.transform.right);
            RaycastHit hit;

            laser.SetPosition(0, gunTip.transform.position);
            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.transform.tag == "Enemey")
                {
                    enemeyrb = hit.transform.gameObject.GetComponent<Rigidbody>();
                    enemeyrb.AddForce(-gunTip.transform.right * recoil, ForceMode.Impulse);
                    if (hit.transform.gameObject.GetComponent<IDamageable<int>>() != null)
                    {
                        hit.transform.gameObject.GetComponent<IDamageable<int>>().TakeDamage(damage);
                    }

                }
                laser.SetPosition(1, hit.point);
                if (rb.velocity.magnitude < maxRecoil)
                {

                    rb.AddForce(gunTip.transform.right * recoil, ForceMode.Impulse);
                }
                if (recoil > recoilLowestValue)
                {
                    recoil -= dropoff;
                }
            }
        }
        else
        {
            recoil = recoilStart;
            laser.enabled = false;
        }

    }

}
