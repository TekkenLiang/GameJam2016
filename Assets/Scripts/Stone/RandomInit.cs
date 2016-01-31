using UnityEngine;
using System.Collections;

public class RandomInit : MonoBehaviour {

	[SerializeField] MinMax positionXRange;
	[SerializeField] MinMax positionYRange;

	// Use this for initialization
	void Awake () {
		Init();
	}

	public void Init()
	{
		Vector3 pos = transform.position;
		pos.x += Random.Range(positionXRange.min, positionXRange.max);
		pos.y += Random.Range(positionYRange.min, positionYRange.max);
		transform.position = pos;
	}
}
