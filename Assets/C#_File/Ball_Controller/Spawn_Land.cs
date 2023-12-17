using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_Land : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject C_Land;
    private Vector3 point;
    private Transform child;
    private bool Is_Spawn; 
    void Start()
    {
        Is_Spawn = false;
    }
    

    private void OnTriggerEnter(Collider other)
    {
        string na;
        na = other.gameObject.tag;
        if (na == "Point" && Is_Spawn == false)
        { 
            child = other.transform.GetChild(0); 
            point = child.position;
            SpawnLand(C_Land);
            Is_Spawn = true;
            print(na + " Spawn True " + gameObject.name);
            Destroy(other.gameObject);
        }
    }

    private void SpawnLand(GameObject newland)
    {
        GameObject land = GameObject.Instantiate(newland);
        Quaternion rotation_land = Quaternion.Euler(25, 0, 0);
        land.transform.position = point;
        land.transform.rotation = rotation_land;
    }
}
