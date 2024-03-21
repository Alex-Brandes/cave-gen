using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSetup : MonoBehaviour
{
    public GameObject groundPrefab;
    int height;
    int width;

    void Start()
    {
        height = GetComponent<MapGenerator>().height;
        width = GetComponent<MapGenerator>().width;
        //Ersatzlösung -0.001f wegen Untergrund 
        GameObject ground1 = Instantiate(groundPrefab, new Vector3(height/2, -0.001f, width/2), Quaternion.identity);
        ground1.transform.localScale = new Vector3(width/10, 1, height/10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
