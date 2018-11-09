using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnTouch : MonoBehaviour {


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerStats m_cmpPlayerStats = collision.gameObject.GetComponent<PlayerStats>();
            m_cmpPlayerStats.Kill();

        }
    }
}
