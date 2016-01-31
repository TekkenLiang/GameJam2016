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
	
public class MusicCore : MonoBehaviour {

	//music list
	public float[] tempoIntervals;

	[SerializeField]
	public float tempoInterval = 0;
	float tempoIntervalHalf = 0;

	public float maxAllowedDiff = 0.55f;


	public Player player1Player;
	public Player player2Player;



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
	[SerializeField]private float BeatDuration = -1; 	// Change Duration to a Custom Value
	public float BeatMoment = 0.0f;

	public float ClickableDuration = 1.0f;
	[SerializeField] private bool bClickable = false;

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
		//MusicEventManager.StartGame();
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

		musicOn(0);

		//StopCoroutine()
	}

	void OnGameEnd()
	{
		StopCoroutine(playPlayer1);
		StopCoroutine(playPlayer2);

		StartCoroutine(playEndingLoops());

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

	private void StartClickableTime()
	{
		CancelInvoke();

		bClickable = true;

		Invoke("StopClickableTime", ClickableDuration);
	}

	private void StopClickableTime()
	{
		bClickable = false;
	}

	private IEnumerator playEndingLoops()
	{
		float currentTrackTime = -1;
		float endTime = -1;

		if (Player1Data.PlayerAudioSource != null && currentTrackTime < 0)
		{
			currentTrackTime = Player1Data.PlayerAudioSource.time;
			endTime = GetStopTime(Player1Data);
		}
		else if (Player2Data.PlayerAudioSource != null)
		{
			currentTrackTime = Player2Data.PlayerAudioSource.time;
			endTime = GetStopTime(Player2Data);
		}

		if (currentTrackTime > 0 && endTime > 0)
		{
			yield return new WaitForSeconds(endTime - currentTrackTime);
		}

		//Play Last Loop for the entire Track

		if(Player1Data.PlayerAudioSource != null)
		{
			Player1Data.PlayerAudioSource.Play();
			Player1Data.PlayerAudioSource.loop = false;
		}

		if (Player2Data.PlayerAudioSource != null)
		{
			Player2Data.PlayerAudioSource.Play();
			Player2Data.PlayerAudioSource.loop = false;
		}

		if(BackgroundSource != null)
		{
			BackgroundSource.Play();
			BackgroundSource.loop = false;
		}
		
		yield break;
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
		{// Loop will run from Start of Track to End of Track.
			if (playerData.audioID > maxAudioID)
				break;

			playerData.PlayerAudioSource.clip = playerData.TrackScript.GetClip(playerData.audioID);

			AudioClip nextClip = playerData.TrackScript.GetClip(playerData.audioID+1);

			if (playerData.PlayerAudioSource == null)
			{
				yield return null;
				continue;
			}

			float startTime = GetStartTime(playerData);
			float stopTime = GetStopTime(playerData);
			float beatDuration = (BeatDuration < 0) ? (stopTime - startTime)/BeatsPerTrack : BeatDuration;
			bool bContinue = false;
			int numberOfBeats = BeatsPerTrack;

			// Start to Play current Track
//			playerData.PlayerAudioSource.Play();
//			playerData.PlayerAudioSource.time = playerData.StartTime;

			int currentAudioID = playerData.audioID;

			timer = 0;
			tempoInterval = beatDuration;

			for (int i=0; i<numberOfBeats; ++i)
			{
				float actualStartTime = (i*beatDuration) + startTime;
				if (currentAudioID != playerData.audioID || playerData.currentLevel > MaxLevel)
				{
					bContinue = true;
					break;
				}

				playerData.PlayerAudioSource.Play();
				playerData.PlayerAudioSource.time = actualStartTime;

				yield return new WaitForSeconds(beatDuration);

				playerData.PlayerAudioSource.Stop();

			}

			if (bContinue)	// Need to change tracks, so move to next iteration of loop.
			{
				continue;
			}

			//yield return new WaitForSeconds(playerData.StopTime - playerData.StartTime);


			//Stop Playing current Track
			//playerData.PlayerAudioSource.Stop();
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
		//tempoInterval = tempoIntervals[idx];
		//tempoIntervalHalf = tempoInterval / 2.0f;

		//start timer;
		currentStepID = 1;
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
			Debug.Log("Early Click registered");
			regInputToStruct(playerData, tempoInterval - timer, resolvedStepID + 1, gridX, gridY);
			return true;
		}
		else if(timer <= maxAllowedDiff)	//late
		{
			regNum += 1;
			Debug.Log("Late Click registered");
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
				player1Player.MakeMove();
			}
			else
			{
				//player 2 win
				player2Player.MakeMove();
			}
		}
		else
		{
			//both can go
			player1Player.MakeMove();
			player2Player.MakeMove();
		}

		resolvedStepID += 1;
		regNum = 0;
	}
		
	/// Get StartTime of Clip.
	float GetStartTime(PlayerMusicData playerData)
	{
		float result = playerData.TrackScript.GetClipStartTime(playerData.audioID);

		if (result < 0.0f)
		{
			//Default
			return playerData.StartTime;
		}
		else
		{
			//Clip Specific
			return result;
		}
	}

	float GetStopTime(PlayerMusicData playerData)
	{
		float result = playerData.TrackScript.GetClipStopTime(playerData.audioID);

		if (result < 0.0f)
		{
			//Default
			return playerData.StopTime;
		}
		else
		{
			//Clip Specific
			return result;
		}
	}

}
