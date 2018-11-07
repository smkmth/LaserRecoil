using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    private Vector3 moveVector;
    private Rigidbody rb;
    public float speed;
    public float maxSpeed;
    public float airSpeed;
    public float maxAirSpeed;
    public float distToGround;
    public bool isGrounded;
    public int jumpCount;
    public float jumpForce;
    public float landDrag;
    public LayerMask whatIsGround;
    public float currentSpeed;
    public float grabDistance;
    public LayerMask whatIsWall;
    public RespawnManager respawnManager;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        
    }
 
    

void Update()
    {
        currentSpeed = rb.velocity.magnitude;
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround, whatIsGround))
        {
            jumpCount = 0;
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }
        if (isGrounded) {

           
            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0);
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(moveVector * speed, ForceMode.Force);
            } 
            

            if (Input.GetAxis("Horizontal") != 0)
            {
                rb.drag = landDrag;
            } else
            {
                rb.drag = landDrag;

            }
            
        } else
        {
            rb.drag = 0f;

            moveVector = new Vector3(Input.GetAxis("Horizontal"), 0);
            if (rb.velocity.magnitude < maxAirSpeed)
            {
                rb.AddForce(moveVector * airSpeed, ForceMode.Force);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("jumps");
            if (jumpCount < 2)
            {
                rb.AddForce(0, 1 * jumpForce, 0, ForceMode.Impulse);
                jumpCount += 1;

            }

        }

        if (Input.GetAxis("Grab") != 0)
        {
            //Debug.Log("2");
            if (Physics.Raycast(transform.position, Vector3.right, grabDistance, whatIsWall))
            {
                rb.useGravity = false;
            }
            else if (Physics.Raycast(transform.position, -Vector3.right, grabDistance, whatIsWall))
            {
                rb.useGravity = false;

            }

        }
        else
        {

            rb.useGravity = true;
        }
    }
}
