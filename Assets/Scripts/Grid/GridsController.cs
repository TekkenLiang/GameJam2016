using UnityEngine;
using System.Collections;

public class GridsController : MonoBehaviour {

    // public variable defines
    public int gridNumberX;
    public int gridNumberY;
    public GameObject gridObject; // grid prefab

    public Vector2[] targetList1 = new Vector2[8];
    public Vector2[] targetList2 = new Vector2[8];
    public Grid player1CurrentTarget;
    public Grid player2CurrentTarget;
    int player1CurStage;
    int player2CurStage;

    // private variable defines   
    private float gridHeight;
    private float gridWidth;
    private Grid[,] grids;
    
    // Use this for initialization
    public void InitializeGrids() {
        // set grid size
        Grid grid = gridObject.GetComponent<Grid>();
        gridHeight = grid.height;
        gridWidth = grid.width;
        grids = new Grid[gridNumberX, gridNumberY];
        // spawn grids
        for(int i = 0; i < gridNumberY; ++i)
        {
            float y = ((gridNumberY - 1) * 0.5f - i) * gridHeight;
            for (int j = 0; j < gridNumberX; ++j)
            {
                float x = ((gridNumberX - 1) * 0.5f - j) * gridWidth;               
                Vector3 position = new Vector3(x, y, 0);
                Grid gridInstance = (Grid)Instantiate(grid, position, Quaternion.identity);
                gridInstance.setPosition(position);
                grids[j, i] = gridInstance;
                gridInstance.transform.SetParent(transform, false);
				//gridInstance.gridsController = transform.parent;
            }
        }

        player1CurStage = 0;
        player2CurStage = 0;
        SetTargetForPlayer(1, player1CurStage);
        SetTargetForPlayer(2, player2CurStage);
    }

    public void SetTargetForPlayer(int playerID, int stage)
    {
        Debug.Log(stage + "for Player: " + playerID);
        //grids[gridX, gridY].setToTargetGrid(playerID);
		if(playerID == 1)
		{
            Grid targetGrid = grids[(int)targetList1[stage].x, (int)targetList1[stage].y];
            targetGrid.setToTargetGrid(playerID);
            player1CurrentTarget = targetGrid;
		}
		else
		{
            Grid targetGrid = grids[(int)targetList2[stage].x, (int)targetList2[stage].y];
            targetGrid.setToTargetGrid(playerID);
            player2CurrentTarget = targetGrid;
		}
    }


	
    // get grid based on its idex
    public Grid getGrid(int x, int y)
    {
        return grids[x, y];
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
		if(!player1CurrentTarget)
		{
			SetTargetForPlayer(1, player1CurStage);
            player1CurStage++;
		}
		if(!player2CurrentTarget)
		{
			SetTargetForPlayer(2, player2CurStage);
            player2CurStage++;
		}

	}
}
