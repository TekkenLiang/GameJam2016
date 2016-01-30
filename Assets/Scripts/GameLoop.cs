using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {

	public float timeLimit = 0;

	[SerializeField]
	float timer = 0;

	public int totalTask = 5;

	public GameObject playerPrefab;
	public GameObject player1;
	public Vector2 player1Pos;

	public GameObject player2;
	public Vector2 player2Pos;


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
		PlayerStatus PS1 = player1.GetComponent<PlayerStatus>();
		PS1.setupPlayerStatus(1 ,totalTask);

		GameObject player2 = (GameObject) Instantiate(playerPrefab, player2Pos, transform.rotation);
		PlayerStatus PS2 = player2.GetComponent<PlayerStatus>();
		PS2.setupPlayerStatus(2 ,totalTask);
	}






	// Update is called once per frame
	void Update () {
		
	}
}
