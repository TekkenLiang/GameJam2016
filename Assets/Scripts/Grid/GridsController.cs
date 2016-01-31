using UnityEngine;
using System.Collections;

public class GridsController : MonoBehaviour {

    // public variable defines
    public int gridNumberX;
    public int gridNumberY;
    public GameObject gridObject; // grid prefab

    // private variable defines
    private int height;
    private int width;
    private int gridsNum;
    private float gridHeight;
    private float gridWidth;

    // Use this for initialization
    void Start () {
        Resolution[] resolutions = Screen.resolutions;
        if (resolutions.Length == 1)
        {
            foreach (Resolution res in resolutions)
            {
                height = res.height;
                width = res.width;
            }
        }
        gridHeight = height / gridNumberY;
        gridWidth = width / gridNumberX;
        gridsNum = gridNumberX * gridNumberY;
        // set grid size
        Grid grid = gridObject.GetComponent<Grid>();
        if(grid)
        {
            grid.setHeight(gridHeight);
            grid.setWidth(gridWidth);
        }

        // spawn grids
        for(int i = 0; i < gridNumberY; ++i)
        {
            float y = (i + 0.5f) * gridHeight;
            for (int j = 0; j < gridNumberX; ++j)
            {
                float x = (i + 0.5f) * gridWidth;               
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
