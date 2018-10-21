using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScanner : MonoBehaviour {

    public CameraControl cameraControl;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemey")
        {
            Debug.Log("Here");
            cameraControl.AddTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemey")
        {
            cameraControl.RemoveTarget(other.transform);
        }
    }
}
