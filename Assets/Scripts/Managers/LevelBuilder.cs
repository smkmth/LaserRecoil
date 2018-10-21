using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {

    public GameObject block;
    public GameObject playerObj;
    [SerializeField]
    static public int LevelHeight = 100;
    [SerializeField]
    static public int LevelWidth = 100;
    public Room[,] Level = new Room[LevelHeight,LevelWidth];
    public int[,] EmptySpaceCords = new int[LevelHeight, LevelWidth];
    public int spaceBetweenBlocks;
    public int numOfPlatforms;
    private Vector3 Objpos;
    public int[] Coords;
    public int[] CoordsToSpawn;
 


    // Use this for initialization
    void Start()
    {
        GenerateLevel();
    }


    int[] GenerateCoords()
    {
        int RandX = Random.Range(10, Level.GetLength(0) - spaceBetweenBlocks);
        int RandY = Random.Range(10, Level.GetLength(1) - spaceBetweenBlocks);
        int RoundedX = (RandX / 10) *10;
        int RoundedY = (RandY / 10) * 10;

        Debug.Log(RoundedX);
        Coords = new int[2];
        Coords[0] = RoundedX;
        Coords[1] = RoundedY;
        return Coords;
    }
    

    public void GenerateLevel()
    {
        CoordsToSpawn = GenerateCoords();

        for (int row = 0; row < Level.GetLength(0); row+= spaceBetweenBlocks)
        {
            for (int col = 0; col < Level.GetLength(1); col+= spaceBetweenBlocks)
            {
                //if (numOfPlatforms > 0)
                //{
                    //if (CoordsToSpawn[0] == row && CoordsToSpawn[1] == col)
                    //{
                    //    Level[row, col] = Room.InitFloor(row, col);
                    //    CoordsToSpawn = GenerateCoords();
                    //    numOfPlatforms -= 1;
                    //}
                //}
                //Walls
                if (row == 0 || row == Level.GetLength(0) - spaceBetweenBlocks)
                {
                    Level[row, col] = Room.InitWall(row, col);
                }
                //Floors
                else if (col == 0 || col == Level.GetLength(1) - spaceBetweenBlocks)
                {
                    Level[row, col] = Room.InitFloor(row, col);
                }
                else
                {
                    EmptySpaceCords[row, col] = 1;
                }
            }
        }
        FillEmptySpace();
    }

    public void FillEmptySpace()
    {

        for (int row = 0; row < EmptySpaceCords.GetLength(0); row += spaceBetweenBlocks)
        {
            for (int col = 0; col < EmptySpaceCords.GetLength(1); col += spaceBetweenBlocks)
            {
               
               if(row == 5 && col == 5 && EmptySpaceCords[row, col] == 1)
                {
                    Objpos = new Vector3(row, col, 0);

                    GameObject player = Instantiate(playerObj, Objpos, transform.rotation);
                    EmptySpaceCords[row, col] = 0;

                }
                if (EmptySpaceCords[row, col] == 1)
                {
                    Objpos = new Vector3(row, col, 0);
                    GameObject obj = Instantiate(block, Objpos, transform.rotation);

                }

            }
        }


    }

    //for (int i = 0; i < amountOfBlocks; i++)
    //{
    //    GameObject newBlock = Instantiate(block, new Vector3(Random.Range(0, levelWidth),Random.Range(0, levelHeight),0), transform.rotation);
    //    newBlock.transform.localScale += new Vector3(Random.Range(blockWidthMin, blockWidth), Random.Range(blockHeightMin, blockHeight), 0f);
    //}
}

