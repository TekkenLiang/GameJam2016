using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    // public variable defines
    public float height;
    public float width;

	// Use this for initialization
	void Start () {
	
	}
	
    public void setHeight(float newHeight)
    {
        height = newHeight;
        transform.localScale.Set(transform.localScale.x, height, transform.localScale.z);
    }

    public void setWidth(float newWidth)
    {
        width = newWidth;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
