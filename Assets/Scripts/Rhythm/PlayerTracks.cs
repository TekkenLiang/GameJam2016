using UnityEngine;
using System.Collections;

public class PlayerTracks : MonoBehaviour {

	[SerializeField] 
	private AudioClip[] AudioClipList;

	private int AudioClipListLength = -1;

	public int TrackListLength
	{
		get { return AudioClipListLength; }
	}

	void Awake()
	{
		AudioClipListLength = AudioClipList.Length; 
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	public AudioClip GetClip(int index)
	{
		if (index < 0 || index >= AudioClipListLength)
		{
			Debug.LogError("Invalid index fro Clip Requested");
			return null;
		}

		return AudioClipList[index];
	}

	

}
