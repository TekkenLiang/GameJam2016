﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //public int playerID;
    //public int getPlayerID() { return m_playerID; }
    public GridsController grids;
    public PlayerStatus playerStatus;

	// Use this for initialization
	void Start () {
        playerStatus = GetComponent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool MakeMove (int direction)
    {
        
        int destX = playerStatus.getPlayerPositionX();
        int destY = playerStatus.getPlayerPositionY();
        switch (direction)
        {
            case 1: //up direction, y+1
                destY += 1;
                break;
            case 2: //down diretion, y-1
                destY -= 1;
                break;
            case 3: //left direction, x-1
                destX -= 1;
                break;
            case 4: //right direction, x+1
                destX += 1;
                break;
        }

        playerStatus.setPlayerPosition(destX, destY);
        transform.position = grids.getGrid(playerStatus.getPlayerPositionX(), playerStatus.getPlayerPositionY()).getPositionV3();

        // check timing
        // pass the intention to a resolve class
        

        return true;
    }
}
