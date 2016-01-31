using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StoneEffect : MonoBehaviour {

	[SerializeField] SpriteRenderer circle1;
	[SerializeField] SpriteRenderer circle2;
	[SerializeField] SpriteRenderer circleThick;

	[SerializeField] Color glowColor;
	[SerializeField] float glowTime;

	[SerializeField] float circle1Scale;
	[SerializeField] float circle2Scale;
	[SerializeField] float circleThickScale;
	[SerializeField] float circle1Delay;
	[SerializeField] float circle2Delay;
	[SerializeField] float circleThickDelay;


	[SerializeField] bool ifDoOnAwake;
	void Awake()
	{
		if (ifDoOnAwake)
			Do();
	}

	public void Do()
	{
		circle1.transform.DOScale(Vector3.one * circle1Scale , glowTime ).SetDelay(circle1Delay);
		circle2.transform.DOScale(Vector3.one * circle2Scale , glowTime ).SetDelay(circle2Delay);
		circleThick.transform.DOScale(Vector3.one * circleThickScale , glowTime ).SetDelay(circleThickDelay);

		circle1.DOFade(0.66f, glowTime/2f).SetDelay(circle1Delay);
		circle1.DOFade(0f, glowTime/2f).SetDelay(glowTime/2f);


		circle2.DOFade(0.66f, glowTime/2f).SetDelay(circle2Delay);
		circle2.DOFade(0f, glowTime/2f).SetDelay(glowTime/2f);


		circleThick.DOFade(0.66f, glowTime/2f).SetDelay(circleThickDelay);
		circleThick.DOFade(0f, glowTime/2f).SetDelay(glowTime/2f);

	}
}
