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

    public Vector2 fullBarstartPos;
    public Vector2 emptyBarstartPos;
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
        float currProgress = fullBarSize.x * Mathf.Clamp01(progress);
        GUI.DrawTexture(new Rect(emptyBarstartPos.x, emptyBarstartPos.y, emptyBarSize.x, emptyBarSize.y), emptyTimerBar);
        GUI.DrawTexture(new Rect(fullBarstartPos.x, fullBarstartPos.y, fullBarSize.x * Mathf.Clamp01(progress), fullBarSize.y), fullTimerBar);
    }
	
	// Update is called once per frame
	void Update () {
        passedTime += Time.deltaTime;
        progress = passedTime / timeLimit;          
	}
}
