using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    public bool occupied = false;
    public GameObject cube;

    Dictionary<Vector2Int,Position> Neighbours = new Dictionary<Vector2Int, Position>();
    Vector2Int grid_pos = new Vector2Int();
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition(int l, int c) {

        grid_pos.x = l;
        grid_pos.y = c;
        occupied = false;

    }

    public Vector2Int GetPosition() {

        Vector2Int vector2Int = new Vector2Int(grid_pos.x,grid_pos.y);
        return vector2Int;

    }

    public void SetNeighbours(List<Position> neighPos) {
        int i = 0;
        Neighbours.Add(Vector2Int.up, neighPos[i]); i++;
        Neighbours.Add(Vector2Int.right, neighPos[i]); i++;
        Neighbours.Add(Vector2Int.down, neighPos[i]); i++;
        Neighbours.Add(Vector2Int.left, neighPos[i]); i++;
    }

    public bool GetNeighOccUp() {
        return Neighbours[Vector2Int.up].occupied;
    }

    public bool GetNeighOccRight() {
        return Neighbours[Vector2Int.right].occupied;
    }

    public bool GetNeighOccDown() {
        return Neighbours[Vector2Int.down].occupied;
    }

    public bool GetNeighOccLeft() {
        return Neighbours[Vector2Int.left].occupied;
    }

    public Position GetNeighUp() {
        return Neighbours[Vector2Int.up];
    }

    public Position GetNeighRight() {
        return Neighbours[Vector2Int.right];
    }

    public Position GetNeighDown() {
        return Neighbours[Vector2Int.down];
    }

    public Position GetNeighLeft() {
        return Neighbours[Vector2Int.left];
    }

    public void SetCube(GameObject newCube) {
        cube = newCube;
    }

}
