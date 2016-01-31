using UnityEngine;
using System.Collections;

public struct inputReg {
	public float timestamp;
	int gridX;
	int gridY;
}


public class MusicCore : MonoBehaviour {

	//music list
	public float[] tempoIntervals;

	[SerializeField]
	public float tempoInterval = 0;
	float tempoIntervalHalf = 0;

	public float maxAllowedDiff = 0.55f;




	[SerializeField]
	float timer = 0;
	[SerializeField]
	int currentStepID;
	[SerializeField]
	int resolvedStepID;

	[SerializeField]
	int regNum;

	public float[] playerRegTime;


	[SerializeField]
	private bool isOn;









	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if(isOn)
		{
			timer += Time.fixedDeltaTime;
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
				//no conflict?
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


		Debug.Log(isOn);
		isOn = true;
		Debug.Log(isOn);
	}

	public bool regPlayerInput(int playerID, int gridX, int gridY)
	{

		if(tempoInterval - timer <= maxAllowedDiff)	//early
		{
			playerRegTime[playerID - 1] = tempoInterval - timer;
			regNum += 1;
			return true;
		}
		else if(timer <= maxAllowedDiff)	//late
		{
			playerRegTime[playerID - 1] = tempoInterval - timer;
			regNum += 1;
			return true;
		}
		else
		{
			return false;	//bad timing
		}
	}

	void resolve()
	{
		resolvedStepID += 1;
		regNum = 0;
	}


}
