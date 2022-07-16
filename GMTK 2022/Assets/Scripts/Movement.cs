using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            agent.destination = target;
        }
    }

    public string AmongUs(string FaceandBottomandRight, string action)
    {
        // the first incoming variable is the face that the user can see, and the face below that and to the side (facing toward the lower part of the monitor), 
        // and the face to the right on the side (facing the right side)
        // e.g. FaceandBottom = "654" is an allowed combination
        // the second incoming variable is the action, so "TurnUp","TurnRight","TurnDown","TurnLeft"

        int Face = Convert.ToInt32(FaceandBottomandRight[0]);
        int Bottom = Convert.ToInt32(FaceandBottomandRight[1]);
        int Right = Convert.ToInt32(FaceandBottomandRight[2]);

        if (action == "TurnUp")
        {
            int NewFace = 7 - Bottom;
            int NewBottom = Face;
            int NewRight = Right;
        }
        else if (action == "TurnRight")
        {
            int NewFace = 7 - Right;
            int NewBottom = Bottom;
            int NewRight = Face;
        }
        else if (action == "TurnDown")
        {
            int NewFace = Bottom;
            int NewBottom = 7 - Face;
            int NewRight = Right;
        }
        else if (action == "TurnLeft")
        {
            int NewFace = Right;
            int NewBottom = Bottom;
            int NewRight = 7 - Face;
        }
        else
        {
            Console.WriteLine("L");
            return "sorry boss, we failed";
        }

        string NewFaceandBottomandRight = NewFace.ToString() + NewBottom.ToString() + NewRight.ToString();

        Console.WriteLine("W");

        return NewFaceandBottomandRight; 
    }
}
