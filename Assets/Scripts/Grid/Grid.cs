using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    // public variable defines
    public float height;
    public float width;
    private Vector2 position;

	// Use this for initialization
	void Start () {
        
	}
	
    void setPosition(Vector2 pos)
    {
        position = pos;
    }

    Vector2 getPosition()
    {
        return position;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
