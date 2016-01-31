using UnityEngine;
using System.Collections;

public enum PlayerID
{
	PLAYER1,
	PLAYER2
}

[System.Serializable]
public class PlayerMusicData
{
	public PlayerID player;
	public int audioID;

	public PlayerTracks TrackScript;
	public AudioSource PlayerAudioSource;
	public int currentLevel;

	public float StartTime;
	public float StopTime;

	public bool ready;
	public int stepID;
	public float timestamp;
	public int gridX;
	public int gridY;

};

//public struct inputReg {
//	public bool ready;
//	public int stepID;
//	public float timestamp;
//	public int gridX;
//	public int gridY;
//}

public class MusicCore : MonoBehaviour {

	//music list
	public float[] tempoIntervals;

	[SerializeField]
	public float tempoInterval = 0;
	float tempoIntervalHalf = 0;

	public float maxAllowedDiff = 0.55f;


//	public inputReg player1Reg;
//	public inputReg player2Reg;



	public float timer = 0;
	[SerializeField]
	int currentStepID;
	[SerializeField]
	int resolvedStepID;

	[SerializeField]
	int regNum;

	public float[] playerRegTime;

	[SerializeField]
	private bool isOn;

	public int MaxLevel = 4;

	public int BeatsPerTrack = 8;
	private float BeatDuration = -1;

	public PlayerMusicData Player1Data; 
	public PlayerMusicData Player2Data;

	public AudioSource BackgroundSource;
	private IEnumerator playPlayer1;
	private IEnumerator playPlayer2;

	void OnEnable()
	{
		MusicEventManager.GameStart += OnGameStart;
		MusicEventManager.GameEnd += OnGameEnd;
		MusicEventManager.LevelProgression += OnLevelProgression;
	}

	void OnDisable()
	{
		MusicEventManager.GameStart -= OnGameStart;
		MusicEventManager.GameEnd -= OnGameEnd;
		MusicEventManager.LevelProgression -= OnLevelProgression;
	}

	void Awake()
	{
		isOn = false;

		playPlayer1 = PlayPlayerMusic(PlayerID.PLAYER1);
		playPlayer2 = PlayPlayerMusic(PlayerID.PLAYER2);
	}



	// Use this for initialization
	void Start () {
		//TODO: Remove this, and call from GameManager
		MusicEventManager.StartGame();
	}

	void OnGameStart()
	{
		if (BackgroundSource != null)
		{
			BackgroundSource.Play();
		}

		StopCoroutine(playPlayer1);
		StopCoroutine(playPlayer2);

		StartCoroutine(playPlayer1);
		StartCoroutine(playPlayer2);

		//StopCoroutine()
	}

	void OnGameEnd()
	{
		StopCoroutine(playPlayer1);
		StopCoroutine(playPlayer2);

		if(Player1Data.PlayerAudioSource != null)
		{
			Player1Data.PlayerAudioSource.Stop();
		}

		if (Player2Data.PlayerAudioSource != null)
		{
			Player2Data.PlayerAudioSource.Stop();
		}

		if(BackgroundSource != null)
		{
			BackgroundSource.Stop();
		}

		//Play End Tracks
	}

	void OnLevelProgression(PlayerID playerID)
	{
		PlayerMusicData playerData;

		if (playerID == PlayerID.PLAYER1)
		{
			playerData = Player1Data;
		}
		else
		{
			playerData = Player2Data;
		}

		//Change Data.
		playerData.audioID += 1;
		playerData.currentLevel += 1;



	}

	private IEnumerator PlayPlayerMusic(PlayerID playerID)
	{
		PlayerMusicData playerData;

		if (playerID == PlayerID.PLAYER1)
		{
			playerData = Player1Data;
		}
		else
		{
			playerData = Player2Data;
		}

		int maxAudioID = playerData.TrackScript.TrackListLength - 1;

		while (playerData.currentLevel <= MaxLevel)
		{
			if (playerData.audioID > maxAudioID)
				break;

			playerData.PlayerAudioSource.clip = playerData.TrackScript.GetClip(playerData.audioID);

			if (playerData.PlayerAudioSource == null)
			{
				yield return null;
				continue;
			}

			playerData.PlayerAudioSource.Play();
			playerData.PlayerAudioSource.time = playerData.StartTime;

			yield return new WaitForSeconds(playerData.StopTime - playerData.StartTime);

			playerData.PlayerAudioSource.Stop();
		}

		MusicEventManager.EndGame();

		yield break;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if(isOn)
		{
			timer += Time.deltaTime;
			if(timer > tempoInterval)	//reset timer
			{
				currentStepID += 1;
				timer = 0;
				if(resolvedStepID > 100)	//just to prevent overflow
				{
					resolvedStepID %= 100;
					currentStepID %= 100;
				}
			}

			if(currentStepID > resolvedStepID + 1)
			{
				if(timer > maxAllowedDiff)	//late
				{
					resolve();
				}
				else if(regNum == 2)	//all registered
				{
					resolve();
				}
			}
		}
	}

	public void musicOn(int idx)
	{
		//setup rhytm
		tempoInterval = tempoIntervals[idx];
		tempoIntervalHalf = tempoInterval / 2.0f;

		//start timer;
		currentStepID = 0;
		resolvedStepID = 0;
		regNum = 0;

		isOn = true;
	}




	public bool regPlayerInput(int playerID, int gridX, int gridY)
	{
		if(playerID == 1)
		{
			if(Player1Data.ready)
			{
				return regPlayerInputCheckTime(Player1Data,gridX,gridY);
			}
			else
			{
				return false;	//not ready
			}
		}
		else
		{
			if(Player2Data.ready)
			{
				return regPlayerInputCheckTime(Player2Data,gridX,gridY);
			}
			else
			{
				return false;	//not ready
			}
		}
	}



	//inputReg reg
	bool regPlayerInputCheckTime(PlayerMusicData playerData, int gridX, int gridY)
	{
		if(tempoInterval - timer <= maxAllowedDiff)	//early
		{
			regNum += 1;
			regInputToStruct(playerData, tempoInterval - timer, resolvedStepID + 1, gridX, gridY);
			return true;
		}
		else if(timer <= maxAllowedDiff)	//late
		{
			regNum += 1;
			regInputToStruct(playerData, tempoInterval - timer, resolvedStepID + 1, gridX, gridY);
			return true;
		}
		else	//bad timing
		{
			regNum += 1;
			regInputToStruct(playerData, tempoInterval, resolvedStepID + 1, gridX, gridY);	//reg to some value always lose in conflict
			return false;
		}
	}

	void regInputToStruct(PlayerMusicData playerData, float timestamp, int stepID, int gridX, int gridY)
	{
		playerData.timestamp = timestamp;
		playerData.stepID = stepID;
		playerData.gridX = gridX;
		playerData.gridY = gridY;
		playerData.ready = false;
	}
		
	void resolve()
	{
		Player1Data.ready = true;
		Player2Data.ready = true;

		//choose winner
		if((Player1Data.gridX == Player2Data.gridX) && (Player1Data.gridY == Player2Data.gridY))
		{
			if(Player1Data.timestamp < Player2Data.timestamp)
			{
				//player 1 win
			}
			else
			{
				//player 2 win
			}
		}
		else
		{
			//both can go
		}

		resolvedStepID += 1;
		regNum = 0;
	}

}
