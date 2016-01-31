using UnityEngine;
using System.Collections;

public class MusicEventManager : MonoBehaviour 
{

	public delegate void GameEvent();
	public static event GameEvent GameStart;
	public static event GameEvent GameEnd;

	public delegate void PlayerEvent(PlayerID playerID);
	public static event PlayerEvent LevelProgression;

	public static void StartGame()
	{
		if(GameStart != null)
		{
			GameStart();
		}
	}

	public static void EndGame()
	{
		if(GameEnd != null)
		{
			GameEnd();
		}
	}

	public static void MoveToNextLevel(PlayerID playerID)
	{
		if (LevelProgression != null)
		{
			LevelProgression(playerID);
		}
	}

}
