using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public int playerID = 0;		// ID = x for player x
	public int completedTask = 0;	//
	public int totalTask = 0;

    protected int posX, posY;
    public void setPlayerPosition(int x, int y) {posX = x; posY = y;}
    public int getPlayerPositionX() { return posX; }
    public int getPlayerPositionY() { return posY; }

    public Player player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setupPlayerStatus(int id, int tasks)
	{
		playerID = id;
		totalTask = tasks;
		completedTask = 0;
		player.InitPlayerAnimation();
	}

	public void completeTask()
	{
		completedTask += 1;

		if(completedTask < totalTask)
		{
			//[update UI]
			//UI progress

			//play music
		}
		else
		{
			//	finish stage!!!!!
			//[update UI]
			//UI progress

			//play music
		}

	}
}
