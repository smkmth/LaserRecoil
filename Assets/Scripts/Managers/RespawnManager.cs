using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RespawnManager : MonoBehaviour {


    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        
		
	}

    public void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
