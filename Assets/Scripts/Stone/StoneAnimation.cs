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

	[SerializeField] Sprite[] normalStone;
	[SerializeField] Sprite[] StepStone;
	[SerializeField] Sprite[] DestinationStone;
	[SerializeField] Sprite[] patternsFirst;
	[SerializeField] Sprite[] patternsSecond;
	[SerializeField] Sprite[] patternBasic;



	void Awake()
	{
		foreach(SpriteRenderer shadow in shadowList)
		{
			shadow.transform.DOScale(shadow.transform.localScale * 0.2f, shadowLoopTime + Random.Range(-0.3f, 0.3f))
			.SetLoops(9999,LoopType.Yoyo)
			.SetDelay(Random.Range(0, shadowLoopTime));
		}
		pattern.color = normalColor;
		SetStoneType(StoneType.Normal);

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

	StoneType myType = StoneType.Normal;

	[SerializeField] float ChangeTypeTime = 1f;
	StoneType toType;
	PlayerType toPlayer;
	int toLevel;
	public void SetStoneType(StoneType type, PlayerType player = PlayerType.None , int level = 0)
	{	
		if (myType != type) // if the type is changed, then do the animation
		{
			toType = type;
			toPlayer = player;
			toLevel = level;
			SwitchTypeAnimation();
		}
		else
			SetStoneTypeImediately(type,player,level);
	}
	public void SetStoneTypeImediately(StoneType type, PlayerType player = PlayerType.None , int level = 0)
	{
		switch(type)
		{
			case StoneType.Normal:
				stone.sprite = normalStone[Random.Range(0, normalStone.Length)];
				pattern.sprite = patternBasic[Random.Range(0, patternBasic.Length)];
				InitShadowStatic(false);
				break;
			case StoneType.Step:
				if ( player == PlayerType.First)
				{	
					stone.sprite = StepStone[0];
					pattern.sprite = patternsFirst[level];
				}

				if ( player == PlayerType.Second)
				{	
					stone.sprite = StepStone[1];
					pattern.sprite = patternsSecond[level];
				}
				InitShadowStatic(true);
				break;
			case StoneType.Destination:
				if ( player == PlayerType.First)
				{	
					stone.sprite = DestinationStone[0];
				}

				if ( player == PlayerType.Second)
				{	
					stone.sprite = DestinationStone[1];
				}
				InitShadowStatic(true);
				break;
			case StoneType.Obstcle:
				break;
			default:
				//do nothing
				break;
		}
	}


	void SetStoneTypeByTo()
	{
		SetStoneTypeImediately(toType, toPlayer, toLevel);
	}
	void SwitchTypeAnimation()
	{
		stone.transform.DOLocalMoveY(16f , ChangeTypeTime / 2f )
		.SetRelative(true)
		.SetEase(Ease.InCirc)
		.OnComplete(SetStoneTypeByTo);


		stone.transform.DOLocalMoveY(-16f , ChangeTypeTime / 2f )
		.SetRelative(true)
		.SetEase(Ease.OutCirc)
		.SetDelay(ChangeTypeTime/2f);


		shadow.transform.DOLocalMoveY(-16f , ChangeTypeTime / 2f )
		.SetRelative(true)
		.SetEase(Ease.InCirc);


		shadow.transform.DOLocalMoveY(16f , ChangeTypeTime / 2f )
		.SetRelative(true)
		.SetEase(Ease.OutCirc)
		.SetDelay(ChangeTypeTime/2f);
	}



	bool myIfBig = false;
	void InitShadowStatic(bool ifBig)
	{
		//if the size is not changed then do nothing 
		if (myIfBig == ifBig)
			return;

		if (ifBig)
		{
			Vector3 shadowScale = shadow.transform.localScale;
			shadowScale.x = 1.4f;
			shadow.transform.localScale = shadowScale;
				
			foreach(SpriteRenderer s in shadowList)
			{
				Vector3 pos = s.transform.localPosition;
				pos.x = (pos.x > 0)?pos.x + 0.4f:pos.x - 0.4f;
				s.transform.localPosition = pos;
			}
		}
		else
		{
			Vector3 shadowScale = shadow.transform.localScale;
			shadowScale.x = 1f;
			shadow.transform.localScale = shadowScale;

			foreach(SpriteRenderer s in shadowList)
			{
				Vector3 pos = s.transform.localPosition;
				pos.x = (pos.x > 0)?pos.x - 0.4f:pos.x + 0.4f;
				s.transform.localPosition = pos;
				
			}
		}


			myIfBig = ifBig;
	}
}
