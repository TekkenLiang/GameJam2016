using UnityEngine;
using System.Collections;

public struct inputReg {
	public bool ready;
	public int stepID;
	public float timestamp;
	public int gridX;
	public int gridY;
}


public class MusicCore : MonoBehaviour {

	//music list
	public float[] tempoIntervals;

	[SerializeField]
	public float tempoInterval = 0;
	float tempoIntervalHalf = 0;

	public float maxAllowedDiff = 0.55f;


	public inputReg player1Reg;
	public inputReg player2Reg;



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
			if(player1Reg.ready)
			{
				return regPlayerInputCheckTime(player1Reg,gridX,gridY);
			}
			else
			{
				return false;	//not ready
			}
		}
		else
		{
			if(player2Reg.ready)
			{
				return regPlayerInputCheckTime(player2Reg,gridX,gridY);
			}
			else
			{
				return false;	//not ready
			}
		}
	}



	//inputReg reg
	bool regPlayerInputCheckTime(inputReg reg, int gridX, int gridY)
	{
		if(tempoInterval - timer <= maxAllowedDiff)	//early
		{
			regNum += 1;
			regInputToStruct(reg, tempoInterval - timer, resolvedStepID + 1, gridX, gridY);
			return true;
		}
		else if(timer <= maxAllowedDiff)	//late
		{
			regNum += 1;
			regInputToStruct(reg, tempoInterval - timer, resolvedStepID + 1, gridX, gridY);
			return true;
		}
		else	//bad timing
		{
			regNum += 1;
			regInputToStruct(reg, tempoInterval, resolvedStepID + 1, gridX, gridY);	//reg to some value always lose in conflict
			return false;
		}
	}

	void regInputToStruct(inputReg reg, float timestamp, int stepID, int gridX, int gridY)
	{
		reg.timestamp = timestamp;
		reg.stepID = stepID;
		reg.gridX = gridX;
		reg.gridY = gridY;
		reg.ready = false;
	}
		
	void resolve()
	{
		player1Reg.ready = true;
		player2Reg.ready = true;

		//choose winner
		if((player1Reg.gridX == player2Reg.gridX) && (player1Reg.gridY == player2Reg.gridY))
		{
			if(player1Reg.timestamp < player2Reg.timestamp)
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
