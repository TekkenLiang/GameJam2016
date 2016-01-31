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

	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			JumpRight();
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			JumpLeft();
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			JumpUp();
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			JumpDown();
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			Fail();
		}
	}

	public void JumpRight()
	{
		myAnimatior.SetBool("jump",true);
		SetRight();
	}

	public void Fail()
	{
		myAnimatior.SetBool("fail",true);
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

}
