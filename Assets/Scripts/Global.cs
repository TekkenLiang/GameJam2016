using UnityEngine;
using System.Collections;

public class Global  {

}


public enum StoneType
{
	None,
	Normal,
	Step,
	Destination,
	Obstcle,
}

public enum PlayerType
{
	None,
	First,
	Second,
}

[System.SerializableAttribute]
public struct MinMax
{
	public float min;
	public float max;
}