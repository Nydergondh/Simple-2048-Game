using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//IMPORTANTE considerar criar novas classes pare diminuir essa classe

public class Board : MonoBehaviour {

    List<Position> grid_pos = new List<Position>();

    [SerializeField] GameObject cube;
    [SerializeField] Transform cubeRepository;
    [SerializeField] Canvas canvas;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 delta = Vector3.zero;
    private int gridSize = 4;
    private List <bool>  isDead;
    private float cubeMoveSpeed = 1f;

    // Use this for initialization
    void Start() {
        InicializeGrid();
        SpawnNewCube();
        SpawnNewCube();
        isDead = new List<bool>();
        InicializeIsDead();
    }

    void Update() {
        /*
        if (Input.GetMouseButtonDown(0)) {
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0)) {
            delta = Input.mousePosition - lastPos;

            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) {
            if (delta.x != 0 && delta.y != 0) {
                MoveCubes();
                SpawnNewCube();
            }
        }
        */
        MoveCubes();
        
    }

    private void InicializeIsDead() {
        isDead.Add(false);
        isDead.Add(false);
        isDead.Add(false);
        isDead.Add(false);
    }

    private void InicializeGrid() {

        int i = 0;

        Position[] pos = GetComponentsInChildren<Position>();

        for (int l = 0; l < gridSize; l++) {
            for (int c = 0; c < gridSize; c++, i++) {
                pos[i].SetPosition(l, c);
                grid_pos.Add(pos[i]);
            }
        }
        SetBoardNeighbours();
    }

    private void SetBoardNeighbours(){

        int i = 0, up = -4, right = 1, down = 4, left = -1; 

        List<Position> neighPos = new List<Position>();

        for (int l = 0; l < gridSize; l++) {
            
            for (int c = 0; c < gridSize; c++, i++) {

                if (l == 0) {
                    if (c == 0) {
                        neighPos.Add(null);
                        neighPos.Add(grid_pos[i+right]);
                        neighPos.Add(grid_pos[i+down]);
                        neighPos.Add(null);                                               
                    }
                    else if(c == 3){
                        neighPos.Add(null);
                        neighPos.Add(null);
                        neighPos.Add(grid_pos[i + down]);
                        neighPos.Add(grid_pos[i + left]);
                    }
                    else {
                        neighPos.Add(null);
                        neighPos.Add(grid_pos[i + right]);
                        neighPos.Add(grid_pos[i + down]);
                        neighPos.Add(grid_pos[i + left]);
                    }
                }

                else if (l == 3) {
                    if (c == 0) {
                        neighPos.Add(grid_pos[i + up]);
                        neighPos.Add(grid_pos[i + right]);
                        neighPos.Add(null);
                        neighPos.Add(null);
                    }
                    else if (c == 3) {
                        neighPos.Add(grid_pos[i + up]);
                        neighPos.Add(null);
                        neighPos.Add(null);
                        neighPos.Add(grid_pos[i + left]);
                    }
                    else {
                        neighPos.Add(grid_pos[i + up]);
                        neighPos.Add(grid_pos[i + right]);
                        neighPos.Add(null);
                        neighPos.Add(grid_pos[i + left]);
                    }
                }
                
                else if (c == 0) {
                    neighPos.Add(grid_pos[i + up]);
                    neighPos.Add(grid_pos[i + right]);
                    neighPos.Add(grid_pos[i + down]);
                    neighPos.Add(null);
                }

                else if (c == 3) {
                    neighPos.Add(grid_pos[i + up]);
                    neighPos.Add(null);
                    neighPos.Add(grid_pos[i + down]);
                    neighPos.Add(grid_pos[i + left]);
                }

                else {
                    neighPos.Add(grid_pos[i + up]);
                    neighPos.Add(grid_pos[i + right]);
                    neighPos.Add(grid_pos[i + down]);
                    neighPos.Add(grid_pos[i + left]);
                }

                grid_pos[i].SetNeighbours(neighPos);
                neighPos.Clear();
            }
        }

    }

    //return a list of the positions that are not occupied by blocks
    private List<Position> GetFreePositions() {

        List<Position> freePositions = new List<Position>();

        for (int i = 0; i < grid_pos.Count ; i++) {         
            
            if (!grid_pos[i].occupied) {
                freePositions.Add(grid_pos[i]);                
            }

        }

        return freePositions;
    }
    

    //spawn new cube (its ment every time a movement happen)
    private void SpawnNewCube() {
        
        List<Position> freePosition = GetFreePositions();        
        
        int newCube;
        int maxRand = freePosition.Count-1;

        if (freePosition.Count > 0) {

            if (maxRand >= 0)
                newCube = Random.Range(0, maxRand);
            else {
                newCube = 0;
            }

            CheckNewPosition(freePosition, newCube);
        }

    }

    //with a random number (newCube) this function chooses witch of the free position avaliable a new cube will get spawned at
    private void CheckNewPosition(List<Position> freePosition, int newCube) {

        for (int i = 0; i < grid_pos.Count; i++) {

            if (grid_pos[i].Equals(freePosition[newCube])) {

                grid_pos[i].occupied = true;
                
                GameObject cubePos = Instantiate(cube, new Vector3 (grid_pos[i].transform.position.x, grid_pos[i].transform.position.y, (grid_pos[i].transform.position.z - 0.1f)), Quaternion.identity, cubeRepository);
                grid_pos[i].SetCube(cubePos);
                grid_pos[i].cube.name = grid_pos[i].name;
                break;
            }

        }

    }

    //check the mouse input to then move the cubes
    private void MoveCubes() {


        int testMovement = 0;

        //(delta.x < 0 && Mathf.Abs(delta.x) >= Mathf.Abs(delta.y)) code for mouse input
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            for (int i = 0; i < grid_pos.Count; i++) {
                if (grid_pos[i].occupied) {
                    if (grid_pos[i].GetNeighLeft() != null) {
                        testMovement += MoveX(i,false);
                    }
                }
            }
            if (testMovement > 0) {
                SpawnNewCube();

                
            }
            else {
                isDead[0] = true;
            }
            print(testMovement + "test");
        }
        //(delta.x > 0 && Mathf.Abs(delta.x) >= Mathf.Abs(delta.y))
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {

            for (int i = grid_pos.Count - 1; i >= 0; i--) {
                if (grid_pos[i].occupied) {
                    if (grid_pos[i].GetNeighRight() != null) {
                        testMovement += MoveX(i, true);
                    }
                }
            }
            if (testMovement > 0) {
                SpawnNewCube();  
            }
            else {
                isDead[1] = true;
            }
            print(testMovement+ "test");
        }
        //(delta.y < 0 && Mathf.Abs(delta.y) >= Mathf.Abs(delta.x))
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {

            for (int i = grid_pos.Count-1; i >= 0; i--) {
                if (grid_pos[i].occupied) {
                    if (grid_pos[i].GetNeighDown() != null) {
                        testMovement += MoveY(i, false);
                    }
                }
            }
            if (testMovement > 0) {
                SpawnNewCube();               
            }
            else {
                isDead[2] = true; 
            }
            print(testMovement + "test");
        }
        //else if (delta.y > 0 && Mathf.Abs(delta.y) >= Mathf.Abs(delta.x))
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {

            for (int i = 0; i < grid_pos.Count; i++) {
                if (grid_pos[i].occupied) {
                    if (grid_pos[i].GetNeighUp() != null) {
                        testMovement += MoveY(i, true);
                    }
                }
            }
            if (testMovement > 0) {
                SpawnNewCube();                
            }
            else {
                isDead[3] = true;
            }
            print(testMovement + "test");
        }

        if (CheckIfDead()) {
            Text text = canvas.GetComponentInChildren<Text>();
            text.enabled = true;
            Invoke("GameOver",5f);
        }
    }

    private bool CheckIfDead() {
        for(int i = 0; i < isDead.Count; i++) {
            if (!isDead[i]) {
                return false;
            }
        }
        return true;
    }

    private void GameOver() {
        SceneManager.LoadScene(0);
    }

    //methods to update the occupation attributes of the positions
    private void UpdateCubePositionsLeft(int i) {
        grid_pos[i].occupied = false;
        grid_pos[i].GetNeighLeft().occupied = true;
        grid_pos[i].GetNeighLeft().SetCube(grid_pos[i].cube);
        grid_pos[i].SetCube(null);
    }

    private void UpdateCubePositionsRight(int i) {
        grid_pos[i].occupied = false;
        grid_pos[i].GetNeighRight().occupied = true;
        grid_pos[i].GetNeighRight().SetCube(grid_pos[i].cube);
        grid_pos[i].SetCube(null);
    }

    private void UpdateCubePositionsUp(int i) {
        grid_pos[i].occupied = false;
        grid_pos[i].GetNeighUp().occupied = true;
        grid_pos[i].GetNeighUp().SetCube(grid_pos[i].cube);
        grid_pos[i].SetCube(null);
    }

    private void UpdateCubePositionsDown(int i) {
        grid_pos[i].occupied = false;
        grid_pos[i].GetNeighDown().occupied = true;
        grid_pos[i].GetNeighDown().SetCube(grid_pos[i].cube);
        grid_pos[i].SetCube(null);
    }

    //Methods to move one position all the present cubes to one direction 
    //depending if the mouse input is positive or negative in the x or y axis

    private int MoveX(int i, bool directionX) {

        Cubes movingCube;
        Cubes hitedCube;

        float distance = 10f;
        float xPos = grid_pos[i].cube.transform.position.x;
        float yPos = grid_pos[i].cube.transform.position.y;
        float zPos = grid_pos[i].cube.transform.position.z;

        int moveResult = 0;

        if(directionX) {

            while (i >= 0) {
                
                if (grid_pos[i].GetNeighRight() == null) {
                    break;
                }

                if (grid_pos[i].GetNeighOccRight()) {
                    movingCube = grid_pos[i].cube.GetComponent<Cubes>();
                    hitedCube = grid_pos[i].GetNeighRight().cube.GetComponent<Cubes>();
                    if (movingCube.GetCubeValue() == hitedCube.GetCubeValue()) {
                        moveResult += 1;
                        movingCube.AddCubesValues(hitedCube);
                    }
                    else {
                        break;
                    }
                }

                while (distance > 0f) {

                    distance -= cubeMoveSpeed;
                    xPos += cubeMoveSpeed;
                    grid_pos[i].cube.transform.position = new Vector3(xPos, yPos, zPos);
                   
                }
                moveResult += 1;
                UpdateCubePositionsRight(i);
                i++;
                distance = 10f;
            }

        }

        else {

            while (i < grid_pos.Count) {
                //in case the index neighbour is bellow or above the the expectations to prevent cashe invalidation
                if (grid_pos[i].GetNeighLeft() == null) {
                    break;
                }

                if (grid_pos[i].GetNeighOccLeft()) {
                    movingCube = grid_pos[i].cube.GetComponent<Cubes>();
                    hitedCube = grid_pos[i].GetNeighLeft().cube.GetComponent<Cubes>();
                    if (movingCube.GetCubeValue() == hitedCube.GetCubeValue()) {
                        moveResult += 1;
                        movingCube.AddCubesValues(hitedCube);
                    }
                    else {
                        break;
                    }
                }

                while (distance > 0f) {

                    distance -= cubeMoveSpeed;
                    xPos -= cubeMoveSpeed;
                    grid_pos[i].cube.transform.position = new Vector3(xPos, yPos, zPos);

                }
                moveResult += 1;
                UpdateCubePositionsLeft(i);
                i--;
                distance = 10f;
            }
            
        }
        return moveResult;
    }

    private int MoveY(int i, bool directionY) {

        Cubes movingCube;
        Cubes hitedCube;
        int moveResult = 0;

        float distance = 10f;
        float xPos = grid_pos[i].cube.transform.position.x;
        float yPos = grid_pos[i].cube.transform.position.y;
        float zPos = grid_pos[i].cube.transform.position.z;

        if (directionY) {
            while (i > 0) {

                if (grid_pos[i].GetNeighUp() == null) {
                    break;
                }

                if (grid_pos[i].GetNeighOccUp()) {
                    movingCube = grid_pos[i].cube.GetComponent<Cubes>();
                    hitedCube = grid_pos[i].GetNeighUp().cube.GetComponent<Cubes>();
                    if (movingCube.GetCubeValue() == hitedCube.GetCubeValue()) {
                        moveResult += 1;
                        movingCube.AddCubesValues(hitedCube);
                    }
                    else {
                        break;
                    }
                }

                while (distance > 0f) {

                    distance -= cubeMoveSpeed;
                    yPos += cubeMoveSpeed;
                    grid_pos[i].cube.transform.position = new Vector3(xPos, yPos, zPos);

                }
                moveResult += 1;
                UpdateCubePositionsUp(i);
                i -= 4;
                distance = 10f;
            }
            
        }

        else {
            while (i < grid_pos.Count) {
                //in case the index neighbour is bellow or above the the expectations to prevent cashe invalidation
                if (grid_pos[i].GetNeighDown() == null) {
                    break;
                }

                if (grid_pos[i].GetNeighOccDown()) {
                    movingCube = grid_pos[i].cube.GetComponent<Cubes>();
                    hitedCube = grid_pos[i].GetNeighDown().cube.GetComponent<Cubes>();
                    if (movingCube.GetCubeValue() == hitedCube.GetCubeValue()) {
                        moveResult += 1;
                        movingCube.AddCubesValues(hitedCube);
                    }
                    else {
                        break;
                    }
                }

                while (distance > 0f) {

                    distance -= cubeMoveSpeed;
                    yPos -= cubeMoveSpeed;
                    grid_pos[i].cube.transform.position = new Vector3(xPos, yPos, zPos);

                }
                moveResult += 1;
                UpdateCubePositionsDown(i);
                i += 4;
                distance = 10f;
            }

        }

        return moveResult;

    }   

}
    

