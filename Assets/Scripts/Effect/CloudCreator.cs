using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CloudCreator : MonoBehaviour {

	[SerializeField] GameObject cloudPrefab;

	[SerializeField] MinMax creatInterval;
	[SerializeField] MinMax moveDistance;
	[SerializeField] float cloudMoveTime = 15f;

	void Awake()
	{
		StartCoroutine(CreateCloud());
	}

	IEnumerator CreateCloud()
	{
		float timer = 0;
		float timerTreshod = Random.Range(creatInterval.min, creatInterval.max);
		AddCloud();
		while(true)
		{

			if (timer > timerTreshod)
			{
				timer = 0 ;
				AddCloud();
				timerTreshod = Random.Range(creatInterval.min, creatInterval.max);
			}
			timer += Time.deltaTime;
			yield return null;
		}

	}

	void AddCloud()
	{
		GameObject cloud = Instantiate( cloudPrefab) as GameObject;
		cloud.transform.parent = this.transform;
		cloud.transform.localPosition = new Vector3(0, Random.Range(-3f, 3f) ,0);
		cloud.transform.localScale = Vector3.one * Random.Range(0.5f, 1f);

		Cloud cloudCom = cloud.GetComponent<Cloud>();
		cloudCom.Init(moveDistance, cloudMoveTime);

	}
}
