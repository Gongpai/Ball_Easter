using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] int Set_Sec = 60;
    [SerializeField] int Set_Minute = 60;
    [SerializeField] int Set_Hour = 60;
    public static int Sec { get; private set; }
    public static int Minute { get; set; }
    public static int Hour { get; private set; }
    private float timer;

    void Start()
    {
        Sec = Set_Sec;
        Minute = Set_Minute;
        Hour = Set_Hour;
        timer = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //rint("hihihihi");
        if (timer <= 0.0f)
        {
            Sec--;
            print("timer not set : " + timer);
            timer = 1 + Time.deltaTime;
            print("timer is set : " + timer);
            print("Sec : " + Sec);
            if (Sec < 0)
            {
                Minute--;
                Sec = 59;
                if (Minute < 0 && Sec < 0)
                {
                    Hour--;
                    Minute = 60;
                }
            }
        }
        else
        {
            print("timer : " + timer);
            timer -= Time.deltaTime;
        }
        print(gameObject);
    }
}
