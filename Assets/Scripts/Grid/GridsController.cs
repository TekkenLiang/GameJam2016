using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class GridsController : MonoBehaviour {

    // public variable defines
    public int gridNumberX;
    public int gridNumberY;
    public GameObject gridObject; // grid prefab

    public GameLoop gameLoop;

    public Vector2[] targetList1 = new Vector2[8];
    public Vector2[] targetList2 = new Vector2[8];
    public Grid player1CurrentTarget;
    public Grid player2CurrentTarget;
    int player1CurStage;
    int player2CurStage;

    // private variable defines   
    private float gridHeight;
    private float gridWidth;
	private List<List<Grid>> grids;

    
    // Use this for initialization
    public void InitializeGrids() {
        // set grid size
        Grid grid = gridObject.GetComponent<Grid>();
        gridHeight = grid.height;
        gridWidth = grid.width;
		grids = new List<List<Grid>>();


        // spawn grids
        for(int i = 0; i < gridNumberX; ++i)
		{
			float x = ((gridNumberX - 1) * 0.5f - i) * gridWidth; 
			List<Grid> gridRow = new List<Grid>();

            for (int j = 0; j < gridNumberY; ++j)
			{              
				float y = ((gridNumberY - 1) * 0.5f - j) * gridHeight;
                Vector3 position = new Vector3(x, y, 0);
                GameObject gridObj = Instantiate(gridObject, position, Quaternion.identity) as GameObject;
				Grid gridInstance = gridObj.GetComponent<Grid>();
				if ( gridInstance != null )
				{
					gridInstance.Init(this,position);
					gridRow.Add(gridInstance);
				}
            }
			grids.Add(gridRow);
        }
			
        player1CurStage = 0;
        player2CurStage = 0;
        SetTargetForPlayer(1, player1CurStage);
        SetTargetForPlayer(2, player2CurStage);
    }

    public void SetTargetForPlayer(int playerID, int stage)
    {
        int n = 1;
        Debug.Log(stage + " for Player: " + playerID);
        //grids[gridX, gridY].setToTargetGrid(playerID);
		if(playerID == 1)
		{
            int x = (int)targetList1[stage].x;
            int y = (int)targetList1[stage].y;
            while (!SetTargetForPlayer(playerID, x, y))
            {
                int delta = Random.Range(-n, n);
                x += delta;
                y += (int)((float)Random.Range(0,1)-0.5) * (n-Mathf.Abs(delta)) * 2 ;
            }

		}
		else
		{
            int x = (int)targetList2[stage].x;
            int y = (int)targetList2[stage].y;
            while (!SetTargetForPlayer(playerID, x, y))
            {
                int delta = Random.Range(-n, n);
                x += delta;
                y += (int)((float)Random.Range(0, 1) - 0.5) * (n - Mathf.Abs(delta)) * 2;
            }
		}
    }

    public bool SetTargetForPlayer(int playerID, int x, int y)
    {
        //if (x < 0) x = 0;
        //else if (x >= gridNumberX) x = gridNumberX - 1;
        //if (y <0) y=0;
        //else if (y >= gridNumberY) y = gridNumberY - 1;
        if (x < 0 || x >= gridNumberX || y < 0 || y >= gridNumberY) return false;

        if (playerID == 1)
        {
			
			Grid targetGrid = grids[x][y];
            player1CurrentTarget = targetGrid;
            return targetGrid.setToTargetGrid(playerID);
        }
        else
        {
			Grid targetGrid = grids[x][y];
            player2CurrentTarget = targetGrid;
            return targetGrid.setToTargetGrid(playerID);
        }

    }


	
    // get grid based on its idex
    public Grid getGrid(int x, int y)
	{ 
		Assert.IsFalse(x < 0 || x >= gridNumberX);
		Assert.IsFalse(y < 0 || y >= gridNumberY);
		return grids[x][y];
    }

    public float getGridHeight()
    {
        return gridHeight;
    }

    public float getGridWidth()
    {
        return gridWidth;
    }

    void Start()
    {

    }

	// Update is called once per frame
	void Update () {
        if (gameLoop.gameTimer > 0)
        {
            if (!player1CurrentTarget)
            {
                SetTargetForPlayer(1, player1CurStage);
                if (player1CurStage < 7) player1CurStage++;

            }
            if (!player2CurrentTarget)
            {
                SetTargetForPlayer(2, player2CurStage);
                if (player2CurStage < 7) player2CurStage++;
            }
        }

	}
}
