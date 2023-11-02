using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int Seed;
    public int Dimension;
    public int ScaleMultiplier = 1;
    [Range(0f, 1f)]
    public double BreakProbability;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BakeMaze()
    {
        World.Maze maze = new(Dimension, BreakProbability);
        maze.Generate(Seed);
        var existingMaze = transform.Find("BakedMaze");
        if (existingMaze != null)
        {
            DestroyImmediate(existingMaze.gameObject);
        }
        var newMaze = new GameObject();
        newMaze.name = "BakedMaze";
        newMaze.transform.parent = transform;
        newMaze.transform.localPosition = Vector3.zero;

        var halfWay = Dimension / 2f - 0.5f;
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < Dimension; j++)
            {
                var cell = maze.Cells[i, j];
                var cellGameObject = new GameObject();
                cellGameObject.name = "Cell";
                cellGameObject.transform.parent = newMaze.transform;
                cellGameObject.transform.localPosition = new Vector3((i - halfWay) * ScaleMultiplier, 0, -(j - halfWay) * ScaleMultiplier);

                if (j == Dimension - 1)
                {
                    var theWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    theWall.transform.parent = cellGameObject.transform;
                    theWall.transform.localPosition = new Vector3(0, 0, -0.5f * ScaleMultiplier);
                    theWall.transform.localScale = new Vector3(1.1f * ScaleMultiplier, 3, 0.1f * ScaleMultiplier);
                }
                if (i == Dimension - 1)
                {
                    var theWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    theWall.transform.parent = cellGameObject.transform;
                    theWall.transform.localPosition = new Vector3(0.5f * ScaleMultiplier, 0, 0);
                    theWall.transform.localScale = new Vector3(0.1f * ScaleMultiplier, 3, 1.1f * ScaleMultiplier);
                }

                if (j == 0 || cell.TopWall)
                {
                    var theWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    theWall.transform.parent = cellGameObject.transform;
                    theWall.transform.localPosition = new Vector3(0, 0, 0.5f * ScaleMultiplier);
                    theWall.transform.localScale = new Vector3(1.1f * ScaleMultiplier, 3, 0.1f * ScaleMultiplier);
                }
                if (i == 0 || cell.LeftWall)
                {
                    var theWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    theWall.transform.parent = cellGameObject.transform;
                    theWall.transform.localPosition = new Vector3(-0.5f * ScaleMultiplier, 0, 0);
                    theWall.transform.localScale = new Vector3(0.1f * ScaleMultiplier, 3, 1.1f * ScaleMultiplier);
                }
            }
        }
    }
}
