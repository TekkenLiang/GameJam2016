using UnityEngine;
using System.Collections;

public class TimerProgressBar : MonoBehaviour {
    private float timeLimit;
    private float passedTime;
    private float progress;
    private GameObject gameLogicObj;
    private GameLoop gameLoop;

    public Vector2 fullBartartPos;
    public Vector2 emptyBarStartPos;
    public Vector2 fullBarSize;
    public Vector2 emptyBarSize;
    public Texture2D fullTimerBar;
    public Texture2D emptyTimerBar;

	// Use this for initialization
	void Start () {
        gameLogicObj = GameObject.Find("GameLogic");
        if(gameLogicObj)
        {
            gameLoop = gameLogicObj.GetComponent<GameLoop>();
        }        
        if(gameLoop)
        {
            timeLimit = gameLoop.timeLimit;
            passedTime = 0.0f;
        }
    }

    void OnGUI()
    {
        if (!emptyTimerBar || !fullTimerBar)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }
        float currProgress = fullBarSize.x * Mathf.Clamp01(progress);
        GUI.DrawTexture(new Rect(emptyBarStartPos.x, emptyBarStartPos.y, emptyBarSize.x, emptyBarSize.y), emptyTimerBar);
        GUI.DrawTexture(new Rect(fullBartartPos.x, fullBartartPos.y, currProgress, fullBarSize.y), fullTimerBar);

        GUI.TextField(new Rect(emptyBarStartPos.x, emptyBarStartPos.y - fullBarSize.y, 70, 20), "B.C.1000");
        GUI.TextField(new Rect(emptyBarStartPos.x + fullBarSize.x - 70, emptyBarStartPos.y - fullBarSize.y, 70, 20), "C.E2016");
        GUI.TextField(new Rect(emptyBarStartPos.x - 18 + currProgress, emptyBarStartPos.y + emptyBarSize.y, 35, 20), "XXX");
    }
	
	// Update is called once per frame
	void Update () {
        passedTime += Time.deltaTime;
        progress = passedTime / timeLimit;          
	}
}
