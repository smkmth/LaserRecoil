using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : Room {

    public GameObject EmptySpace;

    public Empty(float x, float y)
    {


        EmptySpace = new GameObject();

        EmptySpace.transform.position = new Vector3(x, y, 0);

    }
}
