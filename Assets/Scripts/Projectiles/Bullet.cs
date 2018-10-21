using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector3 Velocity;
    public float speed;
	
    

	// Update is called once per frame
	void FixedUpdate () {

        transform.Translate(Velocity * speed * Time.deltaTime);
        

	}
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer < 12)
        {
            if (collision.transform.gameObject.tag == "Player")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
       
    }
}
