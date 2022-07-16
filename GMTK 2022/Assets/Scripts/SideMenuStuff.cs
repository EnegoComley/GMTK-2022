using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SideMenuStuff : MonoBehaviour
{

    public Sprite[] faces;

    public GameObject centerButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject underButton;

    public Tilemap diceMap;

    // Start is called before the first frame update
    void Start()
    {
        diceMap = GameObject.FindGameObjectWithTag("DiceMap").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;
        Vector3Int tilePos = Vector3Int.FloorToInt(target);
        TileBase theTile = diceMap.GetTile(tilePos);


        if (theTile != null)
        {
            Die newDie = new Die(theTile, tilePos);


            Debug.Log(newDie.face);
            centerButton.GetComponent<Image>().sprite = faces[newDie.face - 1];
            upButton.GetComponent<Image>().sprite = faces[6 - newDie.bottom];
            downButton.GetComponent<Image>().sprite = faces[newDie.bottom - 1];
            leftButton.GetComponent<Image>().sprite = faces[6 - newDie.right];
            rightButton.GetComponent<Image>().sprite = faces[newDie.right - 1];
            underButton.GetComponent<Image>().sprite = faces[6 - newDie.face];

        }
    }
}
