using UnityEngine;
using System.Collections;

using DG.Tweening;

public class ChangeAlpha : MonoBehaviour {

	[SerializeField] float fadeAlpha = 0.5f;
	[SerializeField] float duration = 2f;
	void Awake()
	{
		SpriteRenderer render = GetComponent<SpriteRenderer>();

		render.DOFade(fadeAlpha, duration).SetLoops(9999,LoopType.Yoyo);
		
	}
}
