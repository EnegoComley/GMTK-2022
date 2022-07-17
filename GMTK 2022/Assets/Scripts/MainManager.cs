using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class MainManager : MonoBehaviour
{
    public static MainManager manager;
    // Start is called before the first frame update
    public TileBase[] movableDice;
    public TileBase[] fireDice;
    public TileBase[] unmovableFireDice;
    

    private void Awake()
    {
        if (MainManager.manager != null)
        {
            Destroy(gameObject);
        } else
        {
            MainManager.manager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Die
{
    public TileBase myTile;
    public int face;
    public int bottom;
    public int right;
    public Vector3Int pos;


    public Die(TileBase tile, Vector3Int newPos)
    {
        myTile = tile;
        string name = tile.name;
        face = Int32.Parse(name[0].ToString());
        bottom = Int32.Parse(name[1].ToString());
        right = Int32.Parse(name[2].ToString());
        pos = newPos;
    }

    public string TurnDice(string action)
    {
        // the first incoming variable is the face that the user can see, and the face below that and to the side (facing toward the lower part of the monitor), 
        // and the face to the right on the side (facing the right side)
        // e.g. FaceandBottom = "654" is an allowed combination
        // the second incoming variable is the action, so "TurnUp","TurnRight","TurnDown","TurnLeft"



        int newFace;
        int newBottom;
        int newRight;

        if (action == "TurnUp")
        {
            newFace = 7 - bottom;
            newBottom = face;
            newRight = right;
        }
        else if (action == "TurnLeft")
        {
            newFace = 7 - right;
            newBottom = bottom;
            newRight = face;
        }
        else if (action == "TurnDown")
        {
            newFace = bottom;
            newBottom = 7 - face;
            newRight = right;
        }
        else if (action == "TurnRight")
        {
            newFace = right;
            newBottom = bottom;
            newRight = 7 - face;
        }
        else
        {
            Debug.Log("L");
            return "sorry boss, we failed";
        }

        string newName = newFace.ToString() + newBottom.ToString() + newRight.ToString();


        return newName;
    }
}
