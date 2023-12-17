using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Time_Ui : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Text _TextTime;

    void Update()
    {
        _TextTime.text = $"Time {Timer.Hour:00}:{Timer.Minute:00}:{Timer.Sec:00}";
    }
}
