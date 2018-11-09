using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 Velocity;
    public float speed;
    public int ignoreLayers;
    public List<int> iaHitLayers;


    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(Velocity * speed * Time.deltaTime);


    }
    public void OnTriggerEnter(Collider collision)
    {
        ////Debug.Log(collision.gameObject.name);
        if (iaHitLayers.Contains(collision.gameObject.layer))
        {
            gameObject.SetActive(false);
        }


    }
}
