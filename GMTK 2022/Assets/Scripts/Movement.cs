using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    static public Movement player;
    // Start is called before the first frame update
    public Tilemap diceMap;
    public Tilemap unmovableMap;
    public NavMeshAgent agent;
    public TileBase debugBase;
    public GameObject turnSelectionUIPrefab;
    public GameObject turnSelectionMenu;
    public GameObject NavmeshPrefab;
    public List<Fire> growingFires;
    public List<Fire> dyingFires;
    
    


    void Start()
    {
        
        Instantiate(NavmeshPrefab).GetComponent<NavMeshSurface2d>().BuildNavMesh();
        
        if (player != null)
        {
            Destroy(player);
        }
        player = this;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        diceMap = GameObject.FindGameObjectWithTag("DiceMap").GetComponent<Tilemap>();
        unmovableMap = GameObject.FindGameObjectWithTag("Unmovable").GetComponent<Tilemap>();
        growingFires = new List<Fire>();
        dyingFires = new List<Fire>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseClicked();
        }/*
        if(Input.GetMouseButtonUp(1))
        {
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            if (turnSelectionMenu != null)
            {
                return;
            }
            Vector3Int tilePos = Vector3Int.FloorToInt(target);
            CreateFire(tilePos, 4, true);
        }*/
    }

    void LeftMouseClicked()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        if(turnSelectionMenu != null)
        {
            return;
        }
        Vector3Int tilePos = Vector3Int.FloorToInt(target);
        TileBase theTile = diceMap.GetTile(tilePos);
        if (theTile != null)
        {
            Int32 temp;
            if (Int32.TryParse(theTile.name[0].ToString(), out temp))
            {
                turnSelectionMenu = Instantiate(turnSelectionUIPrefab);
                RollSelectorManager selectorManager = turnSelectionMenu.GetComponent<RollSelectorManager>();
                selectorManager.currentDie = new Die(theTile, tilePos);

                return;
            }
            
        }


        
        agent.destination = target;
    }

    public void SetDie(TileBase tile, Vector3Int pos)
    {
        diceMap.SetTile(pos, tile);
    }

    public void CreateFire(Vector3Int position, int number, bool movable)
    {
        growingFires.Add(new Fire(position, number, movable));
        Invoke("SpreadFire", 0.5f);
    }

    public void SpreadFire()
    {
        Fire tempFire = growingFires[0];
        tempFire.Spread();
        growingFires.RemoveAt(0);
        if(tempFire.movable)
        {
            dyingFires.Add(tempFire);
            Invoke("EndFire", 1.5f);
        }
        
    }

    public void EndFire()
    {
        Fire tempFire = dyingFires[0];
        dyingFires.RemoveAt(0);
        tempFire.RemoveTile();
    }

}

public class Fire
{
    public Vector3Int pos;
    public int number;
    public bool movable;

    public Fire(Vector3Int newPos, int newNumber, bool newMovable)
    {
        pos = newPos;
        number = newNumber;
        Movement.player.diceMap.SetTile(pos, null);
        movable = newMovable;
        if(movable)
        {
            Movement.player.unmovableMap.SetTile(pos, MainManager.manager.fireDice[number - 1]);
        } else
        {
            Movement.player.unmovableMap.SetTile(pos, MainManager.manager.unmovableFireDice[number - 1]);
        }
        Debug.Log("Set");
    }

    public void Spread()
    {
        Vector3Int[] sides = {new Vector3Int(1, 0, 0), new Vector3Int(-1, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, -1, 0)};
        foreach(Vector3Int side in sides)
        {
            bool movable = false;
            Vector3Int tilePos = side + pos;
            TileBase diceTile = Movement.player.diceMap.GetTile(tilePos);
            TileBase uTile = Movement.player.unmovableMap.GetTile(tilePos);
            if (diceTile != null)
            {
                if(diceTile.name[0].ToString() != number.ToString())
                {
                    continue;
                }
                movable = true;
            }
            else if(uTile != null) {
                if (uTile.name[0].ToString() != number.ToString())
                {
                    continue;
                }
            } else
            {
                continue;
            }
            Movement.player.CreateFire(tilePos, number, movable);
        }
    }

    public void RemoveTile()
    {
        Movement.player.unmovableMap.SetTile(pos, null);
        GameObject.FindGameObjectWithTag("Navmesh").GetComponent<NavMeshSurface2d>().BuildNavMesh();
    }
}
