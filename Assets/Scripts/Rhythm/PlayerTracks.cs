using UnityEngine;
using System.Collections;

public class PlayerTracks : MonoBehaviour {

	[System.Serializable]
	public class Track
	{
		public AudioClip Clip = null;
		public float StartTime = -1;
		public float StopTime = -1;

		public float BeatMoment;
		public float BeatBuffer;
	}

	[SerializeField]
	private Track[] AudioTrackList;

	public int TrackListLength
	{
		get { return AudioTrackList.Length; }
	}

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	public AudioClip GetClip(int index)
	{
		if (index < 0 || index >= TrackListLength)
		{
			Debug.LogWarning("Invalid index for Clip Requested");
			return null;
		}

		return AudioTrackList[index].Clip;
	}

	public float GetClipStartTime(int index)
	{
		if (index < 0 || index >= TrackListLength)
		{
			Debug.LogWarning("Invalid index for Clip Requested");
			return -1;
		}

		return AudioTrackList[index].StartTime;
	}

	public float GetClipStopTime(int index)
	{
		if (index < 0 || index >= TrackListLength)
		{
			Debug.LogWarning("Invalid index for Clip Requested");
			return -1;
		}

		return AudioTrackList[index].StopTime;
	}

	public float GetBeatMoment(int index)
	{
		if (index < 0 || index >= TrackListLength)
		{
			Debug.LogWarning("Invalid index for Clip Requested");
			return -1;
		}

		return AudioTrackList[index].BeatMoment;
	}

	public float GetBeatBuffer(int index)
	{
		if (index < 0 || index >= TrackListLength)
		{
			Debug.LogWarning("Invalid index for Clip Requested");
			return -1;
		}

		return AudioTrackList[index].BeatBuffer;
	}



}
