using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class StoneAnimation : MonoBehaviour {

	[SerializeField] List<SpriteRenderer> shadowList;
	[SerializeField] float shadowLoopTime = 2f;
	void Awake()
	{
		foreach(SpriteRenderer shadow in shadowList)
		{
			shadow.transform.DOScale(shadow.transform.localScale * 0.5f, shadowLoopTime + Random.Range(-0.3f, 0.3f))
			.SetLoops(9999,LoopType.Yoyo)
			.SetDelay(Random.Range(0, shadowLoopTime));
		}
	}


}
