using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Point : MonoBehaviour 
{
    // Start is called before the first frame update
    public static Action AddPoint;
    public static int totalpoint { get; set; }
    public static string[] totalTime = new String[3];
    public static int point{ get; set;}
    void Start()
    {
        point = 0;
        totalTime[0] = "L1 = 00:00, ";
        totalTime[1] = "L2 = 00:00, ";
        totalTime[2] = "L3 = 00:00, ";
        totalpoint = 0;
        AddPoint?.Invoke();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
