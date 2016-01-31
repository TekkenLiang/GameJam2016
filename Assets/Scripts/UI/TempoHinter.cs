using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TempoHinter : MonoBehaviour {

	public float fillValue = 0;
	public Image progressBar;
	public GameObject player;
	public Camera camera;

	public MusicCore musicCore;

	public float fadeTime = 0.1f;

	// Use this for initialization
	void Start () {
		//musicCore = gameLogic.GetComponent<MusicCore>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		if(player)
		{
			transform.position = camera.WorldToScreenPoint(player.transform.position);

			fillValue = musicCore.timer / musicCore.tempoInterval;

			if(progressBar)
			{
				progressBar.fillAmount = fillValue;
			}
		}
		else
		{
			progressBar.fillAmount = 0;
		}
	}
	/*
	IEnumerator fadeout(bool isGood)
	{
		float timer = fadeTime;
		float originalScale = progressBar.transform.localScale;

		if(!isGood)
		{
			progressBar.color = Color.red;
		}

		while(timer > 0)
		{
			timer -= Time.deltaTime;
			progressBar.transform.localScale = (fadeTime - timer)/fadeTime + 1.0f;
			progressBar.color.g = timer/fadeTime;

			yield return null;
		}
		progressBar.transform.localScale = originalScale;
		progressBar.color = Color.white;
		progressBar.guiText = 1.0f;
	}*/
}
