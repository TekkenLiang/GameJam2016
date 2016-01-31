using UnityEngine;
using System.Collections;

public class TimerProgressBar : MonoBehaviour {
    private float timeLimit;
    private float passedTime;
    private float progress;
    private GameObject gameLogicObj;
    private GameLoop gameLoop;
    private Vector2 fullTimerBarSize;
    private Vector2 emptyTimerBarSize;

    public Vector2 startPos;
    public Vector2 size;
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

        fullTimerBarSize = fullTimerBar.texelSize;
        emptyTimerBarSize = emptyTimerBar.texelSize;
    }

    void OnGUI()
    {
        if (!emptyTimerBar || !fullTimerBar)
        {
            Debug.LogError("Assign a Texture in the inspector.");
            return;
        }
        float currProgress = size.x * Mathf.Clamp01(progress);
        GUI.DrawTexture(new Rect(startPos.x, startPos.y, size.x, size.y), emptyTimerBar);
        GUI.DrawTexture(new Rect(startPos.x, startPos.y, size.x * Mathf.Clamp01(progress), size.y), fullTimerBar);

        //string processText = 
        GUI.TextField(new Rect(startPos.x, startPos.y - size.y, 70, 20), "B.C.1000");
        GUI.TextField(new Rect(startPos.x + size.x - 70, startPos.y - size.y, 70, 20), "C.E2016");
        GUI.TextField(new Rect(startPos.x - 18 + size.x * Mathf.Clamp01(progress), startPos.y + size.y, 35, 20), "XXX");
    }
	
	// Update is called once per frame
	void Update () {
        passedTime += Time.deltaTime;
        progress = passedTime / timeLimit;          
	}
}
