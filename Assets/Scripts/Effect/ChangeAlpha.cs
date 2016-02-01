using UnityEngine;
using System.Collections;

using DG.Tweening;

public class ChangeAlpha : MonoBehaviour {

	[SerializeField] float fadeAlpha = 0.5f;
	[SerializeField] float duration = 2f;
	[SerializeField] bool ifLoop = true;
	[SerializeField] float delay;
	void Awake()
	{
		SpriteRenderer render = GetComponent<SpriteRenderer>();

		if (ifLoop)
			render.DOFade(fadeAlpha, duration).SetLoops(9999,LoopType.Yoyo).SetDelay(delay);
		else
			render.DOFade(fadeAlpha, duration).SetDelay(delay);
		
	}
}
