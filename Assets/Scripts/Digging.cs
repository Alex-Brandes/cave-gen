using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digging : MonoBehaviour
{
    public GameObject mapGenObject;
    MeshGenerator meshGen;
    MapGenerator mapGen;
    int wallLayer;
    Vector3 pos;
    Vector3 targetField;
    void Start()
    {
        meshGen = mapGenObject.GetComponent<MeshGenerator>();
        mapGen = mapGenObject.GetComponent<MapGenerator>();
        Debug.Log(mapGenObject.GetComponent<MeshGenerator>());
        wallLayer = LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    void Update()
    {

        //only for debugging, see onDrawGizmos
        RaycastHit hitGiz;
        Ray rayGiz = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayGiz, out hitGiz, Mathf.Infinity, wallLayer))
        {
            //Debug.Log(Vector3.Angle(hitGiz.normal, Vector3.forward));
            //Debug.Log(hitGiz.point);

            //This if clause sets the right targetField for 90° walls
            if (Mathf.Approximately(Vector3.Angle(hitGiz.normal, Vector3.forward) % 90, 0)) {
                //Debug.Log("approx 0");
                targetField = new Vector3(Mathf.RoundToInt(hitGiz.point.x - hitGiz.normal.x * 1/2), 0, Mathf.RoundToInt(hitGiz.point.z - hitGiz.normal.z * 1/2));
            }

            else
            {
                if (mapGen.map[Mathf.RoundToInt(hitGiz.point.x), Mathf.RoundToInt(hitGiz.point.z)] == 0)
                {
                    //Debug.Log(hitGiz.point.x - Mathf.RoundToInt(hitGiz.point.x) > (-1/6f));
                    if (hitGiz.point.x - Mathf.RoundToInt(hitGiz.point.x) > (-1 / 6f) && hitGiz.point.x - Mathf.RoundToInt(hitGiz.point.x) < (1 / 6f))
                    {
                        Debug.Log("x in the middle");
                        targetField = new Vector3(Mathf.RoundToInt(hitGiz.point.x), 0, Mathf.RoundToInt(hitGiz.point.z + Mathf.RoundToInt(-hitGiz.normal.z)));
                    }
                    else if (hitGiz.point.z - Mathf.RoundToInt(hitGiz.point.z) > (-1 / 6f) && hitGiz.point.z - Mathf.RoundToInt(hitGiz.point.z) < (1 / 6f))
                    {
                        targetField = new Vector3(Mathf.RoundToInt(hitGiz.point.x + Mathf.RoundToInt(-hitGiz.normal.x)), 0, Mathf.RoundToInt(hitGiz.point.z));
                    }
                    else
                    {
                        targetField = new Vector3(Mathf.RoundToInt(hitGiz.point.x - hitGiz.normal.x * Mathf.Sqrt(2) / 2), 0, Mathf.RoundToInt(hitGiz.point.z - hitGiz.normal.z * Mathf.Sqrt(2) / 2));
                    }
                }
                else
                {
                    targetField = new Vector3(Mathf.RoundToInt(hitGiz.point.x), 0, Mathf.RoundToInt(hitGiz.point.z));
                }
                   
            }
            pos = targetField;

            //Debug.Log(mapGen.map[Mathf.RoundToInt(hitGiz.point.x), Mathf.RoundToInt(hitGiz.point.z)]);

        }



        if (Input.GetMouseButtonDown(0))
        {
            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, wallLayer))
            //{

            //    //Debug.Log(hit.point);
            //    if (Mathf.Approximately(Vector3.Angle(hit.normal, Vector3.forward) % 90, 0))
            //    {
            //        Debug.Log("approx 0");
            //        targetField = new Vector3(Mathf.RoundToInt(hit.point.x - hit.normal.x * 1 / 2), 0, Mathf.RoundToInt(hit.point.z - hit.normal.z * 1 / 2));
            //    }

            mapGen.destroyField((int)targetField.x, (int)targetField.z);
            mapGen.reloadMeshes();
        //}
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        //the position of the gizmos is calculated here
        //Vector3 pos = new Vector3(-width/2 + x + .5f,0, -height/2 + y+.5f);
        Gizmos.DrawCube(pos, new Vector3(1, 3, 1));
        
    }
}
