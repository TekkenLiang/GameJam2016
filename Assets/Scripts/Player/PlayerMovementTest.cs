using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerMovementTest : MonoBehaviour {

	[SerializeField] float jumpPrepareTime = 0.5f;
	[SerializeField] float jumpTime = 1.0f;
	void Update()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.DOMoveX(3f, jumpTime).SetRelative(true).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.DOMoveX(-3f, jumpTime).SetRelative(true).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.DOMoveY(3f, jumpTime).SetRelative(true).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.DOMoveY(-3f, jumpTime).SetRelative(true).SetEase(Ease.InOutExpo).SetDelay(jumpPrepareTime);
		}
	}
}
