using UnityEngine;
using System.Collections;

public class GridsController : MonoBehaviour {

    // public variable defines
    public int gridNumberX;
    public int gridNumberY;
    public int height;
    public int width;
    public GameObject gridObject; // grid prefab

    // private variable defines   
    private int gridsNum;
    private float gridHeight;
    private float gridWidth;

    // Use this for initialization
    void Start () {
        // set grid size
        Grid grid = gridObject.GetComponent<Grid>();
        gridHeight = grid.height;
        gridWidth = grid.width;

        // spawn grids
        for(int i = 0; i < gridNumberY; ++i)
        {
            float y = (i + 0.5f) * gridHeight;
            for (int j = 0; j < gridNumberX; ++j)
            {
                float x = (j + 0.5f) * gridWidth;               
                Vector3 position = new Vector3(x, y, 0);
                Instantiate(grid, position, Quaternion.identity);
            }
        }
    }
	
    float getGridHeight()
    {
        return gridHeight;
    }

    float getGridWidth()
    {
        return gridWidth;
    }


	// Update is called once per frame
	void Update () {
	
	}
}
