using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour {

    Vector3 Objpos;

    public Obj(float x, float y, GameObject obj)
    {

        Objpos = new Vector3(x,y,0);


        obj = Instantiate(obj, Objpos, transform.rotation);


    }
}
