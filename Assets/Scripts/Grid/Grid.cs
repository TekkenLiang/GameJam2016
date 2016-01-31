using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    // public variable defines
    public float height;
    public float width;
    protected Vector3 position;

	// Use this for initialization
	void Start () {
        
	}
	
    public void setPosition(Vector3 pos)
    {
        position = pos;
    }

    public Vector2 getPosition()
    {
        return new Vector2(position.x, position.y);
    }

    public Vector3 getPositionV3()
    {
        return position;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
