using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour {

	[SerializeField] Animator myAnimatior;

	[SerializeField] SpriteRenderer[] patterns;
	void Awake()
	{
		if (myAnimatior == null )
		{
			myAnimatior = GetComponent<Animator>();
		}

	}

	int patternIndex = 0;
	[SerializeField] float patternFadeInTime = 1f;
	void UpgradePattern()
	{
		if (patternIndex < patterns.Length-1)
		{
			patterns[patternIndex].DOFade(1f, patternFadeInTime);
		}
	}

	void SetLeft()
	{
		transform.localScale = new Vector3(-1f,1f,1f);
	}

	void SetRight()
	{
		transform.localScale = new Vector3(1f,1f,1f);
	}

	public void JumpRight()
	{
		myAnimatior.SetBool("jump",true);
		SetRight();
	}

	public void JumpLeft()
	{
		myAnimatior.SetBool("jump",true);
		SetLeft();
	}

	public void JumpUp()
	{
		myAnimatior.SetBool("jump",true);
	}

	public void JumpDown()
	{
		myAnimatior.SetBool("jump",true);
	}


	public void Fail(bool isRight=false,bool isLeft=false)
	{
		if (isRight) SetRight();
		if (isLeft) SetLeft();

		myAnimatior.SetBool("fail",true);
	}

}
