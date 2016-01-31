using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	//timer
	public float timeLimit = 0;
	[SerializeField]
	float timer = 0;

	public int totalTask = 5;

	//Players
	public GameObject playerPrefab;
	public GameObject player1;
	public Vector2 player1Pos;

	public GameObject player2;
	public Vector2 player2Pos;

	public GameObject grids;

	// Use this for initialization
	void Start () {

		//initialize data
		timer = timeLimit;

		//initialize grid


		//spawn player
		setupPlayers();

		//start music control

	}

	void setupPlayers()
	{
		GameObject player1 = (GameObject) Instantiate(playerPrefab, player1Pos, transform.rotation);
		player1.name = "Player 1";
		PlayerStatus PS1 = player1.GetComponent<PlayerStatus>();
		PS1.setupPlayerStatus(1 ,totalTask);

		GameObject player2 = (GameObject) Instantiate(playerPrefab, player2Pos, transform.rotation);
		player1.name = "Player 2";
		PlayerStatus PS2 = player2.GetComponent<PlayerStatus>();
		PS2.setupPlayerStatus(2 ,totalTask);
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
		
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer < 0)
		{
			endgame();
		}
	}
}
