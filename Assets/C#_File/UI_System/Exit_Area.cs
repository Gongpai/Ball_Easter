using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exit_Area : MonoBehaviour
{
    [SerializeField] private GameObject M_Menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
             CustomEvent.Trigger(M_Menu, "Level_Time_Out");
        }
        print("EEEEEEXXXXXXIIIITTTTTT");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
