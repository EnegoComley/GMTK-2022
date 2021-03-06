using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;
using UnityEngine.AI;
public class RollSelectorManager : MonoBehaviour
{
    public Sprite[] faces;
    public Die currentDie;
    public GameObject centerButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;
    // Start is called before the first frame update

    void Start()
    {
        centerButton.GetComponent<Image>().sprite = faces[currentDie.face - 1];
        upButton.GetComponent<Image>().sprite = faces[6 - currentDie.bottom];
        downButton.GetComponent<Image>().sprite = faces[currentDie.bottom - 1];
        leftButton.GetComponent<Image>().sprite = faces[6 - currentDie.right];
        rightButton.GetComponent<Image>().sprite = faces[currentDie.right - 1];

        NavMeshAgent agent = Movement.player.agent;
        Vector3 upPos = Vector3Int.FloorToInt(currentDie.pos) + new Vector3(0.5f, 1.5f, 0);
        Vector3 downPos = Vector3Int.FloorToInt(currentDie.pos) + new Vector3(0.5f, -0.5f, 0);
        Vector3 leftPos = Vector3Int.FloorToInt(currentDie.pos) + new Vector3(-0.5f, 0.5f, 0);
        Vector3 rightPos = Vector3Int.FloorToInt(currentDie.pos) + new Vector3(1.5f, 0.5f, 0);
        NavMeshPath upPath = new NavMeshPath();
        agent.CalculatePath(upPos, upPath);
        NavMeshPath downPath = new NavMeshPath();
        agent.CalculatePath(downPos, downPath);
        NavMeshPath leftPath = new NavMeshPath();
        agent.CalculatePath(leftPos, leftPath);
        NavMeshPath rightPath = new NavMeshPath();
        agent.CalculatePath(rightPos, rightPath);
        if((downPath.status != NavMeshPathStatus.PathComplete || (Movement.player.diceMap.GetTile(Vector3Int.FloorToInt(downPos)) != null || Movement.player.unmovableMap.GetTile(Vector3Int.FloorToInt(downPos)) != null))  && (upPath.status != NavMeshPathStatus.PathComplete || (Movement.player.diceMap.GetTile(Vector3Int.FloorToInt(upPos)) != null || Movement.player.unmovableMap.GetTile(Vector3Int.FloorToInt(upPos)) != null)))
        {
            upButton.GetComponent<Button>().interactable = false;
            downButton.GetComponent<Button>().interactable = false;
        }
        if ((leftPath.status != NavMeshPathStatus.PathComplete || (Movement.player.diceMap.GetTile(Vector3Int.FloorToInt(leftPos)) != null || Movement.player.unmovableMap.GetTile(Vector3Int.FloorToInt(leftPos)) != null)) && (rightPath.status != NavMeshPathStatus.PathComplete || (Movement.player.diceMap.GetTile(Vector3Int.FloorToInt(rightPos)) != null || Movement.player.unmovableMap.GetTile(Vector3Int.FloorToInt(rightPos)) != null)))
        {
            leftButton.GetComponent<Button>().interactable = false;
            rightButton.GetComponent<Button>().interactable = false;
        }
        
       
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }

    public void Turn(string direction)
    {
        name = currentDie.TurnDice(direction);
        TileBase tile = MainManager.manager.movableDice[0];
        foreach(TileBase x in MainManager.manager.movableDice)
        {

            if (x.name == name)
            {
                tile = x;
                break;
            }
        }
        
        List<Vector3Int> sides = new List<Vector3Int>();
        if (direction == "TurnUp" || direction == "TurnDown")
        {
            sides.Add(new Vector3Int(0, 1, 0) + currentDie.pos);
            sides.Add(new Vector3Int(0, -1, 0) + currentDie.pos);
        } else
        {
            sides.Add(new Vector3Int(1, 0, 0) + currentDie.pos);
            sides.Add(new Vector3Int(-1, 0, 0) + currentDie.pos);
        }
        NavMeshPath pathA = new NavMeshPath();
        Movement.player.agent.CalculatePath(sides[0] + new Vector3(0.5f, 0.5f, 0), pathA);
        NavMeshPath pathB = new NavMeshPath();
        Movement.player.agent.CalculatePath(sides[1] + new Vector3(0.5f, 0.5f, 0), pathB);
        Movement.player.agent.SetPath(pathA);
        float distanceA = Movement.player.agent.remainingDistance;
        Movement.player.agent.SetPath(pathB);
        float distanceB = Movement.player.agent.remainingDistance;

        if (distanceA < distanceB)
        {
            
            if (pathA.status == NavMeshPathStatus.PathComplete && (Movement.player.diceMap.GetTile(sides[0]) == null && Movement.player.unmovableMap.GetTile(sides[0]) == null))
            {
                Movement.player.agent.SetPath(pathA);
                

            }


        }
        if (pathB.status != NavMeshPathStatus.PathComplete || (Movement.player.diceMap.GetTile(sides[1]) != null || Movement.player.unmovableMap.GetTile(sides[1]) != null))
        {
            Movement.player.agent.SetPath(pathA);
        }
        Movement.player.TurnDie(tile, currentDie.pos);
        

        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
