using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Room {

    int numberOfPlatforms;
    int numberOfExits;

    int Xsize;
    int Ysize;


    public static Room InitWall(int row, int col)
    {
        float x = (float)row;
        float y = (float)col;

        return new Wall(x, y);
    }

    public static Room InitEmpty(int row, int col)
    {
        float x = (float)row;
        float y = (float)col;

        return new Empty(x, y);

    }


    public static Room InitFloor(int row, int col)
    {
        float x = (float)row;
        float y = (float)col;

        return new Floor(x, y, 10);

    }
}
