using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Player : MonoBehaviour {

    public GridsController gridManager;
    public MusicCore musicCore;
    public PlayerStatus playerStatus;
    [SerializeField] GameObject[] playerPrefs;
    PlayerAnimation playerAnimation;
    [SerializeField] float animationScale = 4f;

    [SerializeField] float jumpPrepareTime = 0.25f;
    [SerializeField] float jumpTime = 0.5f;
    
	public GameObject tempoIndicatorPrefab;
	public GameObject tempoIndicator;

    [SerializeField] Vector3 initOffset = new Vector3(0,3f,0);

    bool hasMissed;
    float missedTimer;

	[SerializeField] bool regGood;

	int destX;
	int destY;
	int currentDirection;
	Grid temGrid;

	// Use this for initialization
	void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        playerStatus.player = this;
        hasMissed = false;
        //missedTimer = musicCore.tempoInterval;
	}
	
	// Update is called once per frame
	void Update () {
        if (hasMissed)
        {
            missedTimer -= Time.deltaTime;
            if (missedTimer < 0)
            {
                hasMissed = false;
                // GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
	}

    public void InitPlayerAnimation()
    {
        GameObject player = Instantiate( playerPrefs[playerStatus.playerID-1] ) as GameObject;

        playerAnimation = player.GetComponent<PlayerAnimation>();
        player.transform.parent = transform;
        player.transform.localScale = Vector3.one ;
        player.transform.localPosition = initOffset;

        transform.localScale *= animationScale;
    }



	public void move(int direction)	//register move
	{
		destX = playerStatus.getPlayerPositionX();
		destY = playerStatus.getPlayerPositionY();
		currentDirection = direction;
		switch (direction)
		{   
		// For grid coordination system, the (0,0) point is the top right corner
		case 1: //up direction, y-1
			destY -= 1;
			break;
		case 2: //down diretion, y+1
			destY += 1;
			break;
		case 3: //left direction, x+1
			destX += 1;
			break;
		case 4: //right direction, x-1
			destX -= 1;
			break;
		}
		temGrid = gridManager.getGrid(playerStatus.getPlayerPositionX(), playerStatus.getPlayerPositionY());

		if((destX >= 0 && destY >= 0
			&& destX < gridManager.gridNumberX && destY < gridManager.gridNumberY))
		{
			if (musicCore.regPlayerInput(playerStatus.playerID, destX, destY))
			{
				Debug.Log(playerStatus.playerID + " Move registered!");
				regGood = true;
			}
			else
			{
				Debug.Log(playerStatus.playerID + " Move failed to be registered!");
				temGrid.Move();
				// Press the key in a bad timing, show some feedback effect
				// GetComponent<SpriteRenderer>().color = Color.red;
				hasMissed = true;
				missedTimer = musicCore.tempoInterval / 2.0f;

				regGood = false;
				//Fail
				playerAnimation.Fail(direction==4,direction==3);
			}
		}
		else
		{
			Debug.Log("Move Failed!");
			temGrid.Move();
			// Press the key in a bad timing, show some feedback effect
			// GetComponent<SpriteRenderer>().color = Color.red;
			hasMissed = true;
			missedTimer = musicCore.tempoInterval / 2.0f;

			regGood = false;
			//Fail
			playerAnimation.Fail(direction==4,direction==3);
		}
	}


	public void MakeMove ()	//actual move
	{
		if(regGood)
		{
			playerStatus.setPlayerPosition(destX, destY);	//setting status
			Grid destGrid = gridManager.getGrid(playerStatus.getPlayerPositionX(), playerStatus.getPlayerPositionY());

			if (destGrid.isWalkable)	//obstacle problem?
			{
				// transform.position = destGrid.getPositionV3();
				transform.DOMove(destGrid.getPositionV3(), jumpTime).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
				temGrid.Move();
				destGrid.Glow();
				switch (currentDirection)
				{   
				// For grid coordination system, the (0,0) point is the top right corner
				case 1: //up direction, y-1
					playerAnimation.JumpUp();
					break;
				case 2: //down diretion, y+1
					playerAnimation.JumpDown();
					break;
				case 3: //left direction, x+1
					playerAnimation.JumpLeft();
					break;
				case 4: //right direction, x-1
					playerAnimation.JumpRight();
					break;
				}

				if (destGrid.targetID == playerStatus.playerID)
				{   // Reach the current target, do some update 

				}
			}
			//TODO:	what if not??
		}
		//else do nothing, it has been resolved at the time register fails
	}
}
