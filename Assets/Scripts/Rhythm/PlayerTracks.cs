using UnityEngine;
using System.Collections;

public class PlayerTracks : MonoBehaviour {

	[SerializeField] 
	private AudioClip[] AudioClipList;

	[System.Serializable]
	public class Track
	{
		[SerializeField] private AudioClip Clip = null;
		[SerializeField] float StartTime = -1;
		[SerializeField] float StopTime = -1;

		[SerializeField] int NumberOfBeats = -1;

		[SerializeField] float BeatMoment;
		[SerializeField] float BeatBuffer;
	}

	[SerializeField]
	private Track[] AudioTrackList;

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
