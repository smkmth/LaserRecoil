using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingGuy : MonoBehaviour {

    public GameObject player;
    public Vector3 movementVector;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
	} 
	
	// Update is called once per frame
	void Update () {
        movementVector = player.transform.position - gameObject.transform.position;
        rb.AddForce(movementVector);
	}
}
