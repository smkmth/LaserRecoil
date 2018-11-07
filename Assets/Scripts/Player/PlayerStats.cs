using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IKillable
{
    private RespawnManager respawner;

    public int PlayerHealth;

    private int health;
    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
            if (health <= 0)
            {
                Kill();

            }
        }
    }

    // Use this for initialization
    void Start () {

        respawner = GetComponent<RespawnManager>();
        Health = PlayerHealth;

		
	}
	
    public void Kill()
    {
        respawner.ResetLevel();
    }

	// Update is called once per frame
	void Update () {
		
	}
}
