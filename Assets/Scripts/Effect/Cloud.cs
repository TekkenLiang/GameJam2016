using UnityEngine;
using System.Collections;

using DG.Tweening;

public class Cloud : MonoBehaviour {

	[SerializeField] Sprite[] clouds;
	public void Init(MinMax moveDistance , float cloudMoveTime)
	{
		Sprite s = clouds[Random.Range(0, clouds.Length)];

		SpriteRenderer[] cloudRenders = GetComponentsInChildren<SpriteRenderer>();

		foreach(SpriteRenderer render in cloudRenders)
		{
			render.sprite = s;
		}

		transform.DOMoveX(Random.Range(moveDistance.min, moveDistance.max), cloudMoveTime).OnComplete(SelfDestory);

	}

	void SelfDestory()
	{
		Destroy(this.gameObject);
	}
}
