using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Room {

    public GameObject WallMesh;


    public Wall(float x, float y)
    {
        
        

        WallMesh = GameObject.CreatePrimitive(PrimitiveType.Cube);

        WallMesh.transform.position = new Vector3(x, y, 0);
        WallMesh.transform.localScale = new Vector3(1, 10, 3);
        WallMesh.name = " wall " + x + y;
        WallMesh.layer = LayerMask.NameToLayer("Wall");

    }
}
