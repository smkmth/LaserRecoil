using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : Room {

    public GameObject FloorCube;
    public GameObject FloorObj;
    public GameObject Cube1;
    public GameObject Cube2;
    public GameObject Cube3;



    public Floor(float x, float y, int numOfBlocks)
    {
        FloorObj = new GameObject();

        for (int i = 0; i < numOfBlocks; i++)
        {
            Cube1 = GenFloorCube(x + i, y);
            Cube1.transform.SetParent(FloorObj.transform);
        }

        

    }

    private GameObject GenFloorCube(float x, float y)
    {
        FloorCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        FloorCube.transform.position = new Vector3(x, y, 0);
        FloorCube.transform.localScale = new Vector3(1, 1, 5);
        FloorCube.name = " floor " + x + y;
        FloorCube.layer = LayerMask.NameToLayer("Ground");
        return FloorCube;
    }
}
