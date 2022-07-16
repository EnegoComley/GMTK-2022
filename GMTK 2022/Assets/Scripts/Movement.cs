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
    Tilemap diceMap;
    public NavMeshAgent agent;
    public TileBase debugBase;
    public GameObject turnSelectionUIPrefab;
    public GameObject turnSelectionMenu;
    
    void Start()
    {
        if (player != null)
        {
            Destroy(player);
        }
        player = this;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        diceMap = GameObject.FindGameObjectWithTag("DiceMap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseClicked();
        }
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
            turnSelectionMenu = Instantiate(turnSelectionUIPrefab);
            RollSelectorManager selectorManager = turnSelectionMenu.GetComponent<RollSelectorManager>();
            selectorManager.currentDie = new Die(theTile, tilePos);

            return;
        }


        
        agent.destination = target;
    }

    public void SetDie(TileBase tile, Vector3Int pos)
    {
        diceMap.SetTile(pos, tile);
    }

}
