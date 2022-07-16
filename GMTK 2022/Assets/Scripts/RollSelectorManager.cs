using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;
public class RollSelectorManager : MonoBehaviour
{
    public Sprite[] faces;
    public Die currentDie;
    public GameObject centerButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject player; 
    // Start is called before the first frame update

    void Start()
    {
        Debug.Log(currentDie.face);
        centerButton.GetComponent<Image>().sprite = faces[currentDie.face - 1];
        upButton.GetComponent<Image>().sprite = faces[6 - currentDie.bottom];
        downButton.GetComponent<Image>().sprite = faces[currentDie.bottom - 1];
        leftButton.GetComponent<Image>().sprite = faces[6 - currentDie.right];
        rightButton.GetComponent<Image>().sprite = faces[currentDie.right - 1];
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
            if(x.name == name)
            {
                tile = x;
                break;
            }
        }
        player.GetComponent<Movement>().SetDie(tile, currentDie.pos);
        CloseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
