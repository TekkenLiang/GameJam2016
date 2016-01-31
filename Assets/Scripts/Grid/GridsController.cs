using UnityEngine;
using System.Collections;

public class GridsController : MonoBehaviour {

    // public variable defines
    public int gridNumberX;
    public int gridNumberY;
    public GameObject gridObject; // grid prefab

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
            }
        }
    }

    public void SetTargetForPlayer(int playerID, int gridX, int gridY)
    {
        grids[gridX, gridY].setToTargetGrid(playerID);
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
	
	}
}
