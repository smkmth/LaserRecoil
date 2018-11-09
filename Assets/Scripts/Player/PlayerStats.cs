using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IKillable
{
    public RespawnManager cptRespawner;
    //Mutable in the editor 
    public int iPlayerHealth;

    //actual health value
    private int m_ihealth;
    public int m_iHealth
  {
        get
        {
            return m_ihealth;
        }
        set
        {
            m_ihealth = value;
            if (m_ihealth <= 0)
            {
                Kill();
            }
        }
    }

    // Use this for initialization
    void Start () {

        m_iHealth = iPlayerHealth;
	}
	
    public void Kill()
    {
        cptRespawner.ResetLevel();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
