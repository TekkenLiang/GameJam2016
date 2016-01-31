using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class TempoHinter : MonoBehaviour {

	public float fillValue = 0;
	public Image progressBar;
	public GameObject player;
	public Camera camera;

	public MusicCore musicCore;



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
}
