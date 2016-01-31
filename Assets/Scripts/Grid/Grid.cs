using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    // public variable defines
    public Sprite targetSprite1;
    public Sprite targetSprite2;
    public Sprite normalSprite;
    public Sprite obstacleSprite;
    public float height;
    public float width;
    public bool isWalkable = true;
    public int targetID = 0; // 0 means not a target, 1 means target for player1 etc.

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

    public bool setToTargetGrid(int playerID)
    {
        if (playerID <= 0) return false;

        targetID = playerID;
        if (targetID == 1)
        {
            GetComponent<SpriteRenderer>().sprite = targetSprite1;
        }
        else if (targetID == 2)
        {
            GetComponent<SpriteRenderer>().sprite = targetSprite2;
        }

        return true;
    }

    public void setToObstacle()
    {
        isWalkable = false;
        targetID = 0;
        GetComponent<SpriteRenderer>().sprite = obstacleSprite;
    }

    public void setToNormal()
    {
        targetID = 0;
        isWalkable = true;
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
