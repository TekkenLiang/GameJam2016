using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class StoneAnimation : MonoBehaviour {

	[SerializeField] List<SpriteRenderer> shadowList;
	[SerializeField] float shadowLoopTime = 2f;

	[SerializeField] SpriteRenderer shadow;
	[SerializeField] SpriteRenderer stone;
	[SerializeField] SpriteRenderer pattern;
	[SerializeField] Color normalColor;
	[SerializeField] Color glowColor;
	[SerializeField] float glowTime = 0.3f;
	[SerializeField] float glowDuration = 0.5f;

	[SerializeField] GameObject stoneEffect;

	[SerializeField] AnimationCurve moveCurve;


	void Awake()
	{
		foreach(SpriteRenderer shadow in shadowList)
		{
			shadow.transform.DOScale(shadow.transform.localScale * 0.5f, shadowLoopTime + Random.Range(-0.3f, 0.3f))
			.SetLoops(9999,LoopType.Yoyo)
			.SetDelay(Random.Range(0, shadowLoopTime));
		}
		pattern.color = normalColor;

	}

	void Update()
	{
		if (Input.GetKey(KeyCode.G))
			Glow();
	}


	public void Glow()
	{
		pattern.DOColor(glowColor, glowTime);

		pattern.DOColor(normalColor, glowTime).SetDelay(glowTime + glowDuration);

		GameObject effect = Instantiate(stoneEffect) as GameObject;
		effect.transform.parent = this.transform;
		effect.transform.localPosition = Vector3.zero;

		StartCoroutine(StoneMove(moveCurve,glowTime+glowDuration));

	}

	public void Move()
	{
		StartCoroutine(StoneMove(moveCurve));
	}

	bool ismoving = false;
	IEnumerator StoneMove(AnimationCurve curve, float delay = 0 )
	{
		if (ismoving)
			yield break;
		ismoving = true;
		float timer = 0;
		Vector3 stoneOriPosition = stone.transform.position;
		Vector3 shadowOriPosition = shadow.transform.position;
		while(true)
		{
			if (timer > delay)
			{
				stone.transform.position = stoneOriPosition + new Vector3(0,curve.Evaluate((timer-delay)/(glowTime + glowDuration)),0);
				shadow.transform.position = shadowOriPosition - new Vector3(0,curve.Evaluate((timer-delay)/(glowTime + glowDuration)),0);
			}

			if (timer > glowTime + glowDuration + delay)
				break;

			timer += Time.deltaTime;
			yield return null;
		}
		ismoving = false;
		yield break;

	}

	public void SetStoneType(StoneType type, PlayerType player = PlayerType.None , int level = 0)
	{

	}
}
