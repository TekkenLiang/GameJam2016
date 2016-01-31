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

    private GameLoop gameLoop;

    bool hasMissed;
    float missedTimer;

	// Use this for initialization
	void Start () {
        playerStatus = GetComponent<PlayerStatus>();
        playerStatus.player = this;
        hasMissed = false;
        //missedTimer = musicCore.tempoInterval;
        gameLoop = GameObject.Find("GameLogic").GetComponent<GameLoop>();
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

    public bool MakeMove (int direction)
    {
        
        int destX = playerStatus.getPlayerPositionX();
        int destY = playerStatus.getPlayerPositionY();
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
        Grid temGrid = gridManager.getGrid(playerStatus.getPlayerPositionX(), playerStatus.getPlayerPositionY());

        // Check if player cross each other
        bool isCross = false;
        for (int i = 0; i < 2; ++i)
        {
            PlayerStatus status = gameLoop.playersStatus[i];
            if (status && (status.playerID != playerStatus.playerID) && (status.getPlayerPositionX() == destX && status.getPlayerPositionY() == destY))
            {
                isCross = true;
            }
        }

        // Check timing, pass the intention to a resolve class
        if ((musicCore.regPlayerInput(playerStatus.playerID, destX, destY))
			&& (destX>=0 && destY>=0 && destX<gridManager.gridNumberX && destY<gridManager.gridNumberY) && !isCross)
        {
            Debug.Log("Move succeeded!");
            playerStatus.setPlayerPosition(destX, destY);
            Grid destGrid = gridManager.getGrid(playerStatus.getPlayerPositionX(), playerStatus.getPlayerPositionY());
            if (destGrid.isWalkable)
            {
                // transform.position = destGrid.getPositionV3();
                transform.DOMove(destGrid.getPositionV3(), jumpTime).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
                temGrid.Move();
                destGrid.Glow();
                switch (direction)
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
                return true;
            }
        }
        else
        {   
            temGrid.Move();
            // Press the key in a bad timing, show some feedback effect
            // GetComponent<SpriteRenderer>().color = Color.red;
            hasMissed = true;
            missedTimer = musicCore.tempoInterval / 2.0f;

            //Fail
            playerAnimation.Fail(direction==4,direction==3);
        }        

        return false;
    }
}
