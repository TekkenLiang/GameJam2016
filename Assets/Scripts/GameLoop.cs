using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	//timer
	public float timeLimit = 30;
	[SerializeField]
	float gameTimer = 30.0f, beatTimer;

	public int totalTask = 5;

	public GameObject[] tempoHinters;

	//Players
	public GameObject playerPrefab1;
	public GameObject player1;
	public Vector2 player1Pos;

    public GameObject playerPrefab2;
	public GameObject player2;
	public Vector2 player2Pos;

	public GridsController grids;

	public MusicCore musicCore;

    public PlayerStatus[] playersStatus;

	// Use this for initialization
	void Start () {

		//initialize data
		gameTimer = timeLimit;
        beatTimer = musicCore.tempoInterval;

		//initialize grid
        grids.InitializeGrids();
        grids.SetTargetForPlayer(1, 0, 1);
        grids.SetTargetForPlayer(2, 3, 1);

        playersStatus = new PlayerStatus[2];
        //spawn player
        setupPlayers();

		//start music core	(music and rhythm control)
		startMusicCore(0);       
	}

	void setupPlayers()
	{

		player1 = (GameObject) Instantiate(playerPrefab1, grids.getGrid((int)player1Pos.x, (int)player1Pos.y).getPositionV3(), transform.rotation);
		player1.name = "Player 1";
        player1.GetComponent<Player>().gridManager = grids;
        player1.GetComponent<Player>().musicCore = musicCore;
		PlayerStatus PS1 = player1.GetComponent<PlayerStatus>();
		PS1.setupPlayerStatus(1 ,totalTask);
        PS1.setPlayerPosition((int)player1Pos.x, (int)player1Pos.y);
        playersStatus[0] = PS1;

		tempoHinters[0].GetComponent<TempoHinter>().player = player1;

        player2 = (GameObject)Instantiate(playerPrefab2, grids.getGrid((int)player2Pos.x, (int)player2Pos.y).getPositionV3(), transform.rotation);
        player2.name = "Player 2";
        player2.GetComponent<Player>().gridManager = grids;
        player2.GetComponent<Player>().musicCore = musicCore;
		PlayerStatus PS2 = player2.GetComponent<PlayerStatus>();
		PS2.setupPlayerStatus(2 ,totalTask);
        PS2.setPlayerPosition((int)player2Pos.x, (int)player2Pos.y);
        playersStatus[1] = PS2;

		tempoHinters[1].GetComponent<TempoHinter>().player = player2;
	}

	void endgame()
	{
		Debug.Log("GAME END");
		//play sound

		//freeze grid

		//animation

		//fade out

		//play final music

	}

	void startMusicCore(int song)
	{
		musicCore.musicOn(song);
	}


		
	// Update is called once per frame
	void Update () {
        gameTimer -= Time.deltaTime;
        beatTimer -= Time.deltaTime;

        if (beatTimer < 0)
        {
            beatTimer = musicCore.tempoInterval;    // reset beat timer
        }

        if (gameTimer < 0)
		{
			endgame();
            gameTimer = timeLimit;
		}
	}
}
