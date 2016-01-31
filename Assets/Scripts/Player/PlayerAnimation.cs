using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

	[SerializeField] Animator myAnimatior;
	void Awake()
	{
		if (myAnimatior == null )
		{
			myAnimatior = GetComponent<Animator>();
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
