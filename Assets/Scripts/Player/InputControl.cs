﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Player))]
public class InputControl : MonoBehaviour {
    public enum Direction
    {
        UP=1, DOWN=2, LEFT=3, RIGHT=4
    }

    protected Player player;

	// Use this for initialization
	void Start () {
        player = GetComponent<Player>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    switch (player.playerStatus.playerID) {
            case 1:
                if (Input.GetButtonUp("Player1Up"))
                {
                    player.MakeMove((int)Direction.UP);
                }
                else if (Input.GetButtonUp("Player1Down"))
                {
                    player.MakeMove((int)Direction.DOWN);
                }
                else if (Input.GetButtonUp("Player1Left"))
                {
                    player.MakeMove((int)Direction.LEFT);
                }
                else if (Input.GetButtonUp("Player1Right"))
                {
                    player.MakeMove((int)Direction.RIGHT);
                }
                break;


            case 2: 
                if (Input.GetButtonUp("Player2Up"))
                {
                    player.MakeMove((int)Direction.UP);
                }
                else if (Input.GetButtonUp("Player2Down"))
                {
                    player.MakeMove((int)Direction.DOWN);
                }
                else if (Input.GetButtonUp("Player2Left"))
                {
                    player.MakeMove((int)Direction.LEFT);
                }
                else if (Input.GetButtonUp("Player2Right"))
                {
                    player.MakeMove((int)Direction.RIGHT);
                }
                break;

            default: break;
        }

	}
}
