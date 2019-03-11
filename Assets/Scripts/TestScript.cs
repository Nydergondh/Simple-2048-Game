using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {

    [SerializeField] int cubeValue = 2;
    [SerializeField] List<Material> materials = new List<Material>();

    // Use this for initialization
    void Start () {
        StartCoroutine(CubeColor(1f));
    }

    // Update is called once per frame
    void Update () {
       
    }

    IEnumerator CubeColor(float time) {
        while (true) 
            for (int i = 0; i < 12; i++) {

                cubeValue += cubeValue;
                GetComponent<MeshRenderer>().material = materials[i];
                yield return new WaitForSeconds(time);
            }
            
    }
}
