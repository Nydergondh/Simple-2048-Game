using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubes : MonoBehaviour {

    [SerializeField] int cubeValue;
    public List <Material> materials = new List<Material>();
    MeshRenderer meshRenderer;
    // Use this for initialization

    void Start () {
        Two_Four();
        UpdateLabel();
        CubeChangeColor();
    }

    //Decide if a new block gonna spawn with value 2 (80%) or 4 (20%)
    private void Two_Four() {
        
        int maxRand = 10;
        maxRand = Random.Range(0, 9);

        if (maxRand >= 2) 
            cubeValue = 2;
          
        else
            cubeValue = 4;  
        
    }

    public void AddCubesValues(Cubes neighbourCube) {
        cubeValue += neighbourCube.GetCubeValue();
        CubeChangeColor();
        UpdateLabel();
        Destroy(neighbourCube.gameObject);
    }

    public int GetCubeValue() {
        return cubeValue;
    }

    private void UpdateLabel() {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();
        string labelText = cubeValue.ToString();
        textMesh.text = labelText;
        gameObject.name = labelText;
    }

    private void CubeChangeColor() {

        if (cubeValue == 2) {
            GetComponent<MeshRenderer>().material = materials[0];
        }
        else if (cubeValue == 4) {
            GetComponent<MeshRenderer>().material = materials[1];
        }
        else if (cubeValue == 8) {
            GetComponent<MeshRenderer>().material = materials[2];
        }
        else if (cubeValue == 16) {
            GetComponent<MeshRenderer>().material = materials[3];
        }
        else if (cubeValue == 32) {
            GetComponent<MeshRenderer>().material = materials[4];
        }
        else if (cubeValue == 64) {
            GetComponent<MeshRenderer>().material = materials[5];
        }
        else if (cubeValue == 128) {
            GetComponent<MeshRenderer>().material = materials[6];
        }
        else if (cubeValue == 256) {
            GetComponent<MeshRenderer>().material = materials[7];
        }
        else if (cubeValue == 512) {
            GetComponent<MeshRenderer>().material = materials[8];
        }
        else if (cubeValue == 1024) {
            GetComponent<MeshRenderer>().material = materials[9];
        }
        else if (cubeValue == 2048) {
            GetComponent<MeshRenderer>().material = materials[10];
        }
    }


}
