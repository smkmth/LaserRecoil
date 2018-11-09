using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    private Vector3     v3MoveVector;
    private Rigidbody   rbPlayerBody;
    public float        fGroundSpeed;
    public float        fMaxGroundSpeed;
    public float        fAirSpeed;
    public float        fMaxAirSpeed;
    public float        fDistToGround;
    public int          iMaxNumJumps;
    private int         m_iJumpCounter;
    public float        fJumpForce;
    public float        fLandDrag;
    public float        fAirDrag;
    public LayerMask    lmWhatIsGround;
    public float        fCurrentSpeed;

    public PlayerStates EPlayerState;

    public enum PlayerStates
    {
        Grounded,
        InAir,
        FiringLaser
    };


    private void Start()
    {
        rbPlayerBody = GetComponent<Rigidbody>(); 
    }
 
    

    void Update()
    {
        fCurrentSpeed = rbPlayerBody.velocity.magnitude;

        if (Physics.Raycast(transform.position, -Vector3.up, fDistToGround, lmWhatIsGround))
        {
            m_iJumpCounter = 0;
            rbPlayerBody.drag = fLandDrag;
            EPlayerState = PlayerStates.Grounded;
        } else
        {
            rbPlayerBody.drag = fAirDrag;
            EPlayerState = PlayerStates.InAir;
            
        }
        switch (EPlayerState)
        {
            case PlayerStates.Grounded:
                v3MoveVector = new Vector3(Input.GetAxis("Horizontal"), 0);
                if (rbPlayerBody.velocity.magnitude < fMaxGroundSpeed)
                {
                    rbPlayerBody.AddForce(v3MoveVector * fGroundSpeed, ForceMode.Force);
                }
                break;
            case PlayerStates.InAir:
                v3MoveVector = new Vector3(Input.GetAxis("Horizontal"), 0);
                if (rbPlayerBody.velocity.magnitude < fMaxAirSpeed)
                {
                    rbPlayerBody.AddForce(v3MoveVector * fAirSpeed, ForceMode.Force);
                }
                break;
            case PlayerStates.FiringLaser:

                
                break;
            default:
                Debug.Assert(true, "Default case not allowed");

                break;


        }
        if (Input.GetButtonDown("Jump"))
        {
            if (m_iJumpCounter < iMaxNumJumps)
            {
                Debug.Log("jumps");
                rbPlayerBody.AddForce(0, 1 * fJumpForce, 0, ForceMode.Impulse);
                m_iJumpCounter += 1;
            }
        }
    }
}
